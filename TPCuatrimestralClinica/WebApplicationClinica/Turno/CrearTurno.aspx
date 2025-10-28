<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="CrearTurno.aspx.cs" Inherits="WebApplicationClinica.CrearTurno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-center my-4">Crear Nuevo Turno</h1>

    <main class="min-vh-100 d-flex align-items-stretch py-4">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-12 col-xl-10">
                    <div class="card shadow-lg border-0 rounded-3 w-100">
                        <%--Cabecera opcional fija--%>
                        <div class="card-header bg-white sticky-top">
                            <h4 class="mb-0 text-center">Ingrese el detalle del turno</h4>
                        </div>

                        <%--Cuerpo con scroll interno --%>
                        <div class="card-body p-4 p-md-5 overflow-auto">
                            <label for="txtDocumento" class="form-label">Documento</label>
                            <div class="d-flex align-items-center gap-3 flex-wrap mb-4">
                                <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control w-25"
                                    required="required" pattern="\d+" inputmode="numeric" />

                                <asp:Label runat="server" ID="lblPacienteEstado" CssClass="badge bg-secondary" Text="Pendiente"></asp:Label>

                                <asp:Button runat="server" ID="btnAgregarPaciente"
                                    CssClass="btn btn-outline-secondary btn-sm"
                                    Text="Agregar paciente" UseSubmitBehavior="false" />
                            </div>


                            <div class="row g-3 align-items-end">
                                <%--Especialidad--%>
                                <div class="col-12 col-md-4">
                                    <label for="ddlEspecialidad" class="form-label">Especialidad</label>
                                    <asp:DropDownList runat="server" ID="ddlEspecialidad"
                                        ClientIDMode="Static"
                                        CssClass="form-select"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="">Seleccione una especialidad…</asp:ListItem>
                                        <asp:ListItem Value="GEN">Medicina General</asp:ListItem>
                                        <asp:ListItem Value="CAR">Cardiología</asp:ListItem>
                                        <asp:ListItem Value="PED">Pediatría</asp:ListItem>
                                        <asp:ListItem Value="OFT">Oftalmología</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <%--Médico--%>
                                <div class="col-12 col-md-4">
                                    <label for="ddlMedicoDisponible" class="form-label">Médico</label>
                                    <asp:DropDownList runat="server" ID="ddlMedicoDisponible"
                                        ClientIDMode="Static"
                                        CssClass="form-select"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="">Seleccione un médico…</asp:ListItem>
                                        <asp:ListItem Value="001">Andres Garcia</asp:ListItem>
                                        <asp:ListItem Value="002">Calamaro Fuentes</asp:ListItem>
                                        <asp:ListItem Value="003">SinVerdad Gonzalez</asp:ListItem>
                                        <asp:ListItem Value="004">Vallano hijo-hijo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <%--Fecha--%>
                                <div class="col-12 col-md-4">
                                    <label for="dtFechaTurno" class="form-label">Fecha</label>
                                    <input type="date" id="dtFechaTurno" class="form-control" />
                                </div>
                            </div>

                            <div class="row mt-4">
                                <div class="col-12">
                                    <label for="txtObservaciones" class="form-label">Observaciones</label>
                                    <asp:TextBox runat="server" ID="txtObservaciones"
                                        ClientIDMode="Static"
                                        TextMode="MultiLine"
                                        Rows="6"
                                        CssClass="form-control"
                                        placeholder="Notas u observaciones relevantes…"></asp:TextBox>
                                    <div class="form-text">Detalla indicaciones o restricciones.</div>
                                </div>
                            </div>


                        </div>

                        <%--Pie opcional (botones) fijo--%>
                        <div class="card-footer bg-white sticky-bottom">
                            <div class="d-flex justify-content-end gap-2">
                                <asp:Button runat="server" ID="btnGuardar"
                                    CssClass="btn btn-primary"
                                    Text="Guardar" />

                                <asp:Button runat="server" ID="btnCancelar"
                                    CssClass="btn btn-secondary"
                                    Text="Cancelar"
                                    CausesValidation="false" UseSubmitBehavior="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
