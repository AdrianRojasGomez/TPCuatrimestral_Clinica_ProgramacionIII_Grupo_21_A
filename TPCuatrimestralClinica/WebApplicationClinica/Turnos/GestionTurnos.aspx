<%@ Page Title="Gestión de Turnos" Language="C#" MasterPageFile="~/Clinica.Master"
    AutoEventWireup="true" CodeBehind="GestionTurnos.aspx.cs" Inherits="WebApplicationClinica.GestionTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table th > a { color: #212529 !important; text-decoration: none; }
        .table th > a:hover { color: #000 !important; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-3"><%: Title %></h2>
        <p class="lead">Administración de los turnos de la clínica.</p>

        <div class="card mb-4">
            <div class="card-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-6 col-lg-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarTurno" runat="server" CssClass="form-control"
                                Placeholder="Buscar por paciente, médico o fecha..."></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4 text-end">
                        <asp:Button ID="btnNuevoTurno" runat="server" Text="Agregar Nuevo Turno"
                            CssClass="btn btn-primary w-100" OnClick="btnNuevoTurno_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="alert" role="alert" runat="server" id="divMensaje" visible="false">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="card mb-4">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0"><asp:Label ID="lblFormTitulo" runat="server" Text="Nuevo Turno"></asp:Label></h3>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfTurnoId" runat="server" Value="0" />

                <asp:ValidationSummary ID="vsErrores" runat="server"
                    HeaderText="Por favor, corrige los siguientes errores:"
                    CssClass="alert alert-danger" DisplayMode="BulletList" />

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Label AssociatedControlID="ddlPaciente" runat="server" Text="Paciente:" />
                        <asp:DropDownList ID="ddlPaciente" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPaciente"
                            InitialValue="" ErrorMessage="El paciente es obligatorio."
                            CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="col-md-6 mb-3">
                        <asp:Label AssociatedControlID="ddlEspecialidad" runat="server" Text="Especialidad:" />
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"
                            AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEspecialidad"
                            InitialValue="" ErrorMessage="La especialidad es obligatoria."
                            CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <asp:Label AssociatedControlID="ddlMedico" runat="server" Text="Médico:" />
                        <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMedico"
                            InitialValue="" ErrorMessage="El médico es obligatorio."
                            CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="col-md-3 mb-3">
                        <asp:Label AssociatedControlID="txtFecha" runat="server" Text="Fecha:" />
                        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFecha"
                            ErrorMessage="La fecha es obligatoria." CssClass="text-danger" Display="Dynamic" />
                    </div>

                    <div class="col-md-3 mb-3">
                        <asp:Label AssociatedControlID="txtHora" runat="server" Text="Hora:" />
                        <asp:TextBox ID="txtHora" runat="server" TextMode="Time" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHora"
                            ErrorMessage="La hora es obligatoria." CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtObservaciones" runat="server" Text="Observaciones:" />
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine"
                        Rows="3" CssClass="form-control" />
                </div>

                <div class="row">
                    <div class="col-md-4 mb-3">
                        <asp:Label AssociatedControlID="ddlEstado" runat="server" Text="Estado:" />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Pendiente" Value="Pendiente" />
                            <asp:ListItem Text="Confirmado" Value="Confirmado" />
                            <asp:ListItem Text="Atendido" Value="Atendido" />
                            <asp:ListItem Text="Cancelado" Value="Cancelado" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="mt-4 text-end">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                        CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                        CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </asp:Panel>

        <div class="card card-body">
            <h3 class="mb-3">Lista de Turnos</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdTurno"
                    CssClass="table table-hover table-striped table-bordered"
                    EmptyDataText="No hay turnos registrados."
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTurnos_PageIndexChanging"
                    AllowSorting="true" OnSorting="gvTurnos_Sorting" OnRowCreated="gvTurnos_RowCreated"
                    OnRowCommand="gvTurnos_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="IdTurno" HeaderText="ID" ReadOnly="True" SortExpression="IdTurno" />
                        <asp:BoundField DataField="FechaHora" HeaderText="Fecha y Hora" SortExpression="FechaHora" />
                        <asp:BoundField DataField="Paciente" HeaderText="Paciente" SortExpression="Paciente" />
                        <asp:BoundField DataField="Medico" HeaderText="Médico" SortExpression="Medico" />
                        <asp:BoundField DataField="Especialidad" HeaderText="Especialidad" SortExpression="Especialidad" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarTurno"
                                    CommandArgument='<%# Eval("IdTurno") %>'
                                    CssClass="btn btn-sm btn-info me-2" ToolTip="Editar Turno">
                                    <i class="bi bi-pencil-square"></i> Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="CustomDelete"
                                    CommandArgument='<%# Eval("IdTurno") %>'
                                    CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('¿Está seguro que desea eliminar este turno?');"
                                    ToolTip="Eliminar Turno">
                                    <i class="bi bi-trash"></i> Eliminar
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
