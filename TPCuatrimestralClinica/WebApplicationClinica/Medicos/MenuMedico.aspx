<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MenuMedico.aspx.cs" Inherits="WebApplicationClinica.Medicos.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css">
    <style>
        .turno-item { cursor: pointer; transition: background-color 0.2s; }
        .turno-item:hover { background-color: #f8f9fa; }
        .turno-item.active { border-left: 5px solid #0d6efd; background-color: #e9f0ff; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid py-4">
        <!-- Encabezado -->
        <div class="d-flex align-items-center justify-content-between mb-4 border-bottom pb-3">
            <h2 class="h3 mb-0 text-primary">Panel Médico - Turnos de Hoy</h2>
            <div class="d-flex gap-2 align-items-center">
                <i class="fas fa-user-md text-secondary"></i>
                <asp:Label ID="lblNombreDoctor" runat="server" CssClass="fw-bold" />
                <span class="vr mx-2"></span>
                <asp:Label ID="lblEspecialidad" runat="server" CssClass="badge text-bg-success" />
            </div>
        </div>

        <!-- Alerta Feedback -->
        <asp:Panel ID="pnlAlertaSeleccion" runat="server" Visible="false" CssClass="alert alert-info alert-dismissible fade show">
            <i class="fas fa-info-circle me-2"></i>
            <asp:Literal ID="litMensajeSeleccion" runat="server" />
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </asp:Panel>
        
        <div class="row g-4">
            <!-- Columna Izquierda Detalle -->
            <div class="col-12 col-lg-6">
                <div class="card shadow-sm h-100">
                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                        <span class="fw-semibold"><i class="fas fa-stethoscope me-2"></i>Turno en Consulta</span>
                        <asp:Label ID="lblturnoActual" runat="server" CssClass="badge text-bg-light text-primary" Text="Sin selección" />
                    </div>
                    
                    <div class="card-body">
                        <asp:Panel ID="pnlDetalleTurno" runat="server" Enabled="false">
                            <div class="mb-3 border-bottom pb-2">
                                <div class="fw-semibold text-muted">Paciente</div>
                                <asp:Label ID="lblPaciente" runat="server" CssClass="fs-5" Text="Seleccione un turno..." />
                            </div>
                            <div class="mb-3">
                                <div class="fw-semibold text-muted">Motivo</div>
                                <asp:Label ID="lblMotivoConsulta" runat="server" Text="--" />
                            </div>
                            <div class="mb-3">
                                <div class="fw-semibold text-muted">Observaciones (Diagnóstico)</div>
                                <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control" placeholder="Escriba el diagnóstico..." />
                            </div>
                            <asp:HiddenField ID="hdnIdTurnoSeleccionado" runat="server" />
                        </asp:Panel>
                    </div>

                    <div class="card-footer bg-light d-flex flex-wrap gap-2 justify-content-end">
                        <asp:Button ID="btnAtender" runat="server" CssClass="btn btn-primary" Text="Atender" OnClick="btnAtender_Click" />
                        <asp:Button ID="btnFinalizar" runat="server" CssClass="btn btn-success" Text="Finalizar" OnClick="btnFinalizar_Click" />
                        <asp:Button ID="btnReprogramar" runat="server" CssClass="btn btn-warning" Text="Reprogramar" OnClick="btnReprogramar_Click" />
                    </div>
                </div>
            </div>

            <!-- Columna Derecha Lista -->
            <div class="col-12 col-lg-6">
                <div class="card shadow-sm h-100">
                    <div class="card-header bg-secondary text-white d-flex justify-content-between align-items-center">
                        <span><i class="fas fa-list-ul me-2"></i>Turnos Pendientes</span>
                        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-sm btn-outline-light" Text="Refrescar" OnClick="btnActualizar_Click" />
                    </div>
                    
                    <div class="card-body p-0">
                        <div style="max-height: 70vh; overflow-y: auto;">
                            <asp:Repeater ID="rptColaTurnos" runat="server" OnItemCommand="rptColaTurnos_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSeleccionarTurno" runat="server" 
                                        CommandName="Seleccionar"
                                        CommandArgument='<%# Eval("IdTurno") %>' 
                                        CssClass="d-none" />
                                        
                                    <li class="list-group-item list-group-item-action d-flex justify-content-between align-items-center p-3 turno-item"
                                        onclick="document.getElementById('<%# ((LinkButton)Container.FindControl("lnkSeleccionarTurno")).ClientID %>').click();">
                                        <div>
                                            <div class="fw-semibold text-primary">
                                                <%# Eval("CodigoTurno") %> — <%# Eval("NombrePaciente") %>
                                            </div>
                                            <div class="small text-muted">
                                                <!-- formato para TimeSpan: hh\:mm -->
                                                <i class="far fa-clock me-1"></i> <%# Eval("HoraTurno") %> • 
                                                <i class="fas fa-tag me-1"></i> <%# Eval("Motivo") %>
                                            </div>
                                        </div>                                       
                                        <span class="badge rounded-pill text-bg-<%# GetStatusBadgeClass(Eval("EstadoTurno")) %>">
                                            <%# GetNombreEstado(Eval("EstadoTurno")) %>
                                        </span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Literal ID="litSinTurnos" runat="server" Visible="false" Text='<div class="p-4 text-center text-muted">No hay turnos hoy.</div>' />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Finalizar -->
    <div class="modal fade" id="modalFinalizar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Consulta Finalizada</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <p>Los datos se han guardado correctamente.</p>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>

    
</asp:Content>