<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="WebApplicationClinica.MainMenu" %>

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


    <div class="container-fluid bg-light py-3 shadow-sm ">
        <div class="row g-5 justify-content-center">

            <!-- Tarjeta 1 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">

                    <img src="<%: ResolveUrl("~/content/img/Turnos.png") %>" class="d-block mx-auto mt-4" style="width: 400px; height: auto;" alt="Ilustración de turnos">

                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Turnos</h3>
                        <p class="card-text text-soft mb-4">
                            Gestiona y asigna turnos a pacientes según especialidad y disponibilidad.
                        </p>

                        <!-- Botón de acción rapida -->
                        <asp:HyperLink ID="BtnCrear"
                            runat="server"
                            NavigateUrl="~/Turnos/GestionTurno.aspx"
                            CssClass="btn btn-primary mt-auto"
                            aria-label="Gestion turnos">
                            Gestion Turnos
                        </asp:HyperLink>
                    </div>
                </div>
            </div>

            <!-- Tarjeta 2 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">

                    <img src="<%: ResolveUrl("~/content/img/Pacientes.png") %>" class="d-block mx-auto mt-4" style="width: 400px; height: auto;" alt="Ilustración de Pacientes">
                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Pacientes</h3>
                        <p class="card-text text-soft mb-4">
                            Registra o actualiza la información de tus pacientes de forma segura.
                        </p>
                        <asp:Button ID="BtnAltaPaciente" runat="server" Text="Gestion Paciente" CssClass="btn btn-primary mt-auto shadow" OnClick="BtnAltaPaciente_Click" />
                    </div>
                </div>
            </div>

            <!-- Tarjeta 3 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">

                    <img src="<%: ResolveUrl("~/content/img/Medicos.png") %>" class="d-block mx-auto mt-4" style="width: 400px; height: auto;" alt="Ilustración de Medicos">

                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Medicos</h3>
                        <p class="card-text text-soft mb-4">
                            Gestiona la informacion de los Medicos disponibles, sus especialidades y turnos de trabajo.
                        </p>

                        <a href="<%: ResolveUrl("~/Medicos/AgregarMedico.aspx") %>"
                            class="btn btn-primary mt-auto shadow" aria-label="Buscar Medico">Gestion Medicos 
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid py-5">
        <div class="px-3 d-flex flex-column flex-md-row justify-content-between align-items-start align-items-md-center gap-3">
            <h2 class="card-title mb-2">Turnos Próximos</h2>
            <div class="ms-md-auto">
                <asp:HyperLink ID="btnCrearTurno"
                    runat="server"
                    NavigateUrl="~/Turnos/CrearTurno.aspx"
                    CssClass="btn btn-primary">
                Crear turno
                </asp:HyperLink>
            </div>
        </div>

        <div class="p-3">

            <asp:Literal ID="litErrorTurnos" runat="server" Visible="false" />

            <%-- Panel para mostrar la grilla si hay turnos --%>
            <asp:Panel ID="pnlTurnosDashboard" runat="server" Visible="false" class="table-responsive">
                <asp:GridView ID="gvTurnosProximos" runat="server" AutoGenerateColumns="False"
                    CssClass="table table-hover table-striped table-bordered"
                    AllowPaging="true" PageSize="5" OnPageIndexChanging="gvTurnosProximos_PageIndexChanging"
                    AllowSorting="true" OnSorting="gvTurnosProximos_Sorting" OnRowCreated="gvTurnosProximos_RowCreated">
                    <Columns>
                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha"
                            SortExpression="FechaInicio" DataFormatString="{0:dd/MM/yyyy}" />

                        <asp:TemplateField HeaderText="Hora" SortExpression="HoraInicio">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("HoraInicio", "{0:hh\\:mm}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="PacienteNombre" HeaderText="Paciente"
                            SortExpression="PacienteNombre" />

                        <asp:BoundField DataField="MedicoNombre" HeaderText="Médico"
                            SortExpression="MedicoNombre" />

                        <asp:BoundField DataField="EspecialidadNombre" HeaderText="Especialidad"
                            SortExpression="EspecialidadNombre" />

                        <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
                            <ItemTemplate>
                                <asp:Label runat="server"
                                    Text='<%# (int)Eval("Estado") == 1 ? "Pendiente" :
                       (int)Eval("Estado") == 2 ? "Atendiendo" :
                       (int)Eval("Estado") == 3 ? "Completado" :
                       (int)Eval("Estado") == 4 ? "No asistió" :
                       "Cancelado" %>'
                                    CssClass='<%# (int)Eval("Estado") == 1 ? "badge bg-warning text-dark" :  
                          (int)Eval("Estado") == 2 ? "badge bg-primary" :            
                          (int)Eval("Estado") == 3 ? "badge bg-success" :            
                          (int)Eval("Estado") == 4 ? "badge bg-secondary" :          
                          "badge bg-danger" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#f8f9fa" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerStyle CssClass="pagination-sm" HorizontalAlign="Right" />
                </asp:GridView>
            </asp:Panel>

            <%-- Panel para mostrar si NO hay turnos --%>
            <asp:Panel ID="pnlNoTurnos" runat="server" Visible="false">
                <%-- ... mensaje de "no hay turnos" ... --%>
            </asp:Panel>
        </div>
    </div>

    <div class="container-fluid bg-light py-5 shadow-sm ">
        <div class="px-3">
            <h2 class="card-title mb-2">Gestion del sistema</h2>
        </div>
        <div class="accordion" id="accordionGestion">

            <%--Accordeon Turnos--%>
            <div class="accordion-item" id="accordionTurnos">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTurno" aria-expanded="false" aria-controls="collapseTurno">
                        Gestion Turnos
                    </button>
                </h2>

                <div id="collapseTurno" class="accordion-collapse collapse" data-bs-parent="#accordionTurnos">
                    <div class="accordion-body">

                        <%--Elementos dentro del acordeon--%>
                        <div class="row row-cols-2 g-2">
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Crear.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Crear Turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Modificar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Modificar turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Cancelar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Cancelar Turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Buscar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Buscar Turno
                                </a>
                            </div>
                        </div>
                    </div>
                    <%--Elementos dentro del acordeon--%>
                </div>
            </div>

            <%--Accordeon Pacientes--%>
            <div class="accordion-item" id="accordionPacientes">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapsePaciente" aria-expanded="false" aria-controls="collapsePaciente">
                        Gestion Pacientes
                    </button>
                </h2>
                <div id="collapsePaciente" class="accordion-collapse collapse" data-bs-parent="#accordionPacientes">
                    <div class="accordion-body">
                        <%--Elementos dentro del acordeon--%>
                        <div class="row row-cols-2 g-2">
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Crear.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Crear Turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Modificar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Modificar turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Cancelar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Cancelar Turno
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Buscar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Buscar Turno
                                </a>
                            </div>
                        </div>
                    </div>
                    <%--Elementos dentro del acordeon--%>
                </div>
            </div>


            <%--Accordeon Medicos--%>
            <div class="accordion-item" id="accordionMedicos">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseMedicos" aria-expanded="false" aria-controls="collapseMedicos">
                        Gestion Medicos
                       
                    </button>
                </h2>
                <div id="collapseMedicos" class="accordion-collapse collapse" data-bs-parent="#accordionMedicos">
                    <div class="accordion-body">
                        <%--Elementos dentro del acordeon--%>
                        <div class="row row-cols-2 g-2">
                            <div class="col py-1">

                                <asp:Button ID="btnAgregarMedico" runat="server" CssClass="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center" Text="Agregar Medico" OnClick="btnAgregarMedico_Click" />

                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Modificar.aspx") %>">

                                    <asp:Button ID="btnModificarMedico" runat="server" class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center" Text="Modificar datos de un Medico" OnClick="btnModificarMedico_Click" />

                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Cancelar.aspx") %>">

                                    <asp:Button ID="btnListarMedico" runat="server" class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center" Text="Listar Medicos Disponibles" OnClick="btnListarMedico_Click" />
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Buscar.aspx") %>">
                                    <asp:Button ID="btnBajaMedico" runat="server" class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center" Text="Baja a un Medico" OnClick="btnBajaMedico_Click" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <%--Elementos dentro del acordeon--%>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
