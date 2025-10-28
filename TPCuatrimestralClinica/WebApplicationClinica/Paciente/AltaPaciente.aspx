<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="AltaPaciente.aspx.cs" Inherits="WebApplicationClinica.Pacientes.AltaPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="text-center my-4">Alta Paciente</h1>

    <main class="min-vh-100 d-flex align-items-stretch py-4">
        <div class="container-fluid">
            <div class="row justify-content-center">
                <div class="col-12 col-xl-10">
                    <div class="card shadow-lg border-0 rounded-3 w-100">
                        <%--Cabecera fija--%>
                        <div class="card-header bg-white sticky-top">
                            <h4 class="mb-0 text-center">Ingrese los datos del paciente</h4>
                        </div>

                        <%--Cuerpo con scroll interno --%>

                        <%--Documento--%>
                        <div class="card-body p-4 p-md-5 overflow-auto">
                            <label for="txtDocumento" class="form-label">Documento</label>
                            <div class="d-flex align-items-center gap-3 flex-wrap mb-4">
                                <asp:TextBox runat="server" ID="txtDocumento" CssClass="form-control w-25"
                                    required="required" pattern="\d+" inputmode="numeric" />
                                <asp:Label runat="server" ID="lblPacienteEstado" CssClass="badge bg-secondary" Text="Sin Verificar"></asp:Label>
                            </div>

                            <div class="row g-3">
                                <%--Nombre--%>
                                <div class="col-12 col-md-4">
                                    <label for="txtNombre" class="form-label">Nombre</label>
                                    <asp:TextBox runat="server" ID="txtNombre"
                                        ClientIDMode="Static"
                                        CssClass="form-control"
                                        inputmode="text" />
                                </div>

                                <%--Apellido--%>
                                <div class="col-12 col-md-4">
                                    <label for="txtApellido" class="form-label">Apellido</label>
                                    <asp:TextBox runat="server" ID="txtApellido"
                                        ClientIDMode="Static"
                                        CssClass="form-control"
                                        inputmode="text" />
                                </div>
                                <div class="col-12 col-md-4">
                                    <label for="dtFechaNacimiento" class="form-label">Fecha de Nacimiento</label>
                                    <input type="date" id="dtFechaNacimiento" class="form-control" />
                                </div>
                            </div>

                            <div class="row g-3 align-items-end py-2">
                                <%--Email--%>
                                <div class="col-12 col-md-4">
                                    <label for="txtEmailLocal" class="form-label">Correo electrónico</label>
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtEmailLocal" CssClass="form-control" placeholder="usuario" />
                                        <span class="input-group-text">@</span>
                                        <asp:TextBox runat="server" ID="txtEmailDomain" CssClass="form-control" placeholder="dominio.com" />
                                    </div>
                                </div>

                                <%--Teléfono--%>
                                <div class="col-12 col-md-4">
                                    <label for="txtTelefono" class="form-label">Número de teléfono</label>
                                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" inputmode="tel" placeholder="+54911584628" />
                                </div>
                            </div>
                            <div class="row g-3 align-items-end">
                                <%--Direccion--%>
                                <div class="col-12 col-md-8">
                                    <label for="txtObservaciones" class="form-label">Direccion</label>
                                    <asp:TextBox runat="server" ID="txtObservaciones"
                                        ClientIDMode="Static"
                                        TextMode="MultiLine"
                                        Rows="2"
                                        CssClass="form-control"
                                        placeholder="Calle StakeHolder 24..."></asp:TextBox>
                                </div>
                                <div class="row g-3 align-items-end">

                                    <%--Tipo Cobertura --%>
                                    <div class="col-12 col-md-4">
                                        <label class="form-label d-block mb-2">Tipo de Cobertura</label>
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton runat="server" ID="rbPrepaga" GroupName="Cobertura" CssClass="form-check-input" />
                                            <label class="form-check-label" for="<%= rbPrepaga.ClientID %>">Prepaga</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton runat="server" ID="rbObraSocial" GroupName="Cobertura" CssClass="form-check-input" />
                                            <label class="form-check-label" for="<%= rbObraSocial.ClientID %>">Obra social</label>
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton runat="server" ID="rbNinguno" GroupName="Cobertura" CssClass="form-check-input" />
                                            <label class="form-check-label" for="<%= rbObraSocial.ClientID %>">Ninguno</label>
                                        </div>
                                    </div>

                                    <%--Num. afiliado--%>
                                    <div class="col-12 col-md-4">
                                        <label for="txtNroAfiliado" class="form-label">Número de afiliado</label>
                                        <asp:TextBox runat="server" ID="txtNroAfiliado"
                                            ClientIDMode="Static"
                                            CssClass="form-control"
                                            inputmode="numeric" pattern="[\d\- ]*"
                                            placeholder="Ej.: 12345678" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Pie (botones) fijo--%>
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
    </main>




</asp:Content>
