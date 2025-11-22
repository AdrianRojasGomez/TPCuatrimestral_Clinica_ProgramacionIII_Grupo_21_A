
<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="AgregarMedico.aspx.cs" Inherits="WebApplicationClinica.Medicos.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .dias-semana input[type="checkbox"] {
            margin-right: 5px;
        }

        .dias-semana label {
            margin-right: 20px;
            display: inline-block;
        }
    </style>

    <!-- ===================== CONTENEDOR PRINCIPAL ===================== -->
    <div class="container py-4">
        <!-- ===================== ENCABEZADO ===================== -->
        <div class="d-flex align-items-center justify-content-between mb-4">
            <div>
                
                <asp:Label ID="lblMedicoAdmin" runat="server" Text="Gestión de Médicos" class="mb-1 text-success fw-bold" />

                <asp:Label ID="lblMedicoRecepcion" runat="server" class="mb-1 text-success fw-bold"  Text="Lista de Médicos" /> 

                <asp:Label ID="lblAdminSubtitulo" runat="server" Text="Alta de médicos y asignación de turno + especialidades" 
                    CssClass="text-success mb-0 opacity-75 d-block" />
                <asp:Label ID="lblRecepcionSubTitulo" runat="server" Text="Acceso de solo lectura para Recepción" 
                    class="text-success mb-0 opacity-75 d-block" /> 

            </div>



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
                                <label for="txtNombreHtml" class="form-label text-danger fw-semibold">Nombre</label>

                                <asp:TextBox ID="txtNombreMedico" runat="server" CssClass="form-control" OnTextChanged="txtNombreMedico_TextChanged" />
                            </div>


                            <div class="col-md-4">
                                <label for="txtApellidoHtml" class="form-label text-danger fw-semibold">Apellido</label>

                                <asp:TextBox ID="txtApellidoMedico" runat="server" CssClass="form-control" OnTextChanged="txtApellidoMedico_TextChanged" />
                            </div>

                            <div class="col-md-4">
                                <label for="txtMatriculaHtml" class="form-label text-danger fw-semibold">Matrícula</label>

                                <asp:TextBox ID="txtMatriculaMedico" runat="server" CssClass="form-control" OnTextChanged="txtMatriculaMedico_TextChanged" />
                            </div>

                            <div class="row align-items-end">
                                <div class="col-md-4">
                                    <label for="ddlTurnoHtml" class="form-label text-danger fw-semibold">Turno de trabajo</label>
                                    <asp:DropDownList ID="ddllistTurnoTrabajo" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddllistTurnoTrabajo_SelectedIndexChanged" Style="width: 250px"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">

                                    <label class="form-label text-danger fw-semibold">Hora Inicio</label>
                                    <asp:TextBox ID="txtHoraInicio" runat="server" TextMode="Time"
                                        CssClass="form-control" Style="width: 100px" />
                                </div>




                                <div class="col-md-2">
                                    <label class="form-label text-danger fw-semibold">Hora Fin</label>
                                    <asp:TextBox ID="txtHoraFin" runat="server" TextMode="Time" CssClass="form-control" Style="width: 100px" />

                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="col-md-4" style="margin-left: 100px;">
                    </div>
                    <label class="form-label text-danger fw-semibold">Días de la semana</label>
                    <asp:CheckBoxList ID="cblDiasSemanaNuevo" runat="server" CssClass="dias-semana"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Text="Lunes" Value="1" />
                        <asp:ListItem Text="Martes" Value="2" />
                        <asp:ListItem Text="Miércoles" Value="3" />
                        <asp:ListItem Text="Jueves" Value="4" />
                        <asp:ListItem Text="Viernes" Value="5" />
                        <asp:ListItem Text="Sábado" Value="6" />
                        <asp:ListItem Text="Domingo" Value="7" />
                    </asp:CheckBoxList>
                </div>

                <div class="col-md-8">
                    <label class="form-label text-danger fw-semibold">Especialidades</label>
                    <asp:CheckBoxList ID="cblEspecialidades" runat="server" CssClass="form-check"
                        RepeatDirection="Vertical">
                    </asp:CheckBoxList>
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
        <asp:Button ID="btnMostrar" runat="server" Text="Cargar nuevo medico" CssClass="btn btn-sm btn-outline-primary mx-1" OnClick="btnMostrar_Click" />

        <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold" />
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

        <asp:Label ID="lblEliminarLogicamente" runat="server" CssClass="fw-semibold fs-5 text-dark d-block mb-3" Text="Desea Eliminar este Medico?" />


        <asp:Button ID="txtEliminarLgocimante" runat="server" CssClass="btn btn-danger rounded-pill px-4 py-2 fw-semibold shadow-sm" Text="Si,eliminar" OnClick="txtEliminarLgocimante_Click" />

        <asp:Button ID="txtNoeleiminarlogicamente" runat="server" CssClass="btn btn-outline-primary rounded-pill px-4 py-2 fw-semibold ms-2" Text="No deseo Eliminarlo" OnClick="txtNoeleiminarlogicamente_Click" />



        <asp:Label ID="lblEliminado" runat="server" CssClass="alert alert-success fw-semibold rounded-pill px-4 py-2 shadow-sm d-inline-block mt-2" Text="Eliminado correctamente" />

        <asp:Label ID="lblEminadoEror" runat="server" CssClass="alert alert-danger d-inline-block mt-2" Text="Error al eliminar" />

        <asp:Label ID="lblModificao" runat="server" CssClass="alert alert_success d-inline-block mt-2" />

        <asp:Label ID="lblEroorGuardar" runat="server" CssClass="alert alert_success d-inline-block mt-2" />

        <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-outline-primary rounded-pill px-4 py-2 fw-semibold" Text="Volver" OnClick="btnVolver_Click" />

    </asp:Panel>



    <!-- ===================== LISTADO DE MÉDICOS ===================== -->
    <div class="card shadow-sm">
        <div class="card-header bg-light">
            <strong class="text-success fw-semibold fs-5">Resultados de la búsqueda</strong>
        </div>

        <div class="card-body p-0">

            <asp:GridView ID="gvMedicos" runat="server" EmptyDataText="No hay datos" CssClass="table table-hover mb-0 align-middle"
                HeaderStyle-CssClass="table-light" AutoGenerateColumns="false" DataKeyNames="IdMedico"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvMedicos_PageIndexChanging"
                OnRowEditing="gvMedicos_RowEditing" OnRowUpdating="gvMedicos_RowUpdating"
                OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDeleting="gvMedicos_RowDeleting"
                OnRowDataBound="gvMedicos_RowDataBound" OnRowCommand="gvMedicos_RowCommand1">

                <Columns>




                    <asp:TemplateField HeaderText="Idmedico" Visible="false">
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
                            <asp:DropDownList ID="ddlTurnoEdit" runat="server" CssClass="form-select" DataTextField="Nombre" DataValueField="IdTurnoTrabajo"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlTurnoEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Día">

                        <ItemTemplate>
                            <%# Eval("DiasResumen") %>
                        </ItemTemplate>


                        <EditItemTemplate>
                            <asp:CheckBoxList ID="cblDiasSemanaEdit" runat="server" CssClass="form-check" RepeatDirection="Vertical">


                                <asp:ListItem Text="Lunes" Value="Lunes" />
                                <asp:ListItem Text="Martes" Value="Martes" />
                                <asp:ListItem Text="Miércoles" Value="Miércoles" />
                                <asp:ListItem Text="Jueves" Value="Jueves" />
                                <asp:ListItem Text="Viernes" Value="Viernes" />
                                <asp:ListItem Text="Sábado" Value="Sábado" />
                                <asp:ListItem Text="Domingo" Value="Domingo" />
                            </asp:CheckBoxList>
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
                                Style="width: 80px" ReadOnly="true">
                                   

                            </asp:TextBox>
                            &nbsp;a&nbsp;

                             <asp:TextBox ID="txtHoraFinEdit" runat="server"
                                 CssClass="form-control d-inline-block"
                                 Style="width: 80px" ReadOnly="true">
                                

                             
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
                            <asp:CheckBoxList ID="cblEspEdit" runat="server" DataTextField="Nombre" DataValueField="IdEspecialidad"
                                RepeatDirection="Horizontal" RepeatColumns="5" CssClass="form-check form-check-inline" />

                        </EditItemTemplate>
                    </asp:TemplateField>






                    <asp:TemplateField HeaderText="Acciones" >
                        <ItemTemplate>

                            <asp:Button ID="btnEditar" runat="server" Text="✏️ Modificar"
                                CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Edit" />

                            <asp:Button ID="btnModificar" runat="server" Text="💾 Guardar Cambios"
                                CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Update" />

                            <asp:Button ID="btnCancelar" runat="server" Text="↩️ Cancelar Edicion"
                                CssClass="btn btn-sm btn-outline-primary mx-1" CommandName="Cancel" />


                            <asp:Button ID="btnCrearUsuario" runat="server" Text="➕👤 Crear Usuario" CssClass="btn btn-sm btn-outline-success mx-1"
                                CommandName="CrearUsuario"
                                CommandArgument='<%# Eval("IdMedico") %>'
                                Visible='<%# !(bool)Eval("TieneUsuario") %>' />


                            <asp:Button ID="btnActivarUsuarioDesdeMedico" runat="server" Text="🔓 Activar Usuario"
                                CssClass="btn btn-sm btn-outline-warning mx-1"
                                CommandName="ActivarUsuarioDesdeMedico"
                                CommandArgument='<%# Eval("IdMedico") %>'
                                Visible='<%# (bool)Eval("TieneUsuario") && !(bool)Eval("UsuarioActivo") %>' />




                            <asp:Button ID="btnEliminar" runat="server" Text="🗑️ Eliminar" OnClick="btnEliminar_Click"
                                CssClass="btn btn-sm btn-outline-danger mx-1"
                                CommandName="Eliminar" CommandArgument='<%# Eval("IdMedico") %>' />
                        </ItemTemplate>




                    </asp:TemplateField>

















                </Columns>



            </asp:GridView>




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
