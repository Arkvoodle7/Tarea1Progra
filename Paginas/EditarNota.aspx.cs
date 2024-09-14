using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarea1Progra.Paginas
{
    public partial class EditarNota : System.Web.UI.Page
    {
        private NegociosNota negociosNota = new NegociosNota();
        private int notaId;
        private int usuarioId;
        private string claveUsuario;
        private byte[] salt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_ID"] == null || !int.TryParse(Request.QueryString["notaId"], out notaId))
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            usuarioId = Convert.ToInt32(Session["Usuario_ID"]);
            claveUsuario = Session["ClaveUsuario"] as string;
            salt = Session["SaltUsuario"] as byte[];

            if (!IsPostBack)
            {
                CargarNota();
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
                lblMensaje.Text = "ID de nota no válido.";
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
            Response.Redirect("Dashboard.aspx");
        }
    }
}