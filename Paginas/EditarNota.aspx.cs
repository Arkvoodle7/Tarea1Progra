using System;
using Negocios;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarea1Progra.Paginas
{
    public partial class ModificarNotas : System.Web.UI.Page
    {
        private NegociosNota negociosNota = new NegociosNota();
        private int notaId;
        private int usuarioId;
        private byte[] salt;
        private string claveUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["notaId"], out notaId))
                {
                    CargarNota();
                }
                else
                {
                    lblMensaje.Text = "ID de nota no válido.";
                    lblMensaje.Visible = true;
                }
            }
        }
        private void CargarNota()
        {
            var nota = negociosNota.ObtenerNotaPorId(notaId, usuarioId, claveUsuario, salt);

            if (nota != null)
            {
                txtTitulo.Text = nota.Titulo;
                txtContenido.Text = nota.Contenido;
            }
            else
            {
                lblMensaje.Text = "No se pudo cargar la nota.";
                lblMensaje.Visible = true;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nuevoTitulo = txtTitulo.Text;
            string nuevoContenido = txtContenido.Text;

            try
            {
                negociosNota.ModificarNota(notaId, usuarioId, nuevoTitulo, nuevoContenido, claveUsuario, salt);
                lblMensaje.Text = "Nota modificada con éxito.";
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al modificar la nota: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

            // Redirigir a la pantalla descrita en la HU 3
            Response.Redirect("Dashboard.aspx");  // Reemplazar con la página correspondiente
        }
    }

}