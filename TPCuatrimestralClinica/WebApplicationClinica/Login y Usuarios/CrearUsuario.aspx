<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master"
    AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true"
    CodeBehind="CrearUsuario.aspx.cs"
    Inherits="WebApplicationClinica.WebForm2" %>


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
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Campos" CssClass="btn btn-primary me-3" OnClick="btnLimpiar_Click" />

            </div>

        </div>
    </div>
    <div class="card shadow-sm mb-4">
        <div class="card-header bg-primary text-white">
            <strong>Buscar Usuario</strong>

        </div>

        <div class="card-body">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <asp:TextBox ID="txtFiltradoUsario" runat="server" CssClass="form-control rounded-end" placeholder="Buscar por Usuario"
                        OnTextChanged="txtFiltradoUsario_TextChanged" AutoPostBack="true" />
                    <asp:Label ID="lblInactivoCorecto" runat="server" CssClass="alert alert-danger fw-semibold rounded-pill px-4 py-2 shadow-sm d-inline-block mt-3" Text=" 🛑 Usuario Inactivo" />
                    <asp:Label ID="lblActivoCorrectamente" runat="server" CssClass="alert alert-success fw-semibold rounded-pill px-4 py-2 shadow-sm d-inline-block mt-3" Text="✅ Usuario Activo" />

                    <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-outline-primary rounded-pill px-4 py-2 fw-semibold shadow-sm mt-3" Text="↩️ Volver" OnClick="btnVolver_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="card shadow-sm">
        <div class="card-header bg-light">
            <strong>Resultados de la búsqueda</strong>
        </div>

        <div class="card-body p-0">

            <asp:GridView ID="gvUsuario" runat="server" EmptyDataText="No hay datos"
                CssClass="table table-hover mb-0 align-middle"
                HeaderStyle-CssClass="table-light" AutoGenerateColumns="false" DataKeyNames="IdUsuario"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvUsuario_PageIndexChanging"
                OnRowEditing="gvUsuario_RowEditing" OnRowDataBound="gvUsuario_RowDataBound">



                <Columns>

                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />


                    <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo" />


                    <asp:TemplateField HeaderText="Médico">
                        <ItemTemplate>
                            <%# Eval("Medico") != null 
                        ? ((Dominio.Medico)Eval("Medico")).Nombre + " " + ((Dominio.Medico)Eval("Medico")).Apellido 
                        : "-" %>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# (bool)Eval("Activo") ? "Activo" : "Inactivo" %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnActivar" runat="server" Text="✅ Activar" CssClass="btn btn-sm btn-outline-success mx-1"
                                OnClick="btnActivar_Click1"
                                Visible='<%# !(bool)Eval("Activo") %>'
                                CommandArgument='<%# Eval("IdUsuario") %>' />

                            <asp:Button ID="btnActivarUsuario" runat="server" Text="💾 Guardar Cambios" CssClass="btn btn-sm btn-outline-primary mx-1"
                                OnClick="btnActivarUsuario_Click" CommandName="Update" />


                            <asp:Button ID="btnCancelar" runat="server" Text="↩️ Cancelar" CssClass="btn btn-sm btn-outline-primary mx-1"
                                OnClick="btnCancelar_Click" Visible="false" />



                            <asp:Button ID="btnInactivar" runat="server" Text="🗑️ Inactivar" OnClick="btnInactivar_Click"
                                Visible='<%# (bool)Eval("Activo") %>' CommandArgument='<%# Eval("IdUsuario") %>' />

                            <asp:Button ID="btnGuardarInactivacion" runat="server" Text="💾 Guardar Cambios"
                                CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnGuardarInactivacion_Click"
                                CommandName="Update" />

                            <asp:Button ID="btnCncelar2" runat="server" CssClass="btn btn-sm btn-outline-primary mx-1"
                                Text="↩️ Cancelar" OnClick="btnCncelar2_Click" Visible="false" />




                        </ItemTemplate>
                    </asp:TemplateField>




                </Columns>

            </asp:GridView>










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
