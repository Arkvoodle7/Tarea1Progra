<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarNota.aspx.cs" Inherits="Tarea1Progra.Paginas.EditarNota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar Nota</title>
    <link href="../Estilos/EstiloModNotas.css" rel="stylesheet" />
</head>
<body>
    <form id="formEditarNota" runat="server">
        <div class="form-container">
            <h2>Editar Nota</h2>
            <div class="form-group">
                <label for="txtTitulo">Título:</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="input-text" MaxLength="255"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtContenido">Contenido:</label>
                <asp:TextBox ID="txtContenido" runat="server" TextMode="MultiLine" Rows="10" CssClass="input-text"></asp:TextBox>
            </div>
            <div class="form-group btn-group">
               <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" CssClass="btn" />
               <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn" />
            </div>
            <asp:Label ID="lblMensaje" runat="server" CssClass="success-message" Visible="False"></asp:Label>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" CssClass="modal" Visible="false">
            <div class="modal-content">
                <asp:Label ID="lblMensajeModal" runat="server" Text=""></asp:Label>
                <br /><br />
                <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" CssClass="btn btn-primary" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
