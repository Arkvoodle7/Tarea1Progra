using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class NegociosUsuario
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public int IniciarSesion(string correo, string contrasena, out byte[] salt)
        {
            salt = null;
            int usuarioId = -1;
            byte[] contrasenaAlmacenada = null;

            using (SqlDataReader reader = accesoDatos.ObtenerUsuarioPorCorreo(correo))
            {
                if (reader.Read())
                {
                    usuarioId = Convert.ToInt32(reader["Usuario_ID"]);
                    contrasenaAlmacenada = (byte[])reader["Contrasena"];
                    salt = (byte[])reader["Salt"];
                }
                reader.Close();
            }

            if (usuarioId != -1 && salt != null && contrasenaAlmacenada != null)
            {
                byte[] contrasenaIngresadaHash = HashearContrasena(contrasena, salt);

                if (CompararHashes(contrasenaAlmacenada, contrasenaIngresadaHash))
                {
                    //credenciales correctas
                    return usuarioId;
                }
            }

            //credenciales incorrectas
            return -1;
        }

        public bool RegistrarUsuario(string correo, string contrasena, out string mensaje)
        {
            mensaje = "";
            if (!EsContrasenaValida(contrasena))
            {
                mensaje = "La contraseña no cumple con los requisitos.";
                return false;
            }

            //generar Salt
            byte[] salt = GenerarSalt();

            //hashear la contraseña con el Salt
            byte[] contrasenaHash = HashearContrasena(contrasena, salt);

            try
            {
                accesoDatos.InsertarUsuario(correo, contrasenaHash, salt);
                return true;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    mensaje = "El correo ya está registrado.";
                }
                else
                {
                    mensaje = "Ocurrió un error al registrar el usuario.";
                }
                return false;
            }
        }

        private bool EsContrasenaValida(string contrasena)
        {
            //validar que la contraseña tenga al menos 8 caracteres, letras, números y caracteres especiales
            if (contrasena.Length < 8)
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[A-Za-z]"))
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"\d"))
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"[+\-*/%$#!?]"))
                return false;
            return true;
        }

        private byte[] GenerarSalt()
        {
            byte[] salt = new byte[16]; //128 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] HashearContrasena(string contrasena, byte[] salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(contrasena, salt, 10000))
            {
                return deriveBytes.GetBytes(32); //256 bits
            }
        }

        private bool CompararHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
                return false;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }
            return true;
        }
    }
}