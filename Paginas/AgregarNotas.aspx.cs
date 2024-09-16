using System;
using Negocios;

namespace Tarea1Progra.Paginas
{
    public partial class AgregarNota : System.Web.UI.Page
    {
        private int usuarioId;
        private NegociosNota negociosNota = new NegociosNota();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_ID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                usuarioId = Convert.ToInt32(Session["Usuario_ID"]);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string contenido = txtContenido.Text.Trim();
            string claveUsuario = Session["ClaveUsuario"] as string;
            byte[] salt = Session["SaltUsuario"] as byte[];

            if (!string.IsNullOrEmpty(titulo) && !string.IsNullOrEmpty(contenido))
            {
                negociosNota.AgregarNota(usuarioId, titulo, contenido, claveUsuario, salt);

                lblMensajeModal.Text = "Nota almacenada con éxito";
                pnlMensaje.Visible = true;
                // Ocultar el formulario
                txtTitulo.Enabled = false;
                txtContenido.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            else
            {
                lblMensaje.Text = "Debe ingresar un título y contenido para la nota";
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}
