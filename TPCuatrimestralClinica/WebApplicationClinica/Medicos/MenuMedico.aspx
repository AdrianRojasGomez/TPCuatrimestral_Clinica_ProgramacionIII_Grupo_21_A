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
                <asp:Label ID="lblNombreDoctor" runat="server" CssClass="badge text-bg-primary" Text="Dr. Matías Gómez" />
                <asp:Label ID="lblNombreConsultorio" runat="server" CssClass="badge text-bg-secondary" Text="Consultorio 3" />
                <asp:Label ID="lblEspecialidad" runat="server" CssClass="badge text-bg-success" Text="Clínica Médica" />

            </div>
        </div>

        <!-- Resumen de KPIs -->
        <div class="row g-3 mb-3">
            <div class="col-12 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Turno en curso</div>
                        <asp:Label ID="lblTurnoCurso" runat="server" CssClass="display-6" Text="15:30 hs" />
                        <asp:Label ID="lblNombrePaciente" runat="server" CssClass="small text-muted" Text="Paciente: Juan Pérez" />

                    </div>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <asp:Label ID="lblEsperaTurno" runat="server" CssClass="small text-muted" Text="En espera" />
                        <asp:Label ID="lblMinutosEspera" runat="server" CssClass="display-6" Text="5" />
                        <asp:Label ID="lblTiempoEspera" runat="server" CssClass="small text-muted" Text="Tiempo prom.: 7 min" />

                    </div>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Atendidos hoy</div>
                        <asp:Label ID="lblAtendidos" runat="server" CssClass="display-6" Text="12" />
                        <asp:Label ID="lblOjetivo" runat="server" CssClass="small text-muted" Text="Objetivo: 20" />

                    </div>
                </div>
            </div>
            <div class="col-12 col-md-3">
                <div class="card shadow-sm h-100">
                    <div class="card-body">
                        <div class="small text-muted">Siguiente estimado</div>
                        <asp:Label ID="lblSiguienteTurno" runat="server" CssClass="small text-muted" Text="#A-024" />
                        <asp:Label ID="lblSiguientePaciente" runat="server" CssClass="small text-muted" Text="Paciente: Carlos Díaz" />
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
                        <asp:Label ID="lblturnoActual" runat="server" CssClass="badge text-bg-info" Text="#A-023" />

                    </div>
                    <div class="card-body">
                        <div class="mb-2">
                            <div class="fw-semibold">Paciente</div>
                            <asp:Label ID="lblPaciente" runat="server" Text="María López (DNI 32.123.456)" />

                        </div>
                        <div class="mb-2">
                            <div class="fw-semibold">Motivo</div>
                            <asp:Label ID="lblMotivoConsulta" runat="server" Text="Control general" />

                        </div>
                        <div class="mb-3">
                            <div class="fw-semibold">Observaciones</div>
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control" placeholder="Anotaciones del médico..." />




                        </div>
                        <div class="d-flex flex-wrap gap-2">
                            <asp:Button ID="btnLLamarPaciente" runat="server" CssClass="btn btn-outline-primary" Text="Llamar" OnClick="btnLLamarPaciente_Click" />
                            <asp:Button ID="btnAtender" runat="server" CssClass="btn btn-primary" Text="Atender" OnClick="btnAtender_Click" />

                            <asp:Button ID="btnFinalizar" runat="server" CssClass="btn btn-success" Text="Finalizar" OnClick="btnFinalizar_Click" />
                            <asp:Button ID="btnReprogramar" runat="server" CssClass="btn btn-warning" Text="Reprogramar" OnClick="btnReprogramar_Click" />
                            <asp:Button ID="btnSiguientePaciente" runat="server" CssClass="btn btn-outline-secondary" Text="Siguiente" OnClick="btnSiguientePaciente_Click" />

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
                            <span class="input-group-text">Buscar Paciente</span>
                            <asp:TextBox ID="txteBuscarPaciente" runat="server" OnTextChanged="txteBuscarPaciente_TextChanged" />

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
                            <!-- ========================================================== -->
                            <!-- 🔹 BLOQUE OPCIONAL: COLA DE TURNOS EN ESPERA (REPEATER DINÁMICO FUTURO) -->
                            <!-- ========================================================== -->
                            <!-- 
📘 Descripción general:
Este bloque está comentado para evitar ejecución por ahora.
Más adelante, el compañero encargado de TURNOS podrá descomentar 
todo el contenido para hacer la cola de espera dinámica.

🧩 Qué hará el Repeater:
- Generará un <li> automáticamente por cada turno en espera.
- Mostrará: código de turno, nombre del paciente, DNI, hora, motivo y tiempo de espera.
- El diseño es compatible con Bootstrap (list-group).

⚙️ Qué debe implementar el compañero de TURNOS:
1. Crear una lista de turnos en espera (por ejemplo, List<Turno> listaColaTurnos).
2. Asignarla como fuente de datos:
       rptColaTurnos.DataSource = listaColaTurnos;
       rptColaTurnos.DataBind();
3. Asegurarse de que el objeto Turno tenga las propiedades:
       CodigoTurno, NombrePaciente, DniPaciente, HoraTurno, Motivo, TiempoEspera.
4. Si no hay pacientes, puede usar EmptyDataTemplate para mostrar un mensaje.
-->

                            <%-- 
<h5 class="fw-bold mb-2">Cola de Turnos en Espera</h5>

<asp:Repeater ID="rptColaTurnos" runat="server">
    <ItemTemplate>
        <!-- Cada elemento <li> representa un turno en espera -->
        <li class="list-group-item d-flex justify-content-between align-items-center">
            <div>
                <!-- Código del turno y nombre del paciente -->
                <div class="fw-semibold">
                    <%# Eval("CodigoTurno") %> — <%# Eval("NombrePaciente") %>
                </div>

                <!-- Datos adicionales: DNI, hora y motivo -->
                <div class="small text-muted">
                    DNI <%# Eval("DniPaciente") %> • 
                    <%# Eval("HoraTurno", "{0:HH:mm}") %> • 
                    Motivo: <%# Eval("Motivo") %>
                </div>
            </div>

            <!-- Tiempo estimado de espera mostrado como badge -->
            <span class="badge text-bg-secondary">
                Espera <%# Eval("TiempoEspera") %>
            </span>
        </li>
    </ItemTemplate>
</asp:Repeater>
                            --%>

                            <!-- 🟢 Fin del bloque comentado del Repeater -->

                            <!-- ... -->
                        </ul>
                    </div>
                    <div class="card-footer d-flex justify-content-end gap-2">
                        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-outline-secondary" Text="Actualizar" OnClick="btnActualizar_Click" />
                        <asp:Button ID="btnLlamarProxPaciente" runat="server" CssClass="btn btn-outline-primary" Text="Llamar Siguiente" OnClick="btnLlamarProxPaciente_Click" />
                       
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
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="holaaaaaaaaa"></button>
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
