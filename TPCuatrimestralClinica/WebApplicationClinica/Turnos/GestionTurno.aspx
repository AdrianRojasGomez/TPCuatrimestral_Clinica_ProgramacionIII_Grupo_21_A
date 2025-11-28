<%@ Page Title="Gestión de Turnos" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="GestionTurno.aspx.cs" Inherits="WebApplicationClinica.GestionTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table th > a {
            color: #212529 !important;
            text-decoration: none;
        }

            .table th > a:hover {
                color: #000 !important;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-4">
        <h2 class="mb-3"><%: Title %></h2>
        <p class="lead">Administración de los turnos programados en la clínica.</p>

        <div class="card mb-4">
            <div class="card-body">
                <div class="row g-3 align-items-center">
                    <div class="col-md-6 col-lg-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtBuscarTurno" runat="server" CssClass="form-control"
                                Placeholder="Buscar por DNI, Paciente o Médico..."></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                CssClass="btn btn-outline-secondary" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4 text-end">
                        <asp:Button ID="btnNuevoTurno" runat="server" Text="Agregar Nuevo Turno"
                            CssClass="btn btn-primary w-100" OnClick="btnNuevoTurno_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

        <div class="alert" role="alert" runat="server" id="divMensaje" visible="false">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

        <div class="card card-body">
            <h3 class="mb-3">Lista de Turnos</h3>
            <div class="table-responsive">

                <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" DataKeyNames="IdTurno"
                    CssClass="table table-hover table-striped table-bordered"
                    EmptyDataText="No hay turnos registrados."
                    OnRowCommand="gvTurnos_RowCommand"
                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTurnos_PageIndexChanging"
                    AllowSorting="true" OnSorting="gvTurnos_Sorting" OnRowCreated="gvTurnos_RowCreated">

                    <Columns>
                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha"
                            SortExpression="FechaInicio" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Hora" SortExpression="HoraInicio">
                            <ItemTemplate>
                                <%-- 
                                    Esto convierte el dato (TimeSpan) a string con el formato correcto.
                                    "hh" = horas (00-23)
                                    "\:" = el caracter literal ":"
                                    "mm" = minutos (00-59)
                                --%>
                                <asp:Label runat="server" Text='<%# Eval("HoraInicio", "{0:hh\\:mm}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PacienteDNI" HeaderText="DNI" SortExpression="PacienteDNI" />

                        <asp:BoundField DataField="PacienteNombre" HeaderText="Paciente"
                            SortExpression="PacienteNombre" />

                        <asp:BoundField DataField="MedicoNombre" HeaderText="Médico"
                            SortExpression="MedicoNombre" />

                        <asp:BoundField DataField="EspecialidadNombre" HeaderText="Especialidad"
                            SortExpression="EspecialidadNombre" />

                        <asp:BoundField DataField="Motivo" HeaderText="Motivo"
                            SortExpression="Motivo" />

                        <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
                            <ItemTemplate>
                                <asp:Label runat="server"
                                    Text='<%# (int)Eval("Estado") == 1 ? "Pendiente" :
                       (int)Eval("Estado") == 2 ? "Atendiendo" :
                       (int)Eval("Estado") == 3 ? "Completado" :
                       (int)Eval("Estado") == 4 ? "No asistió" :
                       "Cancelado" %>'
                                    CssClass='<%# (int)Eval("Estado") == 1 ? "badge bg-warning text-dark" :     
                          (int)Eval("Estado") == 2 ? "badge bg-primary" :               
                          (int)Eval("Estado") == 3 ? "badge bg-success" :              
                          (int)Eval("Estado") == 4 ? "badge bg-secondary" :            
                          "badge bg-danger" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="220px">
                            <ItemTemplate>

                                <%-- Botón Editar: Aparece si está Pendiente (0) o Completado (1) --%>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    CommandName="EditarFecha"
                                    CommandArgument='<%# Eval("IdTurno") %>'
                                    CssClass="btn btn-sm btn-info"
                                    ToolTip="Modificar Fecha/Hora"
                                    Visible='<%# (int)Eval("Estado") == 1 || (int)Eval("Estado") == 3 %>'>
                                    <i class="bi bi-pencil-fill"></i> Editar
                                </asp:LinkButton>

                                <%-- Botón Cancelar: Aparece SOLO si está Pendiente (1) --%>
                                <asp:LinkButton ID="btnCancelar" runat="server"
                                    CommandName="CancelarTurno"
                                    CommandArgument='<%# Eval("IdTurno") %>'
                                    CssClass="btn btn-sm btn-danger"
                                    ToolTip="Cancelar Turno"
                                    OnClientClick="return confirm('¿Está seguro de que desea cancelar este turno?');"
                                    Visible='<%# (int)Eval("Estado") == 1 %>'>
                                    <i class="bi bi-x-circle"></i> Cancelar
                                </asp:LinkButton>

                                <%-- Botón Reactivar: Aparece SOLO si está Cancelado (0) --%>
                                <asp:LinkButton ID="btnReactivar" runat="server"
                                    CommandName="ReactivarTurno"
                                    CommandArgument='<%# Eval("IdTurno") %>'
                                    CssClass="btn btn-sm btn-success"
                                    ToolTip="Reactivar Turno"
                                    Visible='<%# (int)Eval("Estado") == 0 %>'>
                                    <i class="bi bi-arrow-clockwise"></i> Reactivar
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#f8f9fa" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerStyle CssClass="pagination-sm" HorizontalAlign="Right" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
