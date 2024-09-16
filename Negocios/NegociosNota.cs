using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Datos;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;

namespace Negocios
{
    public class NegociosNota
    {
        private AccesoDatos accesoDatos = new AccesoDatos();

        public List<NotaDTO> ObtenerNotas(int usuarioId)
        {
            List<NotaDTO> notas = new List<NotaDTO>();
            using (SqlDataReader reader = accesoDatos.ObtenerNotasPorUsuario(usuarioId))
            {
                while (reader.Read())
                {
                    NotaDTO nota = new NotaDTO
                    {
                        Nota_ID = Convert.ToInt32(reader["Nota_ID"]),
                        Titulo = reader["Titulo"].ToString(),
                        Fecha_creacion = Convert.ToDateTime(reader["Fecha_creacion"])
                    };
                    notas.Add(nota);
                }
                reader.Close();
            }
            return notas;
        }

        public string MostrarContenidoNota(int notaId, int usuarioId, string claveUsuario, byte[] salt)
        {
            byte[] contenidoCifrado = accesoDatos.ObtenerContenidoNota(notaId, usuarioId);

            if (contenidoCifrado != null)
            {
                return DesencriptarContenido(contenidoCifrado, claveUsuario, salt);
            }
            else
            {
                return null;
            }
        }

        public void AgregarNota(int usuarioId, string titulo, string contenido, string claveUsuario, byte[] salt)
        {
            byte[] contenidoCifrado = EncriptarContenido(contenido, claveUsuario, salt);
            accesoDatos.InsertarNota(usuarioId, titulo, contenidoCifrado);
        }

        public void EliminarNota(int notaId, int usuarioId)
        {
            accesoDatos.EliminarNota(notaId, usuarioId);
        }

        public bool DuplicarNota(int notaId, int usuarioId, out string mensaje)
        {
            mensaje = "";
            using (SqlDataReader reader = accesoDatos.ObtenerNotaPorId(notaId, usuarioId))
            {
                if (reader.Read())
                {
                    string tituloOriginal = reader["Titulo"].ToString();
                    byte[] contenidoCifrado = (byte[])reader["Contenido"];

                    //crear el título para la nota duplicada
                    string tituloDuplicado = "Copia de " + tituloOriginal;

                    //insertar la nota duplicada
                    accesoDatos.InsertarNota(usuarioId, tituloDuplicado, contenidoCifrado);

                    mensaje = "Nota duplicada con éxito.";
                    return true;
                }
                else
                {
                    mensaje = "Nota no encontrada.";
                    return false;
                }
            }
        }

        public List<NotaExportarDTO> ObtenerNotasParaExportar(List<int> notasIds, int usuarioId, string claveUsuario, byte[] salt)
        {
            List<NotaExportarDTO> notasParaExportar = new List<NotaExportarDTO>();

            foreach (int notaId in notasIds)
            {
                using (var reader = accesoDatos.ObtenerNotaPorId(notaId, usuarioId))
                {
                    if (reader.Read())
                    {
                        string titulo = reader["Titulo"].ToString();
                        byte[] contenidoCifrado = (byte[])reader["Contenido"];
                        string contenidoDesencriptado = DesencriptarContenido(contenidoCifrado, claveUsuario, salt);

                        notasParaExportar.Add(new NotaExportarDTO
                        {
                            Titulo = titulo,
                            Contenido = contenidoDesencriptado
                        });
                    }
                    reader.Close();
                }
            }

            return notasParaExportar;
        }

        //métodos de encriptación y desencriptación
        private byte[] EncriptarContenido(string contenido, string claveUsuario, byte[] salt)
        {
            byte[] textoPlano = Encoding.UTF8.GetBytes(contenido);
            byte[] clave = ObtenerClaveBytes(claveUsuario, salt);

            //generar un IV aleatorio de 12 bytes
            byte[] iv = new byte[12];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(clave), 128, iv, null);
            cipher.Init(true, parameters);

            byte[] textoCifrado = new byte[cipher.GetOutputSize(textoPlano.Length)];
            int len = cipher.ProcessBytes(textoPlano, 0, textoPlano.Length, textoCifrado, 0);
            len += cipher.DoFinal(textoCifrado, len);

