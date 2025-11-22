<%@ Page Title="Crear Nuevo Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="CrearTurno.aspx.cs" Inherits="WebApplicationClinica.CrearTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/css/bootstrap-datepicker.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-center my-4"><%: Title %></h1>

    <main class="min-vh-100 d-flex align-items-stretch py-4">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-12 col-xl-10">
                    <div class="card shadow-lg border-0 rounded-3 w-100">
                        <div class="card-header bg-white sticky-top">
                            <h4 class="mb-0 text-center">Ingrese el detalle del turno</h4>
                        </div>

                        <div class="card-body p-4 p-md-5 overflow-auto">
                            <label for="txtDocumento" class="form-label"><b>1. Buscar Paciente por Documento</b></label>
                            <div class="d-flex align-items-center gap-3 flex-wrap mb-4">
                                <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control w-25" />
                                <asp:Button runat="server" ID="btnBuscarPaciente"
                                    CssClass="btn btn-primary btn-sm"
                                    Text="Buscar Paciente" UseSubmitBehavior="false"
                                    OnClick="btnBuscarPaciente_Click" CausesValidation="false" />
                                <asp:Label runat="server" ID="lblPacienteEstado" CssClass="badge bg-secondary" Text="Pendiente"></asp:Label>
                                <asp:Button runat="server" ID="btnIrAgregarPaciente"
                                    CssClass="btn btn-outline-secondary btn-sm"
                                    Text="Agregar Nuevo Paciente" UseSubmitBehavior="false"
                                    OnClick="btnIrAgregarPaciente_Click" Visible="false" CausesValidation="false" />
                            </div>
                            <asp:Label ID="lblMensajeError" runat="server"
                                CssClass="text-danger d-block" Visible="false"></asp:Label>

                            <asp:Panel ID="pnlDatosPaciente" runat="server" Visible="false" CssClass="card bg-light mb-4">
                                <div class="card-header">
                                    <b>Datos del Paciente Encontrado</b>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                                            <asp:TextBox ID="txtNombrePaciente" runat="server" ReadOnly="true" CssClass="form-control-plaintext" />
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                                            <asp:TextBox ID="txtApellidoPaciente" runat="server" ReadOnly="true" CssClass="form-control-plaintext" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                            <asp:TextBox ID="txtEmailPaciente" runat="server" ReadOnly="true" CssClass="form-control-plaintext" />
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <asp:Label ID="Label4" runat="server" Text="Teléfono:"></asp:Label>
                                            <asp:TextBox ID="txtTelefonoPaciente" runat="server" ReadOnly="true" CssClass="form-control-plaintext" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAgregarPaciente" runat="server" Visible="false" CssClass="card card-body bg-light mb-4">
                                <h5 class="mb-3">Registrar Nuevo Paciente</h5>

                                <asp:Label ID="lblMensajeNuevoPaciente" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label5" runat="server" Text="Nombre:" AssociatedControlID="txtNuevoNombre"></asp:Label>
                                        <asp:TextBox ID="txtNuevoNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNuevoNombre" runat="server" ErrorMessage="Nombre es obligatorio." ControlToValidate="txtNuevoNombre" Display="Dynamic" CssClass="text-danger" ValidationGroup="NuevoPaciente" />
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label6" runat="server" Text="Apellido:" AssociatedControlID="txtNuevoApellido"></asp:Label>
                                        <asp:TextBox ID="txtNuevoApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNuevoApellido" runat="server" ErrorMessage="Apellido es obligatorio." ControlToValidate="txtNuevoApellido" Display="Dynamic" CssClass="text-danger" ValidationGroup="NuevoPaciente" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label7" runat="server" Text="DNI:" AssociatedControlID="txtNuevoDni"></asp:Label>
                                        <asp:TextBox ID="txtNuevoDni" runat="server" CssClass="form-control" ReadOnly="true" ToolTip="DNI tomado de la búsqueda"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label8" runat="server" Text="Email:" AssociatedControlID="txtNuevoEmail"></asp:Label>
                                        <asp:TextBox ID="txtNuevoEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNuevoEmail" runat="server" ErrorMessage="Email es obligatorio." ControlToValidate="txtNuevoEmail" Display="Dynamic" CssClass="text-danger" ValidationGroup="NuevoPaciente" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label9" runat="server" Text="Teléfono:" AssociatedControlID="txtNuevoTelefono"></asp:Label>
                                        <asp:TextBox ID="txtNuevoTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <asp:Label ID="Label10" runat="server" Text="Fecha de Nacimiento:" AssociatedControlID="txtNuevoFechaNacimiento"></asp:Label>
                                        <asp:TextBox ID="txtNuevoFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="Label11" runat="server" Text="Dirección:" AssociatedControlID="txtNuevoDireccion"></asp:Label>
                                    <asp:TextBox ID="txtNuevoDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="d-flex justify-content-end gap-2 mt-3">
                                    <asp:Button ID="btnGuardarNuevoPaciente" runat="server" Text="Guardar y Usar" CssClass="btn btn-success" OnClick="btnGuardarNuevoPaciente_Click" ValidationGroup="NuevoPaciente" />
                                    <asp:Button ID="btnCancelarRegistro" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarRegistro_Click" CausesValidation="false" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDatosTurno" runat="server" Visible="true">
                                <div class="border-top border border-secondary-subtle opacity-50 my-4"></div>
                                <label class="form-label"><b>2. Completar Datos del Turno</b></label>

                                <div class="row row-cols-4 g-3 align-items-stretch">
                                    <%--Especialidad--%>
                                    <div class="col-12 col-md-3">
                                        <div class="card shadow-sm h-100">
                                            <div class="card-body d-flex flex-column">
                                                <h6 class="card-title mb-3">Especialidades</h6>

                                                    <asp:DropDownList
                                                        runat="server"
                                                        ID="ddlEspecialidad"
                                                        ClientIDMode="Static"
                                                        CssClass="form-select"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"
                                                        AppendDataBoundItems="true">
                                                        <asp:ListItem Value="">Seleccione una especialidad…</asp:ListItem>
                                                    </asp:DropDownList>
                                                

                                                <small
                                                    id="EspecialidadMuted"
                                                    runat="server"
                                                    class="text-muted d-block mt-2 mt-auto">1. Comience seleccionando una especialidad.
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Médico--%>
                                    <div class="col-12 col-md-3">
                                        <div class="card shadow-sm h-100">
                                            <div class="card-body d-flex flex-column">
                                                <h6 class="card-title mb-3">Medicos</h6>

                                                    <asp:DropDownList
                                                        runat="server"
                                                        ID="ddlMedicoDisponible"
                                                        ClientIDMode="Static"
                                                        CssClass="form-select"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlMedicoDisponible_SelectedIndexChanged"
                                                        AppendDataBoundItems="true">
                                                        <asp:ListItem Value="">Seleccione un médico…</asp:ListItem>
                                                    </asp:DropDownList>
                                               

                                                <small
                                                    id="MedicoMuted"
                                                    runat="server"
                                                    class="text-muted d-block mt-2 mt-auto">2. Seleccione una especialidad para ver los medicos disponibles.
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Fecha--%>
                                    <%--TextMode="Date" removido, ahora lo  aneja fechas.js--%>
                                    <div class="col-12 col-md-3">
                                        <div class="card shadow-sm h-100">
                                            <div class="card-body d-flex flex-column">
                                                <h6 class="card-title mb-3">Fechas Disponibles</h6>

                                                <%--Calendario inline visible --%>
                                                <div class="flex-grow-1 d-flex justify-content-center align-items-center">
                                                    <div id="calendarioTurnos" class="border rounded p-2"></div>
                                                </div>
                                                <%--Campo invisible para almacenar la fecha seleccionada--%>
                                                <asp:HiddenField
                                                    ID="hdnFechaTurno"
                                                    runat="server"
                                                    ClientIDMode="Static" />

                                                <small
                                                    id="FechaMuted"
                                                    runat="server"
                                                    class="text-muted d-block mt-2 mt-auto">3. Seleccione un medico para ver las fechas disponibles.
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Horarios disponibles--%>
                                    <div class="col-12 col-md-3">
                                        <div class="card shadow-sm h-100">
                                            <div class="card-body d-flex flex-column">
                                                <h6 class="card-title mb-3">Horarios disponibles</h6>

                                                
                                                    <asp:DropDownList
                                                        runat="server"
                                                        ID="ddlHorario"
                                                        CssClass="form-select"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                                                        <asp:ListItem Value="">Seleccione un horario…</asp:ListItem>
                                                    </asp:DropDownList>
                                                

                                                <small
                                                    id="HorarioMuted"
                                                    runat="server"
                                                    class="text-muted d-block mt-2 mt-auto">4. Seleccione una fecha para ver los horarios disponibles.
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row mt-4">
                                    <div class="col-12">
                                        <label for="txtMotivo" class="form-label">Motivo</label>
                                        <asp:TextBox
                                            runat="server"
                                            ID="txtMotivo"
                                            ClientIDMode="Static"
                                            TextMode="MultiLine"
                                            Rows="2"
                                            CssClass="form-control"
                                            placeholder="Motivo de la consulta…">
                                        </asp:TextBox>
                                    </div>
                                </div>

                                <div class="row mt-4">
                                    <div class="col-12">
                                        <label for="txtObservaciones" class="form-label">Observaciones</label>
                                        <asp:TextBox
                                            runat="server"
                                            ID="txtObservaciones"
                                            ClientIDMode="Static"
                                            TextMode="MultiLine"
                                            Rows="6"
                                            CssClass="form-control"
                                            placeholder="Notas u observaciones relevantes…">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <%--Pie (botones) fijo--%>
                        <asp:Panel ID="pnlFooter" runat="server" Visible="true">
                            <div class="card-footer bg-white sticky-bottom">
                                <div class="d-flex justify-content-end gap-2">
                                    <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
                                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn btn-secondary" Text="Cancelar"
                                        CausesValidation="false" UseSubmitBehavior="false" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <%--MODALS--%>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <asp:Label ID="lblTituloModal" runat="server" CssClass="modal-title h5"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>

                <div class="modal-body">
                    <asp:Literal ID="litCuerpoModal" runat="server"></asp:Literal>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        Cancelar
       
                    </button>

                    <%--Botón que confirmará en el servidor--%>
                    <asp:Button ID="btnConfirmarModal" runat="server"
                        Text="Confirmar"
                        CssClass="btn btn-primary"
                        OnClick="btnConfirmarModal_Click" />
                </div>

            </div>
        </div>
    </div>

    <%--MODAL DE MENSAJE DE ÉXITO --%>
    <div class="modal fade" id="modalMensaje" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <asp:Label ID="lblTituloMensaje" runat="server" CssClass="modal-title h5"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>

                <div class="modal-body">
                    <asp:Literal ID="litCuerpoMensaje" runat="server"></asp:Literal>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnExito" runat="server"
                        Text="Confirmar"
                        CssClass="btn btn-primary"
                        OnClick="btnExito_Click" />
                </div>

            </div>
        </div>
    </div>

    <%--srcs--%>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/js/bootstrap-datepicker.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.10.0/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="<%: ResolveUrl("~/scripts/fechas.js") %>"></script>
</asp:Content>
