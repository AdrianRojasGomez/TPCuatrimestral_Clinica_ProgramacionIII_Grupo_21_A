<%@ Page Title="Nuevo Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="CrearTurno.aspx.cs" Inherits="WebApplicationClinica.CrearTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Asegúrate de tener Bootstrap cargado en tu Master -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-12 col-lg-10">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0"><i class="fas fa-calendar-plus me-2"></i>Nuevo Turno Médico</h4>
                    </div>
                    <div class="card-body">
                        
                        <!-- SECCIÓN 1: DATOS DEL PACIENTE (Primero, como pediste) -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">1. Datos del Paciente</h5>
                        
                        <!-- Buscador -->
                        <div class="row g-3 align-items-end mb-3">
                            <div class="col-md-4">
                                <label class="form-label">DNI Paciente</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Ingrese DNI"></asp:TextBox>
                                    <asp:Button ID="btnBuscarPaciente" runat="server" Text="Buscar" CssClass="btn btn-outline-primary" OnClick="btnBuscarPaciente_Click" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblPacienteEstado" runat="server" CssClass="badge bg-secondary" Text="Esperando búsqueda..."></asp:Label>
                            </div>
                        </div>

                        <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-warning d-block" Visible="false"></asp:Label>
                        <asp:Button ID="btnIrAgregarPaciente" runat="server" Text="Registrar Nuevo Paciente" CssClass="btn btn-success btn-sm mb-3" OnClick="btnIrAgregarPaciente_Click" Visible="false" />

                        <!-- Panel: Datos del Paciente Encontrado -->
                        <asp:Panel ID="pnlDatosPaciente" runat="server" Visible="false" CssClass="bg-light p-3 rounded border mb-3">
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombrePaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtApellidoPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Email</label>
                                    <asp:TextBox ID="txtEmailPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtTelefonoPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <!-- Panel: Agregar Nuevo Paciente -->
                        <asp:Panel ID="pnlAgregarPaciente" runat="server" Visible="false" CssClass="bg-light p-3 rounded border border-success mb-3">
                            <h6 class="text-success">Registrar Nuevo Paciente</h6>
                            <div class="row g-3">
                                <div class="col-md-4">
                                    <label>DNI</label>
                                    <asp:TextBox ID="txtNuevoDni" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Nombre</label>
                                    <asp:TextBox ID="txtNuevoNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Apellido</label>
                                    <asp:TextBox ID="txtNuevoApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtNuevoEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Teléfono</label>
                                    <asp:TextBox ID="txtNuevoTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-8">
                                    <label>Dirección</label>
                                    <asp:TextBox ID="txtNuevoDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Fecha Nacimiento</label>
                                    <asp:TextBox ID="txtNuevoFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mt-3 text-end">
                                <asp:Label ID="lblMensajeNuevoPaciente" runat="server" CssClass="text-danger me-2" Visible="false"></asp:Label>
                                <asp:Button ID="btnCancelarRegistro" runat="server" Text="Cancelar" CssClass="btn btn-secondary btn-sm" OnClick="btnCancelarRegistro_Click" />
                                <asp:Button ID="btnGuardarNuevoPaciente" runat="server" Text="Guardar Paciente" CssClass="btn btn-success btn-sm" OnClick="btnGuardarNuevoPaciente_Click" />
                            </div>
                        </asp:Panel>

                        <!-- SECCIÓN 2: DATOS DEL TURNO (Segundo, selección de médico/fecha) -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">2. Datos del Turno</h5>
                        <div class="row g-3 mb-4">
                            <div class="col-md-6">
                                <label class="form-label">Especialidad</label>
                                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                                </asp:DropDownList>
                                <small id="EspecialidadMuted" runat="server" class="text-muted d-block mt-1">1. Seleccione especialidad.</small>
                            </div>
                            
                            <div class="col-md-6">
                                <label class="form-label">Médico</label>
                                <asp:DropDownList ID="ddlMedicoDisponible" runat="server" CssClass="form-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMedicoDisponible_SelectedIndexChanged" Enabled="false">
                                </asp:DropDownList>
                                <small id="MedicoMuted" runat="server" class="text-muted d-block mt-1">2. Luego seleccione médico.</small>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Fechas Disponibles</label>
                                <%-- Mantenemos el DropDownList para filtrar días válidos --%>
                                <asp:DropDownList ID="ddlFechaTurno" runat="server" CssClass="form-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFechaTurno_SelectedIndexChanged" Enabled="false">
                                </asp:DropDownList>
                                <small id="FechaMuted" runat="server" class="text-muted d-block mt-1">3. Elija una fecha de la lista.</small>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Horario Disponible</label>
                                <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged" Enabled="false">
                                </asp:DropDownList>
                                <small id="HorarioMuted" runat="server" class="text-muted d-block mt-1">4. Seleccione hora.</small>
                            </div>
                        </div>

                        <!-- SECCIÓN 3: DETALLES FINALES -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">3. Detalles Finales</h5>
                        <div class="mb-3">
                            <label class="form-label">Motivo de Consulta</label>
                            <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" placeholder="Ej: Dolor de cabeza recurrente..."></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Observaciones Adicionales</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>

                    </div>
                    <div class="card-footer bg-light text-end">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Agendar Turno" CssClass="btn btn-primary" OnClick="btnGuardar_Click" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Confirmación -->
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"><asp:Label ID="lblTituloModal" runat="server" /></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Literal ID="litCuerpoModal" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Corregir</button>
                    <asp:Button ID="btnConfirmarModal" runat="server" Text="Confirmar Turno" CssClass="btn btn-primary" OnClick="btnConfirmarModal_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Éxito -->
    <div class="modal fade" id="modalMensaje" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title"><asp:Label ID="lblTituloMensaje" runat="server" /></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Literal ID="litCuerpoMensaje" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnExito" runat="server" Text="Aceptar" CssClass="btn btn-success" OnClick="btnExito_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Script Bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
