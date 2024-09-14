<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Tarea1Progra.Paginas.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KeepNotes Dashboard</title>
    <link rel="stylesheet" type="text/css" href="../Estilos/Styles.css" />
    <script src="../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/dashboard.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h1>Mis Notas</h1>
            <asp:Button ID="btnAgregarNota" runat="server" Text="Agregar Nota" OnClick="btnAgregarNotas_Click" CssClass="btn" />
            <asp:Button ID="btnDuplicarNota" runat="server" Text="Duplicar" OnClick="btnDuplicarNota_Click" CssClass="btn" />
            <asp:Button ID="btnExportarNotas" runat="server" Text="Exportar" OnClick="btnExportarNotas_Click" CssClass="btn" />
            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                OnRowCommand="gvNotas_RowCommand" DataKeyNames="Nota_ID">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Fecha_creacion" HeaderText="Fecha de Guardado" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVer" runat="server" CommandName="Ver" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Ver</asp:LinkButton>
                           <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Editar</asp:LinkButton>
                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <!-- Dialogo de confirmación para eliminar -->
        <asp:Panel ID="pnlConfirmacion" runat="server" CssClass="modal" Visible="false">
            <div class="modal-content">
                <span class="close" onclick="$('#<%= pnlConfirmacion.ClientID %>').hide();">&times;</span>
                <p>¿Realmente desea eliminar la nota seleccionada?</p>
                <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Si" OnClick="btnConfirmarEliminar_Click" CssClass="btn" />
                <asp:Button ID="btnCancelarEliminar" runat="server" Text="No" OnClick="btnCancelarEliminar_Click" CssClass="btn" />
            </div>
        </asp:Panel>

        <!-- Mensajes -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
    </form>
</body>
</html>
