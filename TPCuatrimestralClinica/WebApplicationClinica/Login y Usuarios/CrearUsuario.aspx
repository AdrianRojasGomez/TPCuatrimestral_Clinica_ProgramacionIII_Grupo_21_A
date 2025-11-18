<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master"
    AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true"
    CodeBehind="CrearUsuario.aspx.cs"
    Inherits="WebApplicationClinica.CrearUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hfIdUsuario" runat="server" />

    <div class="container py-4">
        
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-1 text-success fw-bold">Gestión de Usuarios</h2>
            <asp:Button ID="Button1" runat="server" 
                Text="↩️ Volver a Médicos"
                CssClass="btn btn-secondary" OnClick="btnVolver_Click" /> 
        </div>

        <div class="card shadow-sm p-4 mb-4">
            <h4 class="card-title mb-3">Datos del Usuario</h4>
            
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="TxtNombreUsuario" class="form-label">Nombre de usuario</label>
                    <asp:TextBox ID="TxtNombreUsuario" runat="server" CssClass="form-control" placeholder="Ej: mgomez" />
                </div>

                <div class="col-md-6 mb-3">
                    <label for="TxtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" placeholder="********" />
                </div>
            </div>

            <div class="mb-3">
                <label for="ddlTipoUsuario" class="form-label">Tipo de usuario (Rol)</label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione tipo..." Value="" />
                    <asp:ListItem Text="Administrador" Value="1" />
                    <asp:ListItem Text="Médico" Value="2" />
                    <asp:ListItem Text="Recepcionista" Value="3" />
                </asp:DropDownList>
            </div>

            <div class="d-flex align-items-center mt-2">
                <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar Usuario" CssClass="btn btn-primary me-3" OnClick="btnGuardarUsuario_Click" />
                
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar / Cancelar" CssClass="btn btn-outline-secondary me-3" OnClick="btnLimpiar_Click" />
                
                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-semibold ms-2"></asp:Label>
            </div>
        </div>

        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <strong>Buscar Usuario</strong>
            </div>
            <div class="card-body">
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFiltradoUsario" runat="server" CssClass="form-control" placeholder="Escriba para buscar..."
                            OnTextChanged="txtFiltradoUsario_TextChanged" AutoPostBack="true" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card shadow-sm">
            <div class="card-header bg-light">
                <strong class="mb-1 text-success fw-bold">Listado de Usuarios</strong>
            </div>

            <div class="card-body p-0">
                <asp:GridView ID="gvUsuario" runat="server" EmptyDataText="No hay usuarios registrados."
                    CssClass="table table-hover mb-0 align-middle"
                    HeaderStyle-CssClass="table-light" AutoGenerateColumns="false" 
                    DataKeyNames="IdUsuario"
                    OnRowCommand="gvUsuario_RowCommand" 
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvUsuario_PageIndexChanging"
                    OnRowDataBound="gvUsuario_RowDataBound">

                    <Columns>
                        <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                        
                        <asp:BoundField DataField="TipoUsuario" HeaderText="Rol" />

                        <asp:TemplateField HeaderText="Médico Asignado">
                            <ItemTemplate>
                                <%# Eval("Medico") != null ? ((Dominio.Medico)Eval("Medico")).Apellido + ", " + ((Dominio.Medico)Eval("Medico")).Nombre : "-" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# (bool)Eval("Activo") ? 
                                    "<span class='badge bg-success'>Activo</span>" : 
                                    "<span class='badge bg-danger'>Inactivo</span>" %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="300px">
                            <ItemTemplate>
                                
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                    CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn btn-sm btn-warning mx-1" ToolTip="Modificar datos">
                                    <i class="bi bi-pencil-square"></i> Editar
                                </asp:LinkButton>

                                <asp:Button ID="btnActivar" runat="server" Text="✅ Activar" CssClass="btn btn-sm btn-outline-success mx-1"
                                    OnClick="btnActivarUsuario_Click" Visible='<%# !(bool)Eval("Activo") %>' 
                                    CommandArgument='<%# Eval("IdUsuario") %>' />
                                
                                <asp:Button ID="btnInactivar" runat="server" Text="🗑️ Baja" CssClass="btn btn-sm btn-outline-danger mx-1"
                                    OnClick="btnGuardarInactivacion_Click" Visible='<%# (bool)Eval("Activo") %>' 
                                    OnClientClick="return confirm('¿Está seguro de que desea dar de baja a este usuario?');"/>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var __filterTimer = null;
        function liveFilter(uniqueId) {
            if (__filterTimer) clearTimeout(__filterTimer);
            __filterTimer = setTimeout(function () {
                __doPostBack(uniqueId, '');
            }, 300);
        }
    </script>

</asp:Content>