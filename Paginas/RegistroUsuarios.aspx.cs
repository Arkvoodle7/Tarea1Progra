using System;
using Negocios;

namespace Tarea1Progra.Paginas
{
    public partial class RegistroUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            NegociosUsuario negociosUsuario = new NegociosUsuario();
            string mensaje;

            bool registrado = negociosUsuario.RegistrarUsuario(correo, contrasena, out mensaje);

            if (registrado)
            {
                lblMensajeModal.Text = "Se ha creado el usuario.";
                pnlMensaje.Visible = true;
                //RegistroPanel.Visible = false; // Ocultamos el panel de registro
            }
            else
            {
                lblMensaje.Text = mensaje;
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
