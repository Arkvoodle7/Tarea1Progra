<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgregarNotas.aspx.cs" Inherits="Tarea1Progra.Paginas.AgregarNota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agregar Nota</title>
    <link href="../Estilos/EstiloAgrModNotas.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h1>Agregar Nueva Nota</h1>
            <div class="form-group">
                <asp:Label ID="lblTitulo" runat="server" Text="Título" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="input-text"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblContenido" runat="server" Text="Contenido" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtContenido" runat="server" TextMode="MultiLine" CssClass="input-textarea"></asp:TextBox>
            </div>
            <div class="form-group btn-group">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />
            </div>
            <!-- Mensajes -->
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
        </div>

        <!-- Panel Modal para el mensaje -->
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
