<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="Tarea1Progra.Paginas.RegistroUsuarios1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Registro de usuarios</title>
    <link href="../Estilos/EstiloLogRed.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="iniciarS-form">
    <asp:Panel class="panel" ID="LoginPanel" runat="server">
        <h1> Registro </h1>
        <div class="form-group">
            <asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label>            
            <p><asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" placeholder="Usuario"></asp:TextBox></p>
        </div>
        <div class="form-group">
            <asp:Label ID="lblContraseña" runat="server" Text="Contraseña"></asp:Label> 
            <p><asp:TextBox ID="txtContraseña" CssClass="form-control" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox></p>
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
            <asp:Button ID="btnIniciarSesion" CssClass="btn btn-Ini" runat="server" Text="Iniciar Sesion" OnClick="btnRegistrar_Click" />
        </div>
    </asp:Panel>
</div>
    </form>
</body>
</html>
