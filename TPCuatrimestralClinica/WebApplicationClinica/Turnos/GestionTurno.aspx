<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="GestionTurno.aspx.cs" Inherits="WebApplicationClinica.Turnos.GestionTurno" %>
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
        <p class="lead">Administración de la información de los Turnos de la clínica.</p>

        <div class="card mb-4">
            <div class="card-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-6 col-lg-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarPTurno" runat="server" CssClass="form-control" Placeholder="Buscar Turno por DNI..."></asp:TextBox>
<%--                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary" <%--OnClick="btnBuscar_Click--%>" />--%>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4 text-end">
<%--                        <asp:Button ID="btnNuevoTurno" runat="server" Text="Agregar Nuevo Turno" CssClass="btn btn-primary w-100" <%--OnClick="btnNuevoTurno_Click--%>" />--%>
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
                    <asp:Label ID="lblFormTitulo" runat="server" Text="Nuevo Turno"></asp:Label></h3>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTurnoId" runat="server" Value="0" />

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
                             
                        <%--DNI requerido--%>
                        <asp:RequiredFieldValidator ID="rfvDni" runat="server"
                            ControlToValidate="txtDni"
                            ErrorMessage="El DNI es obligatorio."
                            Display="Dynamic" CssClass="text-danger">
                         </asp:RequiredFieldValidator>
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
<%--                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" 
                   <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click"--%>
                </div>
            </div>
        </asp:Panel>

        <div class="card card-body">
            <h3 class="mb-3">Lista de Turno</h3>
            <div class="table-responsive">
               <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdTurno"
                    CssClass="table table-hover table-striped table-bordered" EmptyDataText="No hay Turnos registrados."
                    OnRowCommand="gvTurnos_RowCommand"

                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTurnos_PageIndexChanging"
                    AllowSorting="true" OnSorting="gvTurnos_Sorting" OnRowCreated="gvTurnos_RowCreated">
                    <Columns>
                        
                        <asp:BoundField DataField="T.NumeroTurno" HeaderText="Numero Turno" SortExpression="NumeroTurno" />
                        <asp:BoundField DataField="T.FechaInicio" HeaderText="Fecha Inicio" SortExpression="FechaInicio" />
                        <asp:BoundField DataField="T.HoraInicio" HeaderText="Fecha Inicio" SortExpression="HoraInicio" />
                        <asp:BoundField DataField="PacienteNombreCompleto" HeaderText="Nombre Paciente" SortExpression="TurnoNombre" />
                        <asp:BoundField DataField="MedicoNombreCompleto" HeaderText="Nombre Medico" SortExpression="MedicoNombre" />
                        <asp:BoundField DataField="EspecialidadNombre" HeaderText="Especialidad" SortExpression="EspecialidadNombre" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarTurno" CommandArgument='<%# Eval("IdTurno") %>' CssClass="btn btn-sm btn-info me-2" ToolTip="Editar Turno"><i class="bi bi-pencil-square"></i> Editar</asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="CustomDelete" CommandArgument='<%# Eval("IdTurno") %>' CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('¿Está seguro que desea cancelar este Turno?');" ToolTip="Cancelar Turno"><i class="bi bi-trash"></i> Eliminar</asp:LinkButton>
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
