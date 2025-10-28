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
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Gestion Turnos
                    </button>
                </h2>
                <div id="collapseTurno" class="accordion-collapse collapse" data-bs-parent="#accordionTurnos">
                    <div class="accordion-body">
                        <%--Elementos dentro del acordeon--%>
                    </div>
                </div>
            </div>

            <%--Accordeon Pacientes--%>
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Gestion Pacientes
                    </button>
                </h2>
                <div id="collapsePaciente" class="accordion-collapse collapse" data-bs-parent="#accordionPacientes">
                    <div class="accordion-body">
                        <%--Elementos dentro del acordeon--%>
                    </div>
                </div>
            </div>

            <%--Accordeon Medicos--%>
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                        Gestion Medicos
                    </button>
                </h2>
                <div id="collapseMedicos" class="accordion-collapse collapse" data-bs-parent="#accordionMedicos">
                    <div class="accordion-body">
                        <%--Elementos dentro del acordeon--%>
                    </div>
                </div>
            </div>


        </div>
    </div>


</asp:Content>
