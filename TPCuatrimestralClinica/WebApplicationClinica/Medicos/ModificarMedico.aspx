<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="ModificarMedico.aspx.cs" Inherits="WebApplicationClinica.Medicos.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- ===================== CONTENEDOR PRINCIPAL ===================== -->
    <div class="container py-4">

        <!-- ===================== ENCABEZADO ===================== -->
        <div class="d-flex align-items-center justify-content-between mb-4">
            <div>
                <h2 class="mb-1">Buscar y Modificar Médico</h2>
                <p class="text-muted mb-0">Usá el filtro para encontrar el médico que querés editar</p>
            </div>
        </div>

        <!-- ===================== BUSCADOR ===================== -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <strong>Filtro de búsqueda</strong>
            </div>
            <div class="card-body">
                <div class="row g-3 align-items-end">
                    <div class="col-md-6">
                        <%-- ASP: Reemplazar por <asp:TextBox ID="txtBuscarMedico" runat="server" CssClass="form-control" Placeholder="Ingresá el nombre o apellido..." /> --%>
                        <label for="txtBuscarMedicoHtml" class="form-label">Nombre o Apellido del médico</label>
                        
                        <asp:TextBox ID="txtBuscarMedico" runat ="server" AutoPostBack="true" CssClass ="form-label" OnTextChanged="txtBuscarMedico_TextChanged"  />
                    </div>

                    <div class="col-md-3">
                        <%-- ASP: Reemplazar por <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="btnBuscar_Click" /> --%>
                       
                        <asp:Button ID="btnBuscarMedico" runat="server" CssClass="btn btn-primary w-100" Text="Buscar" OnClick="btnBuscarMedico_Click" />
                    </div>

                    <div class="col-md-3">
                        <%-- ASP: Reemplazar por <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiarFiltro_Click" /> --%>
                      
                        <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-outline-secondary w-100" Text="Limpiar" OnClick="btnLimpiar_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ===================== RESULTADOS ===================== -->
        <div class="card shadow-sm">
            <div class="card-header bg-light">
                <strong>Resultados de la búsqueda</strong>
            </div>

            <div class="card-body p-0">


                <asp:GridView ID="gvMedicos" runat="server" EmptyDataText="hoy hay nada" CssClass="table table-hover mb-0 align-middle" HeaderStyle-CssClass="table-light" AutoGenerateColumns="false"
                    DataKeyNames="Idmedico" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMedicos_PageIndexChanging" OnRowEditing="gvMedicos_RowEditing"
                    OnRowUpdating="gvMedicos_RowUpdating" OnRowCancelingEdit="gvMedicos_RowCancelingEdit">


                    <Columns>



                        <asp:TemplateField   HeaderText="Idmedico">
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
                                <asp:DropDownList ID="ddlTurnodit" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="IdTurnoTrabajo">
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
                            EditText="Modificar" />
                    </Columns>





                </asp:GridView>
            </div>
        </div>
</asp:Content>
