<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MenuMedico.aspx.cs" Inherits="WebApplicationClinica.Medicos.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Bootstrap 5 (CDN) — borrar si ya lo cargás en tu Master -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">

    <div class="container-fluid py-3">
        <!-- Encabezado -->
        <div class="d-flex align-items-center justify-content-between mb-3">
            <h2 class="h4 mb-0">Panel del Médico</h2>
            <div class="d-flex gap-2">
                <span class="badge text-bg-primary">Dr. Juan Pérez</span>
                <span class="badge text-bg-secondary">Consultorio 3</span>
                <span class="badge text-bg-success">Especialidad: Clínica</span>
            </div>
        </div>

        <!-- Resumen de KPIs -->
        <div class="row g-3 mb-3">
            <div class="col-12 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Turno en curso</div>
                        <div class="display-6" id="kpiActual">#A-023</div>
                        <div class="small text-muted">Paciente: <span id="kpiPaciente">María López</span></div>
                    </div>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">En espera</div>
                        <div class="display-6" id="kpiEspera">5</div>
                        <div class="small text-muted">Tiempo prom.: 7 min</div>
                    </div>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Atendidos hoy</div>
                        <div class="display-6" id="kpiAtendidos">12</div>
                        <div class="small text-muted">Objetivo: 20</div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Siguiente estimado</div>
                        <div class="display-6" id="kpiProximo">#A-024</div>
                        <div class="small text-muted">Paciente: Carlos Díaz</div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Contenido principal -->
        <div class="row g-3">
            <!-- Columna izquierda: Turno en curso -->
            <div class="col-12 col-lg-6">
                <div class="card shadow-sm">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <span class="fw-semibold">Turno en curso</span>
                        <span class="badge text-bg-info">#A-023</span>
                    </div>
                    <div class="card-body">
                        <div class="mb-2">
                            <div class="fw-semibold">Paciente</div>
                            <div id="pacienteActual">María López (DNI 32.123.456)</div>
                        </div>
                        <div class="mb-2">
                            <div class="fw-semibold">Motivo</div>
                            <div id="motivoActual">Control general</div>
                        </div>
                        <div class="mb-3">
                            <div class="fw-semibold">Observaciones</div>
                            <textarea class="form-control" id="obsActual" rows="3" placeholder="Anotaciones del médico..."></textarea>

                        </div>
                        <div class="d-flex flex-wrap gap-2">
                            <button class="btn btn-outline-primary" id="btnLlamar">Llamar</button>
                            <button class="btn btn-primary" id="btnAtender">Atender</button>
                            <button class="btn btn-success" id="btnFinalizar" data-bs-toggle="modal" data-bs-target="#modalFinalizar">Finalizar</button>
                            <button class="btn btn-warning" id="btnReprogramar" data-bs-toggle="modal" data-bs-target="#modalReprogramar">Reprogramar</button>
                            <button class="btn btn-outline-secondary" id="btnSiguiente">Siguiente turno</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Columna derecha: Cola de turnos -->
            <div class="col-12 col-lg-6">
                <div class="card shadow-sm h-100">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <span class="fw-semibold">Cola de turnos</span>
                        <div class="input-group" style="max-width: 260px;">
                            <span class="input-group-text">Buscar</span>
                            <input type="text" class="form-control" placeholder="DNI / Nombre / Nº turno" />
                        </div>
                    </div>
                    <div class="card-body p-0">
                        <ul class="list-group list-group-flush" id="listaCola">
                            <!-- Ejemplos de items -->
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div>
                                    <div class="fw-semibold">#A-024 — Carlos Díaz</div>
                                    <div class="small text-muted">DNI 28.987.654 • 09:40 • Motivo: Control</div>
                                </div>
                                <span class="badge text-bg-secondary">Espera 05:23</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                <div>
                                    <div class="fw-semibold">#A-025 — Laura Giménez</div>
                                    <div class="small text-muted">DNI 35.456.789 • 09:50 • Motivo: Dolor</div>
                                </div>
                                <span class="badge text-bg-secondary">Espera 00:58</span>
                            </li>
                            <!-- ... -->
                        </ul>
                    </div>
                    <div class="card-footer d-flex justify-content-end gap-2">
                        <button class="btn btn-outline-secondary">Actualizar</button>
                        <button class="btn btn-outline-primary">Llamar siguiente</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Finalizar -->
    <div class="modal fade" id="modalFinalizar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Finalizar consulta</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <label class="form-label">Diagnóstico</label>
                    <textarea class="form-control" rows="3" placeholder="Diagnóstico..."></textarea>
                    <label class="form-label mt-3">Indicaciones</label>
                    <textarea class="form-control" rows="3" placeholder="Indicaciones al paciente..."></textarea>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-outline-secondary" data-bs-dismiss="modal" type="button">Cancelar</button>
                    <button class="btn btn-success" type="submit">Guardar y finalizar</button>
                </div>
            </form>
        </div>
    </div>

    <!-- Modal Reprogramar -->
    <div class="modal fade" id="modalReprogramar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <form class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Reprogramar turno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="row g-2">
                        <div class="col-6">
                            <label class="form-label">Fecha</label>
                            <input type="date" class="form-control">
                        </div>
                        <div class="col-6">
                            <label class="form-label">Hora</label>
                            <input type="time" class="form-control">
                        </div>
                    </div>
                    <label class="form-label mt-3">Observaciones</label>
                    <input type="text" class="form-control" placeholder="Motivo de reprogramación...">
                </div>
                <div class="modal-footer">
                    <button class="btn btn-outline-secondary" data-bs-dismiss="modal" type="button">Cancelar</button>
                    <button class="btn btn-warning" type="submit">Reprogramar</button>
                </div>
            </form>
        </div>
    </div>

    <!-- Bootstrap JS (CDN) — borrar si ya lo cargás -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
