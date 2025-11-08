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


            <asp:Button ID="btnMostrar" runat="server" Text="Cargar nuevo medico" CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnMostrar_Click" />
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


                            <div class="col-md-4">
                                <label for="txtNombreHtml" class="form-label">Nombre</label>

                                <asp:TextBox ID="txtNombreMedico" runat="server" CssClass="form-control" OnTextChanged="txtNombreMedico_TextChanged" />
                            </div>


                            <div class="col-md-4">
                                <label for="txtApellidoHtml" class="form-label">Apellido</label>

                                <asp:TextBox ID="txtApellidoMedico" runat="server" CssClass="form-control" OnTextChanged="txtApellidoMedico_TextChanged" />
                            </div>

                            <div class="col-md-4">
                                <label for="txtMatriculaHtml" class="form-label">Matrícula</label>

                                <asp:TextBox ID="txtMatriculaMedico" runat="server" CssClass="form-control" OnTextChanged="txtMatriculaMedico_TextChanged" />
                            </div>


                            <div class="col-md-4">
                                <label for="ddlTurnoHtml" class="form-label">Turno de trabajo</label>
                                <asp:DropDownList ID="ddllistTurnoTrabajo" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>


                            <div class="col-md-8">
                                <label class="form-label d-block">Especialidades</label>
                                <asp:DropDownList ID="DdlistEspecilidad" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label">Hora Inicio</label>
                                <asp:TextBox ID="txtHoraInicio" runat="server" TextMode="Time" CssClass="form-control" />

                            </div>

                            <div class="col-md-4">
                                <label class="form-label">Hora Fin</label>
                                <asp:TextBox ID="txtHoraFin" runat="server" TextMode="Time" CssClass="form-control" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <hr class="my-4" />
        </asp:Panel>

        <!-- BOTONES -->
        <div class="d-flex gap-2">



            <asp:Button ID="btnGuardarMedico" runat="server" Text="💾 Guardar Medico" CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnGuardarMedico_Click" />



            <asp:Button ID="btnBotonLimpiarMedico" runat="server" Text=" 🧹 Limpiar" CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnBotonLimpiarMedico_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="↩️ Cancelar" CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnCancelar_Click" />
        </div>

        <!-- CONTENEDOR DEL FILTRO -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <strong>Buscar Médico</strong>
            </div>

            <div class="card-body">
                <div class="row align-items-center">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFiltrarMedico" runat="server" CssClass="form-control rounded-end" placeholder="Buscar por Nombre o apellido" OnTextChanged="txtFiltrarMedico_TextChanged" AutoPostBack="true"></asp:TextBox>

                    </div>
                </div>
            </div>
        </div>

        <asp:Panel ID="panelEliminar" runat="server" Visible="false">


            <style>
                .btn-eliminar {
                    background-color: #dc3545;
                    color: #fff;
                    border: none;
                    border-radius: 8px;
                    padding: 10px 20px;
                    font-weight: 600;
                    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
                    transition: all 0.3s ease;
                }

                    .btn-eliminar:hover {
                        background-color: #bb2d3b;
                        transform: scale(1.05);
                        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.25);
                    }
            </style>

            <asp:Label ID="lblEliminarLogicamente" runat="server"  CssClass="fw-semibold fs-5 text-dark d-block mb-3" Text="Desea Eliminar este Medico?" />


            <asp:Button ID="txtEliminarLgocimante" runat="server" CssClass="btn btn-danger rounded-pill px-4 py-2 fw-semibold shadow-sm" Text="Si,eliminar" OnClick="txtEliminarLgocimante_Click" />

            <asp:Button ID="txtNoeleiminarlogicamente" runat="server"  CssClass="btn btn-outline-primary rounded-pill px-4 py-2 fw-semibold ms-2" Text="No deseo Eliminarlo" OnClick="txtNoeleiminarlogicamente_Click" />

       

            <asp:Label ID="lblEliminado" runat="server"   CssClass="alert alert-success fw-semibold rounded-pill px-4 py-2 shadow-sm d-inline-block mt-2" Text="Eliminado correctamente" />

            <asp:Label ID="lblEminadoEror" runat="server" CssClass="alert alert-danger d-inline-block mt-2" Text="Error al eliminar" />

            <asp:Label ID="lblModificao" runat="server" CssClass="alert alert_success d-inline-block mt-2"  />

            <asp:Label ID="lblEroorGuardar" runat="server" CssClass="alert alert_success d-inline-block mt-2"  />

            <asp:Button ID="btnVolver" runat="server"  CssClass="btn btn-outline-primary rounded-pill px-4 py-2 fw-semibold" Text="Volver" OnClick="btnVolver_Click" />

        </asp:Panel>



        <!-- ===================== LISTADO DE MÉDICOS ===================== -->
        <div class="card shadow-sm">
            <div class="card-header bg-light">
                <strong>Resultados de la búsqueda</strong>
            </div>

            <div class="card-body p-0">

                <asp:GridView ID="gvMedicos" runat="server" EmptyDataText="No hay datos" CssClass="table table-hover mb-0 align-middle"
                    HeaderStyle-CssClass="table-light" AutoGenerateColumns="false" DataKeyNames="IdMedico" 
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMedicos_PageIndexChanging"
                    OnRowEditing="gvMedicos_RowEditing" OnRowUpdating="gvMedicos_RowUpdating" 
                    OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDeleting="gvMedicos_RowDeleting"
                    OnRowDataBound="gvMedicos_RowDataBound" OnRowCommand="gvMedicos_RowCommand1">

                    <Columns>




                        <asp:TemplateField HeaderText="Idmedico" Visible="false" >
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
                            <EditItemTemplate>

                                <asp:TextBox ID="txtHoraInicioEdit" runat="server"
                                    CssClass="form-control d-inline-block"
                                    Style="width: 80px">
                                   

                                </asp:TextBox>
                                &nbsp;a&nbsp;

                             <asp:TextBox ID="txtHoraFinEdit" runat="server"
                                 CssClass="form-control d-inline-block"
                                 Style="width: 80px">
                                

                             
                             </asp:TextBox>

                            </EditItemTemplate>
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Especialidades">
                            <ItemTemplate>
                                <asp:Repeater ID="RepEsp" runat="server" DataSource='<%# Eval("Especialidades") %>'>
                                    <ItemTemplate><%# Eval("Nombre") %></ItemTemplate>
                                    <SeparatorTemplate>, </SeparatorTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBoxList ID="cblEspEdit" runat="server" DataTextField="Nombre" DataValueField="IdEspecialidad" RepeatDirection="Horizontal" RepeatColumns="5" CssClass="form-check form-check-inline" />

                            </EditItemTemplate>
                        </asp:TemplateField>






                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>

                                <asp:Button ID="btnEditar" runat="server" Text="✏️ Modificar"
                                    CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Edit" />

                                <asp:Button ID="btnModificar" runat="server" Text="💾 Guardar Cambios"
                                    CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Update" />

                                <asp:Button ID="btnCancelar" runat="server" Text="↩️ Cancelar Edicion"
                                    CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Cancel" />





                                <asp:Button ID="btnEliminar" runat="server" Text="🗑️ Eliminar" OnClick="btnEliminar_Click"
                                    CssClass="btn btn-sm btn-outline-danger mx-1"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("IdMedico") %>' />
                            </ItemTemplate>




                        </asp:TemplateField>


                        














                    </Columns>



                </asp:GridView>

                <asp:Button ID="btnCrearUsuario" runat="server" CssClass="btn btn-outline-primary" Text="Crear Usuario" OnClick="btnCrearUsuario_Click" />


            </div>
        </div>
    </div>

    <script type="text/javascript">
        var __filterTimer = null;
        function liveFilter(uniqueId) {
            if (__filterTimer) clearTimeout(__filterTimer);
            __filterTimer = setTimeout(function () {
                __doPostBack(uniqueId, '');
            }, 300);
        }
    </script>




</asp:Content>
