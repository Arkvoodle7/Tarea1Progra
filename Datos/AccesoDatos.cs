using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos
{
    public class AccesoDatos
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KeepNotesDB"].ConnectionString;

        // Método genérico para ejecutar comandos SQL con SqlDataReader
        private SqlDataReader EjecutarConsulta(string query, Action<SqlCommand> agregarParametros)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            agregarParametros(cmd);

            conn.Open();
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        // Método genérico para ejecutar comandos SQL que no devuelven resultados
        private void EjecutarComando(string query, Action<SqlCommand> agregarParametros)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                agregarParametros(cmd);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Métodos para usuarios
        public SqlDataReader ObtenerUsuarioPorCorreo(string correo)
        {
            string query = "SELECT Usuario_ID, Contrasena, Salt FROM Usuarios WHERE Correo = @Correo";
            return EjecutarConsulta(query, cmd => cmd.Parameters.AddWithValue("@Correo", correo));
        }

        public void InsertarUsuario(string correo, byte[] contrasenaHash, byte[] salt)
        {
            string query = "INSERT INTO Usuarios (Correo, Contrasena, Salt) VALUES (@Correo, @Contrasena, @Salt)";
            EjecutarComando(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contrasena", contrasenaHash);
                cmd.Parameters.AddWithValue("@Salt", salt);
            });
        }

        // Métodos para notas
        public SqlDataReader ObtenerNotasPorUsuario(int usuarioId)
        {
            string query = "SELECT Nota_ID, Titulo, Fecha_creacion FROM Notas WHERE Usuario_ID = @Usuario_ID";
            return EjecutarConsulta(query, cmd => cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId));
        }

        public byte[] ObtenerContenidoNota(int notaId, int usuarioId)
        {
            string query = "SELECT Contenido FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
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
            string query = "INSERT INTO Notas (Usuario_ID, Titulo, Contenido) VALUES (@Usuario_ID, @Titulo, @Contenido)";
            EjecutarComando(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
                cmd.Parameters.AddWithValue("@Titulo", titulo);
                cmd.Parameters.AddWithValue("@Contenido", contenidoCifrado);
            });
        }

        public void EliminarNota(int notaId, int usuarioId)
        {
            string query = "DELETE FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            EjecutarComando(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
            });
        }

        public SqlDataReader ObtenerNotaPorId(int notaId, int usuarioId)
        {
            string query = "SELECT Titulo, Contenido FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            return EjecutarConsulta(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
            });
        }

        public SqlDataReader ObtenerNotasConContenido(int usuarioId)
        {
            string query = "SELECT Nota_ID, Titulo, Contenido, Fecha_creacion FROM Notas WHERE Usuario_ID = @Usuario_ID";
            return EjecutarConsulta(query, cmd => cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId));
        }

        public SqlDataReader ObtenerTituloNotaPorId(int notaId, int usuarioId)
        {
            string query = "SELECT Titulo FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            return EjecutarConsulta(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
            });
        }

        public void ActualizarNota(int notaId, int usuarioId, string nuevoTitulo, byte[] contenidoCifrado)
        {
            string query = "UPDATE Notas SET Titulo = @Titulo, Contenido = @Contenido WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
            EjecutarComando(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Titulo", nuevoTitulo);
                cmd.Parameters.AddWithValue("@Contenido", contenidoCifrado);
                cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
            });
        }
    }
}
