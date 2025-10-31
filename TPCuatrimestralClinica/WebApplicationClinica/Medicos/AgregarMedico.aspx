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

                        <asp:TextBox ID="txtNombreMedico" runat="server" CssClass="form-control" OnTextChanged="txtNombreMedico_TextChanged" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" /> --%>
                    <div class="col-md-4">
                        <label for="txtApellidoHtml" class="form-label">Apellido</label>

                        <asp:TextBox ID="txtApellidoMedico" runat="server" CssClass="form-control" OnTextChanged="txtApellidoMedico_TextChanged" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" /> --%>
                    <div class="col-md-4">
                        <label for="txtMatriculaHtml" class="form-label">Matrícula</label>

                        <asp:TextBox ID="txtMatriculaMedico" runat="server" CssClass="form-control" OnTextChanged="txtMatriculaMedico_TextChanged" />
                    </div>

                    <%-- ASP: Reemplazar por <asp:DropDownList ID="ddlTurno" runat="server" CssClass="form-select"> --%>
                    <div class="col-md-4">
                        <label for="ddlTurnoHtml" class="form-label">Turno de trabajo</label>
                        <asp:DropDownList ID="ddllistTurnoTrabajo" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <%-- ASP: Reemplazar por <asp:CheckBoxList ID="chkEspecialidades" runat="server" CssClass="d-flex flex-wrap gap-3" /> --%>
                    <div class="col-md-8">
                        <label class="form-label d-block">Especialidades</label>
                        <asp:DropDownList ID="DdlistEspecilidad" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <hr class="my-4" />

        <!-- BOTONES -->
        <div class="d-flex gap-2">
            <%-- ASP: Reemplazar por <asp:Button ID="btnGuardar" runat="server" Text="Guardar médico" CssClass="btn btn-success" OnClick="btnGuardar_Click" /> --%>


            <asp:Button ID="btnGuardarMedico" runat="server" Text="Guardar Medico" CssClass="bnt btn-success" OnClick="btnGuardarMedico_Click" />

            <%-- ASP: Reemplazar por <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" /> --%>
            <button type="button" class="btn btn-outline-secondary">
                Limpiar
            </button>
            <asp:Button ID="btnBotonLimpiarMedico" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnBotonLimpiarMedico_Click" />
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
                <asp:GridView ID="dvMedicos" runat="server" CssClass ="table table-hover mb-0 align-middle" AutoGenerateColumns="false" EmptyDataText=" Nohaymedicos" DataKeyNames="IdMedico" AllowPaging="true" PageSize="10" OnPageIndexChanging="dvMedicos_PageIndexChanging">
                  
                    <Columns>
                       <asp:BoundField DataField ="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField ="Apellido" HeaderText="Apellido" />
                   <asp:BoundField DataField ="Matricula" HeaderText="Matricula" />


                        <asp:TemplateField HeaderText="Turno">
                            <ItemTemplate><%# Eval("TurnoTrabajo.Nombre")  %></ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Horario">
                            <ItemTemplate>
                         <%# String.Format("{0:hh\\:mm}-{1:hh\\:mm}",
                       Eval("TurnoTrabajo.HoraInicio"), Eval("TurnoTrabajo.HoraFin")) %>
                        </ItemTemplate>
                      </asp:TemplateField>

                        <asp:TemplateField HeaderText="Especialidades">
                            <ItemTemplate>
                                <asp:Repeater ID="RepEsp" runat="server" DataSource='<%# Eval("Especialidades") %>'>
                                    <ItemTemplate><%# Eval("Nombre") %></ItemTemplate>
                                    <SeparatorTemplate> , </SeparatorTemplate>
                         </asp:Repeater>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                          </asp:GridView>
            </div>
            </div>
        </div>
            
        
</asp:Content>
