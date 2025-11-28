<%@ Page Title="Modificar Turno" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="ModificarTurno.aspx.cs" Inherits="WebApplicationClinica.Turnos.ModificarTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Hidden: Id del turno -->
    <asp:HiddenField ID="hdnIdTurno" runat="server" />

    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-12 col-lg-10">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white">
                        <h4 class="mb-0">
                            <i class="fas fa-calendar-alt me-2"></i>
                            Modificar Turno Médico
                        </h4>
                    </div>
                    <div class="card-body">

                        <!-- DATOS DEL PACIENTE -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">1. Datos del Paciente</h5>

                        <!-- Info solo lectura -->
                        <asp:Panel ID="pnlDatosPaciente" runat="server" CssClass="bg-light p-3 rounded border mb-3">
                            <div class="row g-3">
                                <div class="col-md-3">
                                    <label class="form-label">DNI Paciente</label>
                                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label">Nombre</label>
                                    <asp:TextBox ID="txtNombrePaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label">Apellido</label>
                                    <asp:TextBox ID="txtApellidoPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label">Teléfono</label>
                                    <asp:TextBox ID="txtTelefonoPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Email</label>
                                    <asp:TextBox ID="txtEmailPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Dirección</label>
                                    <asp:TextBox ID="txtDireccionPaciente" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-warning d-block" Visible="false"></asp:Label>

                        <!-- DATOS DEL TURNO -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">2. Datos del Turno</h5>
                        <div class="row g-3 mb-4">
                            <div class="col-md-6">
                                <label class="form-label">Especialidad</label>
                                <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                                </asp:DropDownList>
                                <small id="EspecialidadMuted" runat="server" class="text-muted d-block mt-1">1. Seleccione una especialidad (o mantenga la actual).
                                </small>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Médico</label>
                                <asp:DropDownList ID="ddlMedicoDisponible" runat="server" CssClass="form-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMedicoDisponible_SelectedIndexChanged">
                                </asp:DropDownList>
                                <small id="MedicoMuted" runat="server" class="text-muted d-block mt-1">2. Seleccione el médico (o mantenga el actual).
                                </small>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Fechas Disponibles</label>
                                <asp:DropDownList ID="ddlFechaTurno" runat="server" CssClass="form-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFechaTurno_SelectedIndexChanged">
                                </asp:DropDownList>
                                <small id="FechaMuted" runat="server" class="text-muted d-block mt-1">3. Elija una fecha de la lista.
                                </small>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label">Horario Disponible</label>
                                <asp:DropDownList ID="ddlHorario" runat="server" CssClass="form-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                                </asp:DropDownList>
                                <small id="HorarioMuted" runat="server" class="text-muted d-block mt-1">4. Seleccione el horario.
                                </small>
                            </div>
                        </div>

                        <!-- DETALLES FINALES -->
                        <h5 class="text-secondary border-bottom pb-2 mb-3">3. Detalles del Turno</h5>
                        <div class="mb-3">
                            <label class="form-label">Motivo de Consulta</label>
                            <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control"
                                placeholder="Ej: Dolor de cabeza recurrente..."></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Observaciones Adicionales</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control"
                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>

                    </div>
                    <div class="card-footer bg-light text-end">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardarCambios_Click" />
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
                    <h5 class="modal-title">
                        <asp:Label ID="lblTituloModal" runat="server" />
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:Literal ID="litCuerpoModal" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Corregir</button>
                    <asp:Button ID="btnConfirmarModal" runat="server" Text="Confirmar Cambios" CssClass="btn btn-primary" OnClick="btnConfirmarModal_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Mensaje -->
    <div class="modal fade" id="modalMensaje" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">
                        <asp:Label ID="lblTituloMensaje" runat="server" />
                    </h5>
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

</asp:Content>

