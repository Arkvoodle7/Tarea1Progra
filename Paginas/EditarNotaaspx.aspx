<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarNotaaspx.aspx.cs" Inherits="Tarea1Progra.Paginas.EditarNotaaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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

            <div class="form-buttons">
               <asp:Button ID="BtnGuardar" runat="server" Text="Guardar Cambios" OnClick="BtnGuardar_Click" CssClass="btn" />
               <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="btn" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="success-message" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>