            //concatenar IV y texto cifrado
            byte[] resultado = new byte[iv.Length + textoCifrado.Length];
            Array.Copy(iv, 0, resultado, 0, iv.Length);
            Array.Copy(textoCifrado, 0, resultado, iv.Length, textoCifrado.Length);

            return resultado;
        }

        private string DesencriptarContenido(byte[] contenidoCifrado, string claveUsuario, byte[] salt)
        {
            byte[] clave = ObtenerClaveBytes(claveUsuario, salt);

            //extraer el IV (primeros 12 bytes)
            byte[] iv = new byte[12];
            Array.Copy(contenidoCifrado, 0, iv, 0, iv.Length);

            //extraer el texto cifrado y el tag
            int textoCifradoLength = contenidoCifrado.Length - iv.Length;
            byte[] textoCifrado = new byte[textoCifradoLength];
            Array.Copy(contenidoCifrado, iv.Length, textoCifrado, 0, textoCifradoLength);

            //configurar el cifrador
            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(clave), 128, iv, null);
            cipher.Init(false, parameters);

            //desencriptar el texto cifrado
            byte[] textoPlano = new byte[cipher.GetOutputSize(textoCifrado.Length)];
            int len = cipher.ProcessBytes(textoCifrado, 0, textoCifrado.Length, textoPlano, 0);
            len += cipher.DoFinal(textoPlano, len);

            return Encoding.UTF8.GetString(textoPlano, 0, len);
        }

        private byte[] ObtenerClaveBytes(string claveUsuario, byte[] salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(claveUsuario, salt, 10000))
            {
                return deriveBytes.GetBytes(32); // 256 bits para AES-256
            }
        }
        public List<NotaDTO> ObtenerNotasConContenido(int usuarioId, string claveUsuario, byte[] salt)
        {
            List<NotaDTO> notas = new List<NotaDTO>();
            using (SqlDataReader reader = accesoDatos.ObtenerNotasConContenido(usuarioId))
            {
                while (reader.Read())
                {
                    NotaDTO nota = new NotaDTO
                    {
                        Nota_ID = Convert.ToInt32(reader["Nota_ID"]),
                        Titulo = reader["Titulo"].ToString(),
                        Fecha_creacion = Convert.ToDateTime(reader["Fecha_creacion"]),
                        Contenido = DesencriptarContenido((byte[])reader["Contenido"], claveUsuario, salt)
                    };
                    notas.Add(nota);
                }
                reader.Close();
            }
            return notas;
        }

        public string ObtenerTituloNota(int notaId, int usuarioId)
        {
            using (SqlDataReader reader = accesoDatos.ObtenerTituloNotaPorId(notaId, usuarioId))
            {
                if (reader.Read())
                {
                    return reader["Titulo"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public void ModificarNota(int notaId, int usuarioId, string nuevoTitulo, string nuevoContenido, string claveUsuario, byte[] salt)
        {
            byte[] contenidoCifrado = EncriptarContenido(nuevoContenido, claveUsuario, salt);
            accesoDatos.ActualizarNota(notaId, usuarioId, nuevoTitulo, contenidoCifrado);
        }
        public NotaDTO ObtenerNotaPorId(int notaId, int usuarioId, string claveUsuario, byte[] salt)
        {
            NotaDTO nota = null;
            using (var reader = accesoDatos.ObtenerNotaPorId(notaId, usuarioId))
            {
                if (reader.Read())
                {
                    string titulo = reader["Titulo"].ToString();
                    byte[] contenidoCifrado = (byte[])reader["Contenido"];
                    string contenidoDesencriptado = DesencriptarContenido(contenidoCifrado, claveUsuario, salt);

                    nota = new NotaDTO
                    {
                        Nota_ID = notaId,
                        Titulo = titulo,
                        Contenido = contenidoDesencriptado  //asignar el contenido desencriptado
                    };
                }
                reader.Close();
            }
            return nota;
        }

    }

    //DTO para notas
    public class NotaDTO
    {
        public int Nota_ID { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public string Contenido { get; set; }
    }

    public class NotaExportarDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}
