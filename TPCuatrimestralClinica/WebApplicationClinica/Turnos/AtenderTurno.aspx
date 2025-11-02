<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="AtenderTurno.aspx.cs" Inherits="WebApplicationClinica.Turnos.AtenderTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-center my-4">Atención de Turnos</h1>


    <main class="min-vh-100 d-flex align-items-stretch py-4">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-12 col-xl-10">
                    <div class="card shadow-lg border-0 rounded-3 w-100">


                        <%--Cuerpo con scroll interno --%>
                        <div class="card-body p-4 p-md-5 overflow-auto">

                            <%--Botonera de Acciones--%>
                            <div class="row g-3">
                                <%-- Fila 1: tres botones--%>
                                <div class="col-12 col-sm-4">
                                    <a class="btn btn-outline-primary w-100">Llamar Paciente</a>
                                </div>
                                <div class="col-12 col-sm-4">
                                    <a class="btn btn-outline-primary w-100">Reprogramar Turno</a>
                                </div>
                                <div class="col-12 col-sm-4">
                                    <a class="btn btn-outline-primary w-100">Cancelar Turno</a>
                                </div>

                                <%--Fila 2: CTA--%>
                                <div class="d-grid gap-2">
                                    <a class="btn btn-primary d-block mx-auto w-75">Atender Paciente</a>
                                </div>
                            </div>

                            <div class="border-top border border-secondary-subtle opacity-50 my-4"></div>

                            <%--Lectura de datos para el proximo Turno, deshabilitados--%>

                            <h4 class="text-left my-4">Datos del proximo Turno</h4>


                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                                    <asp:TextBox ID="txtNombre" runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control-plaintext text-body fw-semibold"
                                        ToolTip="Solo lectura"></asp:TextBox>
                                </div>

                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                                    <asp:TextBox ID="txtApellido" runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control-plaintext text-body fw-semibold"
                                        ToolTip="Solo lectura"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="Label3" runat="server" Text="Edad"></asp:Label>
                                    <asp:TextBox ID="txtEdad" runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control-plaintext text-body fw-semibold"
                                        ToolTip="Solo lectura"></asp:TextBox>
                                </div>

                                <div class="col-md-6 mb-3">
                                    <asp:Label ID="Label4" runat="server" Text="Tipo de Cobertura"></asp:Label>
                                    <asp:TextBox ID="txtCobertura" runat="server"
                                        ReadOnly="true"
                                        CssClass="form-control-plaintext text-body fw-semibold"
                                        ToolTip="Solo lectura"></asp:TextBox>
                                </div>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="Label7" runat="server" Text="Observaciones Solicitud :"></asp:Label>
                                <asp:TextBox ID="txtSolicitud" TextMode="MultiLine" Rows="3" runat="server"
                                    ReadOnly="true"
                                    CssClass="form-control-plaintext text-body fw-semibold"
                                    ToolTip="Solo lectura"></asp:TextBox>
                            </div>

                            <%--Campo Diagnostico para el medico, habilitado--%>
                            <div class="mb-3">
                                <asp:Label ID="Label8" runat="server" Text="Observaciones Diagnostico :"></asp:Label>
                                <asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </main>


</asp:Content>
