
<%@ Page Title="Gestión de Turnos" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="GestionTurnos.aspx.cs" Inherits="WebApplicationClinica.GestionTurnos" %>

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
                        <asp:BoundField DataField="HoraInicio" HeaderText="Hora" 
                            SortExpression="HoraInicio" DataFormatString="{0:hh\\:mm}" />
                        <asp:BoundField DataField="PacienteNombre" HeaderText="Paciente" 
                            SortExpression="PacienteNombre" />
                        <asp:BoundField DataField="MedicoNombre" HeaderText="Médico" 
                            SortExpression="MedicoNombre" />
                        <asp:BoundField DataField="Motivo" HeaderText="Motivo" 
                            SortExpression="Motivo" />

                        <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
                            <ItemTemplate>
                                <asp:Label runat="server"
                                    Text='<%# (bool)Eval("Estado") == false ? "Pendiente" : "Completado" %>'
                                    CssClass='<%# (bool)Eval("Estado") == false ? "badge bg-warning text-dark" : "badge bg-success" %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAtender" runat="server" 
                                    CommandName="Atender" CommandArgument='<%# Eval("IdTurno") %>' 
                                    CssClass="btn btn-sm btn-info me-2" ToolTip="Atender Turno">
                                    <i class="bi bi-pencil-square"></i> Atender
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancelarTurno" runat="server" 
                                    CommandName="CancelarTurno" CommandArgument='<%# Eval("IdTurno") %>' 
                                    CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('¿Está seguro que desea CANCELAR este turno?');" ToolTip="Cancelar Turno">
                                    <i class="bi bi-trash"></i> Cancelar
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