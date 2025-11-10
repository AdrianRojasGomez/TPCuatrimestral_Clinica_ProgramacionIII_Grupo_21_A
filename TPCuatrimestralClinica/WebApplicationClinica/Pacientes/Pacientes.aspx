<%@ Page Title="Gestión de Pacientes" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="WebApplicationClinica.Pacientes" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table th > a {
            color: #212529 !important; 
            text-decoration: none;
        }

        .table th > a:hover {
            color: #000 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
    <div class="container mt-4">
        <h2 class="mb-3"><%: Title %></h2>
        <p class="lead">Administración de la información de los pacientes de la clínica.</p>

        <div class="card mb-4">
            <div class="card-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-6 col-lg-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarPaciente" runat="server" CssClass="form-control" Placeholder="Buscar paciente por nombre o DNI..."></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4 text-end">
                        <asp:Button ID="btnNuevoPaciente" runat="server" Text="Agregar Nuevo Paciente" CssClass="btn btn-primary w-100" OnClick="btnNuevoPaciente_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="alert" role="alert" runat="server" id="divMensaje" visible="false">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="card mb-4">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">
                    <asp:Label ID="lblFormTitulo" runat="server" Text="Nuevo Paciente"></asp:Label></h3>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfPacienteId" runat="server" Value="0" />

                <asp:ValidationSummary ID="vsErrores" runat="server"
                HeaderText="Por favor, corrige los siguientes errores:"
                CssClass="alert alert-danger" 
                DisplayMode="BulletList" />

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>

                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                            ErrorMessage="El nombre es obligatorio." Display="Dynamic"
                            CssClass="text-danger">                     
                    </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                    
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                            ControlToValidate="txtApellido"
                            ErrorMessage="El apellido es obligatorio."
                            Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label3" runat="server" Text="DNI:"></asp:Label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                             
                        <asp:RequiredFieldValidator ID="rfvDni" runat="server"
                            ControlToValidate="txtDni"
                            ErrorMessage="El DNI es obligatorio."
                            Display="Dynamic" CssClass="text-danger">
                         </asp:RequiredFieldValidator>

                        <%-- Validador para formato de DNI (solo números) --%>
                        <asp:RegularExpressionValidator ID="revDni" runat="server"
                            ControlToValidate="txtDni"
                            ErrorMessage="El DNI debe contener solo números."
                            ValidationExpression="^\d+$"
                            Display="Dynamic" CssClass="text-danger">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label4" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>

                        <asp:CompareValidator ID="cvFecha" runat="server"
                            ControlToValidate="txtFechaNacimiento"
                            Operator="DataTypeCheck"
                            Type="Date"
                            ErrorMessage="La fecha de nacimiento no es válida."
                            Display="Dynamic" CssClass="text-danger">
                        </asp:CompareValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>

                        <%-- Validador para Email (Requerido) --%>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El email es obligatorio."
                            Display="Dynamic" CssClass="text-danger">
                        </asp:RequiredFieldValidator>

                        <%-- Validador para formato de Email --%>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El formato del email no es válido."
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic" CssClass="text-danger">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-6 mb-3">
                        <asp:Label ID="Label6" runat="server" Text="Teléfono:"></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="mb-3">
                    <asp:Label ID="Label7" runat="server" Text="Dirección:"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="mt-4 text-end">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </asp:Panel>

        <div class="card card-body">
            <h3 class="mb-3">Lista de Pacientes</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" DataKeyNames="IdPaciente"
                    CssClass="table table-hover table-striped table-bordered" EmptyDataText="No hay pacientes registrados."
                    OnRowCommand="gvPacientes_RowCommand"

                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPacientes_PageIndexChanging"
                    AllowSorting="true" OnSorting="gvPacientes_Sorting" OnRowCreated="gvPacientes_RowCreated">
                    <Columns>
                        <%--<asp:BoundField DataField="IdPaciente" HeaderText="ID" ReadOnly="True" SortExpression="IdPaciente" />--%>
                        <asp:BoundField DataField="Dni" HeaderText="DNI" SortExpression="Dni" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                       <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                        <ItemTemplate>
        
                            <asp:LinkButton ID="btnEditar" runat="server"
                                CommandName="EditarPaciente"
                                CommandArgument='<%# Eval("IdPaciente") %>'
                                CssClass="btn btn-sm btn-info"
                                ToolTip="Editar Paciente">
                                <i class="bi bi-pencil-fill"></i> Editar
                            </asp:LinkButton>
        
                            <%-- Botón Eliminar (Lógico) - Visible solo si Estado = 1 (true) --%>
                            <asp:LinkButton ID="btnEliminar" runat="server"
                                CommandName="CustomDelete"
                                CommandArgument='<%# Eval("IdPaciente") %>'
                                CssClass="btn btn-sm btn-danger"
                                ToolTip="Dar de Baja"
                                OnClientClick="return confirm('¿Está seguro de que desea dar de baja a este paciente?');"
                                Visible='<%# Convert.ToBoolean(Eval("Estado")) %>'>
                                <i class="bi bi-trash-fill"></i> Eliminar
                            </asp:LinkButton>

                            <%-- Botón Reactivar - Visible solo si Estado = 0 (false) --%>
                            <asp:LinkButton ID="btnReactivar" runat="server"
                                CommandName="ReactivarPaciente"
                                CommandArgument='<%# Eval("IdPaciente") %>'
                                CssClass="btn btn-sm btn-success"
                                ToolTip="Reactivar Paciente"
                                Visible='<%# !Convert.ToBoolean(Eval("Estado")) %>'>
                                <i class="bi bi-arrow-clockwise"></i> Reactivar
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#f8f9fa" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerStyle CssClass="pagination-sm" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
