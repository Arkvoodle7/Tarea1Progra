using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Negocios;

namespace Tarea1Progra.Paginas
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private int usuarioId;
        private NegociosNota negociosNota = new NegociosNota();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificar si el usuario ha iniciado sesión
            if (Session["Usuario_ID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                usuarioId = Convert.ToInt32(Session["Usuario_ID"]);
                if (!IsPostBack)
                {
                    CargarNotas();
                }
            }
        }

        private void CargarNotas()
        {
            string claveUsuario = Session["ClaveUsuario"] as string;
            byte[] salt = Session["SaltUsuario"] as byte[];

            List<NotaDTO> notas = negociosNota.ObtenerNotasConContenido(usuarioId, claveUsuario, salt);
            rptNotas.DataSource = notas;
            rptNotas.DataBind();
        }

        protected void btnAgregarNotas_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarNotas.aspx");
        }

        protected void rptNotas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //cerrar el modal de ver nota si está visible
            pnlVerNota.Visible = false;
            lblTituloNota.Text = string.Empty;
            lblContenidoNota.Text = string.Empty;

            int notaId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Ver")
            {
                MostrarContenidoNota(notaId);
            }
            else if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarNota.aspx?notaId={notaId}");
            }

            else if (e.CommandName == "Eliminar")
            {
                ViewState["NotaSeleccionadaID"] = notaId;
                pnlConfirmacion.Visible = true;
            }
        }

        private void MostrarContenidoNota(int notaId)
        {
            string claveUsuario = Session["ClaveUsuario"] as string;
            byte[] salt = Session["SaltUsuario"] as byte[];

            string contenidoDesencriptado = negociosNota.MostrarContenidoNota(notaId, usuarioId, claveUsuario, salt);

            if (contenidoDesencriptado != null)
            {
                // Obtener el título de la nota
                string tituloNota = negociosNota.ObtenerTituloNota(notaId, usuarioId);

                lblTituloNota.Text = tituloNota;
                lblContenidoNota.Text = contenidoDesencriptado;
                pnlVerNota.Visible = true;
            }
            else
            {
                lblMensaje.Text = "No se pudo obtener el contenido de la nota.";
            }
        }

        protected void btnDuplicarNota_Click(object sender, EventArgs e)
        {
            List<int> notasSeleccionadas = ObtenerNotasSeleccionadas();
            if (notasSeleccionadas.Count == 1)
            {
                int notaId = notasSeleccionadas[0];
                string mensaje;
                bool duplicada = negociosNota.DuplicarNota(notaId, usuarioId, out mensaje);

                lblMensaje.Text = mensaje;

                if (duplicada)
                {
                    CargarNotas();
                }
            }
            else if (notasSeleccionadas.Count == 0)
            {
                lblMensaje.Text = "Por favor seleccione una nota";
            }
            else
            {
                lblMensaje.Text = "Seleccione solo una nota";
            }
        }

        protected void btnExportarNotas_Click(object sender, EventArgs e)
        {
            List<int> notasSeleccionadas = ObtenerNotasSeleccionadas();
            if (notasSeleccionadas.Count > 0)
            {
                string claveUsuario = Session["ClaveUsuario"] as string;
                byte[] salt = Session["SaltUsuario"] as byte[];

                List<NotaExportarDTO> notasParaExportar = negociosNota.ObtenerNotasParaExportar(notasSeleccionadas, usuarioId, claveUsuario, salt);

                StringBuilder sb = new StringBuilder();

                foreach (var nota in notasParaExportar)
                {
                    sb.AppendLine($"Título: {nota.Titulo}");
                    sb.AppendLine($"Contenido: {nota.Contenido}");
                    sb.AppendLine("---------------");
                }

                // Descargar como archivo de texto
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment; filename=NotasExportadas.txt");
                Response.Write(sb.ToString());
                Response.End();
            }
            else
            {
                lblMensaje.Text = "Por favor, seleccione al menos una nota";
            }
        }

        private List<int> ObtenerNotasSeleccionadas()
        {
            List<int> seleccionadas = new List<int>();
            foreach (RepeaterItem item in rptNotas.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkSelect");
                HiddenField hdnNotaId = (HiddenField)item.FindControl("hdnNotaId");
                if (chk != null && hdnNotaId != null && chk.Checked)
                {
                    int notaId = Convert.ToInt32(hdnNotaId.Value);
                    seleccionadas.Add(notaId);
                }
            }
            return seleccionadas;
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int notaId = Convert.ToInt32(ViewState["NotaSeleccionadaID"]);

            //eliminar la nota
            negociosNota.EliminarNota(notaId, usuarioId);

            //ocultar cualquier modal que esté abierto, en este caso, el de ver nota
            pnlVerNota.Visible = false;
            lblTituloNota.Text = string.Empty;
            lblContenidoNota.Text = string.Empty;

            //actualizar la interfaz
            pnlConfirmacion.Visible = false;
            lblMensaje.Text = "Nota eliminada con éxito";
            CargarNotas();
        }

        protected void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            pnlConfirmacion.Visible = false;
        }

    }
}
