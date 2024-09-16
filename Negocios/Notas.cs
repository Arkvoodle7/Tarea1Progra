using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocios
{
    public class Notas
    {
        
        public bool DuplicarNota(int notaId, int usuarioId)
        {
            using (SqlConnection conexion_db = Class1.ObtenerConexion()) // TODO: Matchear con la cadena de conexion
            {
                conexion_db.Open();
                // Obtener la nota original
                string querySelect = "SELECT * FROM Notas WHERE Nota_ID = @Nota_ID AND Usuario_ID = @Usuario_ID";
                Nota notaOriginal = null;

                using (SqlCommand cmd = new SqlCommand(querySelect, conexion_db))
                {
                    cmd.Parameters.AddWithValue("@Nota_ID", notaId);
                    cmd.Parameters.AddWithValue("@Usuario_ID", usuarioId);
            
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notaOriginal = new Nota
                            {
                                Nota_ID = Convert.ToInt32(reader["Nota_ID"]),
                                Usuario_ID = Convert.ToInt32(reader["Usuario_ID"]),
                                Titulo = "Copia de " + reader["Titulo"].ToString(),
                                Contenido = (byte[])reader["Contenido"],
                                Fecha_creacion = DateTime.Now,
                                Fecha_modificacion = null
                            };
                        }
                    }
                }

                // Insertar la nota duplicada 
                if (notaOriginal != null)
                {
                    string queryInsert = "INSERT INTO Notas (Usuario_ID, Titulo, Contenido, Fecha_creacion) VALUES (@Usuario_ID, @Titulo, @Contenido, @Fecha_creacion)";
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexion_db))
                    {
                        cmdInsert.Parameters.AddWithValue("@Usuario_ID", notaOriginal.Usuario_ID);
                        cmdInsert.Parameters.AddWithValue("@Titulo", notaOriginal.Titulo);
                        cmdInsert.Parameters.AddWithValue("@Contenido", notaOriginal.Contenido);
                        cmdInsert.Parameters.AddWithValue("@Fecha_creacion", notaOriginal.Fecha_creacion);

                        return cmdInsert.ExecuteNonQuery() > 0;
                    }
                }
            }
            return false;
        }
    }
}
