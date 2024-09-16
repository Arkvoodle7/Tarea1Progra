using System;
using Negocios;

namespace Tarea1Progra.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            NegociosUsuario negociosUsuario = new NegociosUsuario();
            byte[] salt;

            int usuarioId = negociosUsuario.IniciarSesion(correo, contrasena, out salt);

            if (usuarioId != -1)
            {
                // Credenciales correctas
                Session["Usuario_ID"] = usuarioId;
                Session["ClaveUsuario"] = contrasena; // La contraseña se usará para derivar la clave
                Session["SaltUsuario"] = salt;
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                lblMensaje.Text = "Usuario y/o contraseña incorrectos.";
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuarios.aspx");
        }
    }
}