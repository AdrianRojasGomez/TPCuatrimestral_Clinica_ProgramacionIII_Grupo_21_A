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


            <asp:Button ID="btnMostrar" runat="server" Text="Cargar nuevo medico" CssClass="btn-outline-secondary" OnClick="btnMostrar_Click" />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold" />
        </div>

        <!-- ===================== CARD FORMULARIO ===================== -->
        <asp:Panel ID="panelGrillaMedico" runat="server" Visible="false">
            <div class="card shadow-sm">
                <div class="card-header bg-light"><strong>Medicos cargados</strong></div>

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
        </asp:Panel>

        <!-- BOTONES -->
        <div class="d-flex gap-2">



            <asp:Button ID="btnGuardarMedico" runat="server" Text="Guardar Medico" CssClass="bnt btn-success" OnClick="btnGuardarMedico_Click" />



            <asp:Button ID="btnBotonLimpiarMedico" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnBotonLimpiarMedico_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-outline-secondary" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <!-- CONTENEDOR DEL FILTRO -->
<div class="card shadow-sm mb-4">
    <div class="card-header bg-primary text-white">
        <strong>Buscar Médico</strong>
    </div>

    <div class="card-body">
        <div class="row align-items-center">
            <div class="col-md-6">
    <asp:TextBox ID="txtFiltrarMedico" runat="server" CssClass="" OnTextChanged="txtFiltrarMedico_TextChanged" AutoPostBack="true"></asp:TextBox>
              
            </div>
        </div>
    </div>
</div>



    <!-- ===================== LISTADO DE MÉDICOS ===================== -->
    <div class="card shadow-sm">
        <div class="card-header bg-light">
            <strong>Resultados de la búsqueda</strong>
        </div>

        <div class="card-body p-0">

            <asp:GridView ID="gvMedicos" runat="server" EmptyDataText="No hay datos" CssClass="table table-hover mb-0 align-middle" HeaderStyle-CssClass="table-light" AutoGenerateColumns="false" DataKeyNames="IdMedico" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMedicos_PageIndexChanging"
                OnRowEditing="gvMedicos_RowEditing" OnRowUpdating="gvMedicos_RowUpdating" OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDeleting="gvMedicos_RowDeleting">

                <Columns>



                    <asp:TemplateField HeaderText="Idmedico">
                        <ItemTemplate><%# Eval("IdMedico") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblIdMedico" runat="server" Text='<%# Eval("IdMedico") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate><%# Eval("Nombre") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNombreEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Nombre") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Apellido">
                        <ItemTemplate><%# Eval("Apellido") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtApellidoEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Apellido") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Matricula">
                        <ItemTemplate><%# Eval("Matricula") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMatriculaEdit" runat="server" CssClass="form-control"
                                Text='<%# Bind("Matricula") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>







                    <asp:TemplateField HeaderText="Turno">
                        <ItemTemplate><%# Eval ("TurnoTrabajo.Nombre") %> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTurnoEdit" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="IdTurnoTrabajo">
                            </asp:DropDownList>
                        </EditItemTemplate>
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
                                <SeparatorTemplate>, </SeparatorTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBoxList ID="cblEspEdit" runat="server" DataTextField="Nombre" DataValueField="IdEspecialidad" RepeatDirection="Horizontal" />
                        </EditItemTemplate>
                    </asp:TemplateField>














                    <asp:CommandField ShowEditButton="true"
                        EditText="Modificar" ShowDeleteButton="true" DeleteText="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary mx-1" />
                </Columns>


            </asp:GridView>


        </div>
    </div>



</asp:Content>
