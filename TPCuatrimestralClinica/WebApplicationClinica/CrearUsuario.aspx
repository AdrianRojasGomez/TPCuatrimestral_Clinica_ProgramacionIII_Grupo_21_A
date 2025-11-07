<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="WebApplicationClinica.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container py-4">
        <h2 class="mb-4">Crear nuevo usuario</h2>

        <div class="card shadow-sm p-4">
            <div class="mb-3">
                <label for="TxtNombreUsuario" class="form-label">Nombre de usuario</label>
                <asp:TextBox ID="TxtNombreUsuario" runat="server" CssClass="form-control" placeholder="Ej: mgomez" />
            </div>

            <div class="mb-3">
                <label for="TxtPassword" class="form-label">Contraseña</label>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="********" />
            </div>

            <div class="mb-3">
                <label for="ddlTipoUsuario" class="form-label">Tipo de usuario</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione tipo..." Value="" />
                    <asp:ListItem Text="Administrador" Value="1" />
                    <asp:ListItem Text="Médico" Value="2" />
                    <asp:ListItem Text="Recepcionista" Value="3" />
                </asp:DropDownList>
            </div>

            <div class="d-flex align-items-center">
               
                <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar Usuario" CssClass="btn btn-primary me-3" OnClick="btnGuardarUsuario_Click" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-semibold"></asp:Label>
                
            </div>
        </div>
    </div>

</asp:Content>
