<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Tarea1Progra.Paginas.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KeepNotes Dashboard</title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../Estilos/EstiloDashboard.css" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="../Scripts/dashboard.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row my-3">
                <h2>Mis Notas</h2>
            </div>
            <div class="row mb-3">
                <asp:Button ID="btnAgregarNota" runat="server" Text="Agregar Nota" OnClick="btnAgregarNotas_Click" CssClass="btn btn-primary mr-2" />
                <asp:Button ID="btnDuplicarNota" runat="server" Text="Duplicar" OnClick="btnDuplicarNota_Click" CssClass="btn btn-secondary mr-2" />
                <asp:Button ID="btnExportarNotas" runat="server" Text="Exportar" OnClick="btnExportarNotas_Click" CssClass="btn btn-secondary" />
            </div>
            <div class="row">
                <asp:Repeater ID="rptNotas" runat="server" OnItemCommand="rptNotas_ItemCommand">
                    <ItemTemplate>
                        <div class="col-sm-3">
                            <div class="sticker bg-warning">
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="sticker-checkbox" />
                                <asp:HiddenField ID="hdnNotaId" runat="server" Value='<%# Eval("Nota_ID") %>' />
                                <div class="sticker-content">
                                    <h5><%# Eval("Titulo") %></h5>
                                </div>
                                <div class="sticker-actions">
                                    <asp:LinkButton ID="lnkVer" runat="server" CommandName="Ver" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Ver</asp:LinkButton>
                                    <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Editar</asp:LinkButton>
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Nota_ID") %>' CssClass="action-link">Eliminar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlVerNota" runat="server" CssClass="modal" Visible="false">
                    <div class="modal-content">
                        <span class="close" onclick="$('#<%= pnlVerNota.ClientID %>').hide();">&times;</span>
                        <h3><asp:Label ID="lblTituloNota" runat="server"></asp:Label></h3>
                        <p><asp:Label ID="lblContenidoNota" runat="server"></asp:Label></p>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <!-- Dialogo de confirmación para eliminar -->
        <asp:Panel ID="pnlConfirmacion" runat="server" CssClass="modal" Visible="false">
            <div class="modal-content">
                <span class="close" onclick="$('#<%= pnlConfirmacion.ClientID %>').hide();">&times;</span>
                <p>¿Realmente desea eliminar la nota seleccionada?</p>
                <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Sí" OnClick="btnConfirmarEliminar_Click" CssClass="btn btn-danger" />
                <asp:Button ID="btnCancelarEliminar" runat="server" Text="No" OnClick="btnCancelarEliminar_Click" CssClass="btn btn-secondary" />
            </div>
        </asp:Panel>

        <!-- Mensajes -->
        <div class="text-center my-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
        </div>
    </form>
</body>
</html>
