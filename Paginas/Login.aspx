<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tarea1Progra.Paginas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Login</title>
    <link href="../Estilos/EstiloLogReg.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <!-- Seccion principal del login -->
        <div class="sidenav"> 
            <div class="login-main-text">
                <h2>KeepNotes</h2>
                <p>Inicio de sesión y registro de usuarios</p>
            </div>
        </div>
        <div class="main">
                <div class="iniciarS-form">
                    <asp:Panel ID="LoginPanel" runat="server" CssClass="panel">
                        <div class="form-group">
                            <asp:Label ID="lblCorreo" runat="server" Text="Usuario (Correo Electrónico)"></asp:Label>            
                            <p><asp:TextBox ID="txtCorreo" CssClass="form-control" runat="server" placeholder="Correo Electrónico"></asp:TextBox></p>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contrasena"></asp:Label> 
                            <p><asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contrasena"></asp:TextBox></p>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnIniciarSesion" CssClass="btn btn-black" runat="server" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
                            <asp:Button ID="btnRegistrar" CssClass="btn btn-2" runat="server" Text="Registrar Usuario" OnClick="btnRegistrar_Click" />
                        </div>
                        <!-- Mensajes -->
                        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
                    </asp:Panel>
                </div>
            </div>
    </form>
</body>
</html>
