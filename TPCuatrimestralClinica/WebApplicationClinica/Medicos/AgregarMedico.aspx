<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="AgregarMedico.aspx.cs" Inherits="WebApplicationClinica.Medicos.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- ===================== CONTENEDOR PRINCIPAL ===================== -->
    <div class="container py-4">

        <!-- ===================== ENCABEZADO ===================== -->
        <div class="d-flex align-items-center justify-content-between mb-4">
            <div>
                <h2 class="mb-1">Gestión de Médicos</h2>
                <p class="text-muted mb-0">Alta de médicos y asignación de turno + especialidades</p>
            </div>
            <%-- ASP: Reemplazar por <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-outline-secondary" OnClick="btnVolver_Click" /> --%>
            <button type="button" class="btn btn-outline-secondary">
                ← Volver
            </button>
        </div>

        <!-- ===================== CARD FORMULARIO ===================== -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <strong>Nuevo Médico</strong>
            </div>
            <div class="card-body">
                <div class="row g-3">

                    <%-- ASP: Reemplazar por <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" /> --%>
                    <div class="col-md-4">
                        <label for="txtNombreHtml" class="form-label">Nombre</label>
                        <input id="txtNombreHtml" type="text" class="form-control" placeholder="Ej: Matías" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" /> --%>
                    <div class="col-md-4">
                        <label for="txtApellidoHtml" class="form-label">Apellido</label>
                        <input id="txtApellidoHtml" type="text" class="form-control" placeholder="Ej: Gómez" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" /> --%>
                    <div class="col-md-4">
                        <label for="txtMatriculaHtml" class="form-label">Matrícula</label>
                        <input id="txtMatriculaHtml" type="text" class="form-control" placeholder="Ej: M-12345" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:DropDownList ID="ddlTurno" runat="server" CssClass="form-select"> --%>
                    <div class="col-md-4">
                        <label for="ddlTurnoHtml" class="form-label">Turno de trabajo</label>
                        <select id="ddlTurnoHtml" name="ddlTurnoHtml" class="form-select">
                            <option value="">Seleccionar...</option>
                            <option value="1">Mañana</option>
                            <option value="2">Tarde</option>
                            <option value="3">Noche</option>
                        </select>
                    </div>

                    <%-- ASP: Reemplazar por <asp:CheckBoxList ID="chkEspecialidades" runat="server" CssClass="d-flex flex-wrap gap-3" /> --%>
                    <div class="col-md-8">
                        <label class="form-label d-block">Especialidades</label>
                        <asp:DropDownList ID="DdlistEspecilidad" runat="server" CssClass="form-select"  ></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="my-4" />

                <!-- BOTONES -->
                <div class="d-flex gap-2">
                    <%-- ASP: Reemplazar por <asp:Button ID="btnGuardar" runat="server" Text="Guardar médico" CssClass="btn btn-success" OnClick="btnGuardar_Click" /> --%>
                    <button type="button" class="btn btn-success">
                        Guardar médico
                    </button>

                    <%-- ASP: Reemplazar por <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" /> --%>
                    <button type="button" class="btn btn-outline-secondary">
                        Limpiar
                    </button>
                </div>
            </div>
        </div>

        <!-- ===================== LISTADO DE MÉDICOS ===================== -->
        <div class="card shadow-sm">
            <div class="card-header bg-light">
                <strong>Médicos cargados</strong>
            </div>
            <div class="card-body p-0">
                <%-- ASP: Reemplazar esta tabla por <asp:GridView ID="gvMedicos" runat="server" CssClass="table table-hover mb-0" AutoGenerateColumns="False" OnRowCommand="gvMedicos_RowCommand"> --%>
                <div class="table-responsive">
                    <table class="table table-hover mb-0 align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Matrícula</th>
                                <th>Turno</th>
                                <th">Horario</th>
                                <th>Especialidades</th>
                                
                            </tr>
                        </thead>
                        <tbody>
                            <!-- FILA DE EJEMPLO (BORRAR CUANDO CARGUES DESDE BD) -->
                            <tr>
                             
                            </tr>
                            <!-- ASP: Las filas se van a generar dinámicamente -->
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="card-footer d-flex justify-content-between align-items-center">
                <%-- ASP: Agregar paginación si usás GridView con AllowPaging="True" --%>
                <small class="text-muted">Mostrando 1 de 1</small>
                <div class="btn-group">
                    <button type="button" class="btn btn-sm btn-outline-secondary">Anterior</button>
                    <button type="button" class="btn btn-sm btn-outline-secondary">Siguiente</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
