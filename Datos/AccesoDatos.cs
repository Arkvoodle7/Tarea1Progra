using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos
{
    public class AccesoDatos
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KeepNotesDB"].ConnectionString;

        // Métodos para usuarios
        public SqlDataReader ObtenerUsuarioPorCorreo(string correo)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT Usuario_ID, Contrasena, Salt FROM Usuarios WHERE Correo = @Correo";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Correo", correo);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        public void InsertarUsuario(string correo, byte[] contrasenaHash, byte[] salt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuarios (Correo, Contrasena, Salt) VALUES (@Correo, @Contrasena, @Salt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contrasena", contrasenaHash);
                cmd.Parameters.AddWithValue("@Salt", salt);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Métodos para notas
        public SqlDataReader ObtenerNotasPorUsuario(int usuarioId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT Nota_ID, Titulo, Fecha_creacion FROM Notas WHERE Usuario_ID = @Usuario_ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        public byte[] ObtenerContenidoNota(int notaId, int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Contenido FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

                conn.Open();
                byte[] contenidoCifrado = cmd.ExecuteScalar() as byte[];
                conn.Close();

                return contenidoCifrado;
            }
        }

        public void InsertarNota(int usuarioId, string titulo, byte[] contenidoCifrado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Notas (Usuario_ID, Titulo, Contenido) VALUES (@Usuario_ID, @Titulo, @Contenido)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
                cmd.Parameters.AddWithValue("@Titulo", titulo);
                cmd.Parameters.AddWithValue("@Contenido", contenidoCifrado);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void EliminarNota(int notaId, int usuarioId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public SqlDataReader ObtenerNotaPorId(int notaId, int usuarioId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT Titulo, Contenido FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nota_ID", notaId);
            cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }
        public SqlDataReader ObtenerNotasConContenido(int usuarioId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT Nota_ID, Titulo, Contenido, Fecha_creacion FROM Notas WHERE Usuario_ID = @Usuario_ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        public SqlDataReader ObtenerTituloNotaPorId(int notaId, int usuarioId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT Titulo FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nota_ID", notaId);
            cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }
        public void ActualizarNota(int notaId, int usuarioId, string nuevoTitulo, byte[] contenidoCifrado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Notas SET Titulo = @Titulo, Contenido = @Contenido WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Titulo", nuevoTitulo);
                cmd.Parameters.AddWithValue("@Contenido", contenidoCifrado);
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}