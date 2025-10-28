<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="WebApplicationClinica.MainMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid bg-light py-5 shadow-sm ">
        <div class="row g-5 justify-content-center">

            <!-- Tarjeta 1 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <!-- Esteban: Si quieres agregar una imagen, si no borra esto
          <img src="<%: ResolveUrl("~/content/img/turnos.jpg") %>" class="card-img-top" alt="Ilustración de turnos">
          -->
                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Turnos</h3>
                        <p class="card-text text-soft mb-4">
                            Gestiona y asigna turnos a pacientes según especialidad y disponibilidad.
                        </p>

                        <!-- Botón de acción rapida -->
                        <a href="<%: ResolveUrl("~/Turnos/Crear.aspx") %>"
                            class="btn btn-primary mt-auto"
                            aria-label="Crear nuevo turno">Crear turno
                        </a>
                    </div>
                </div>
            </div>

            <!-- Tarjeta 2 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <!-- Esteban aca igual pana mio
          <img src="<%: ResolveUrl("~/content/img/pacientes.jpg") %>" class="card-img-top" alt="Ilustración de pacientes">
          -->
                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Pacientes</h3>
                        <p class="card-text text-soft mb-4">
                            Registra y actualiza la información de tus pacientes de forma segura.
                        </p>


                        <%--Esteban podemos sombrear este boton para que se noten los 2 distintos links mas rapido?--%>
                        <a href="<%: ResolveUrl("~/Pacientes/Nuevo.aspx") %>"
                            class="btn btn-primary mt-auto"
                            aria-label="Alta de nuevo paciente">Alta paciente
                        </a>
                    </div>
                </div>
            </div>

            <!-- Tarjeta 3 -->
            <div class="col-12 col-md-4">
                <div class="card h-100 shadow-sm border-0">
                    <!-- Esteban aca igual pana mio
          <img src="<%: ResolveUrl("~/content/img/pacientes.jpg") %>" class="card-img-top" alt="Ilustración de pacientes">
          -->
                    <div class="card-body d-flex flex-column">
                        <h3 class="card-title mb-2">Medicos</h3>
                        <p class="card-text text-soft mb-4">
                            Gestiona la informacion de los Medicos disponibles, sus especialidades y turnos de trabajo.
                        </p>
                        <%--Esteban podemos sombrear este boton para que se noten los 2 distintos links mas rapido?--%>
                        <a href="<%: ResolveUrl("~/Pacientes/Nuevo.aspx") %>"
                            class="btn btn-primary mt-auto"
                            aria-label="Buscar Medico">Buscar Medicos en el sistema
                        </a>
                    </div>
                </div>
            </div>


        </div>
    </div>

    <div class="container-fluid py-5">
        <%--        Aca vamos a hacer un DATAGRID con los turnos proximos, podemos ponerlos color coded dependiendo de lo proximo que esten,
        o podemos intercalar colores para mejorar la visibilidad--%>
        <div class="px-3">
            <h2 class="card-title mb-2">Turnos Proximos</h2>
        </div>
        <div class="d-flex flex-column flex-md-row justify-content-between align-items-start align-items-md-center gap-3 p-3">
            <div class="text-soft alert alert-primary">
                No hay turnos disponibles por ahora.
            </div>
            <div class="ms-md-auto">
                <a href="<%: ResolveUrl("~/Turnos/Crear.aspx") %>" class="btn btn-primary">Crear turno
                </a>
            </div>
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
                                <a href="<%: ResolveUrl("~/Turnos/Crear.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Agregar nuevo Medico
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Modificar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Modificar datos de un Medico
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Cancelar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center"> Listar Medicos Disponibles
                                </a>
                            </div>
                            <div class="col py-1">
                                <a href="<%: ResolveUrl("~/Turnos/Buscar.aspx") %>"
                                    class="btn btn-outline-primary w-100 py-3 fw-semibold d-flex align-items-center justify-content-center">Baja a un Medico
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
