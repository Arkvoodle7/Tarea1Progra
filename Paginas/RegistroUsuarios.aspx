<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="Tarea1Progra.Paginas.RegistroUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro de usuarios</title>
    <link href="../Estilos/EstiloLogReg.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="iniciarS-form">
            <asp:Panel ID="RegistroPanel" runat="server" CssClass="panel">
                <h1> Registro </h1>
                <div class="form-group">
                    <asp:Label ID="lblCorreo" runat="server" Text="Usuario (Correo Electrónico)"></asp:Label>            
                    <p><asp:TextBox ID="txtCorreo" CssClass="form-control" runat="server" placeholder="Correo Electrónico"></asp:TextBox></p>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label> 
                    <p><asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox></p>
                </div>
                <div class="btn">
                    <asp:Button ID="btnRegistrar" CssClass="btn btn-Regis" runat="server" Text="Registrar Usuario" OnClick="btnRegistrar_Click" />
                </div>
                <div class="from-group">
                    <div class="login-or">
                        <hr class="hr-or">
                    </div>
                </div>
                <div class="btn">
                    <asp:Button ID="btnRegresar" CssClass="btn btn-Ini" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                </div>
                <!-- Mensajes -->
                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
            </asp:Panel>

            <!-- Panel Modal para el mensaje -->
            <asp:Panel ID="pnlMensaje" runat="server" CssClass="modal" Visible="false">
                <div class="modal-content">
                    <asp:Label ID="lblMensajeModal" runat="server" Text=""></asp:Label>
                    <br /><br />
                    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" CssClass="btn" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

