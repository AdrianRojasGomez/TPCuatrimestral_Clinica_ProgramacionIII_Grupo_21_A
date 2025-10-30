<%@ Page Title="Gestion de Pacientes" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="WebApplicationClinica.Pacientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4"> 
        <h2><%: Title %></h2>
        <p>Administración de la información de los pacientes.</p>

        <div class="mb-3">
            <asp:Button ID="btnNuevoPaciente" runat="server" Text="Agregar Nuevo Paciente" CssClass="btn btn-primary" OnClick="btnNuevoPaciente_Click" />
        </div>

        <div class="alert" role="alert" runat="server" id="divMensaje" visible="false">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </div>

        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="card card-body mb-4">
            <h3><asp:Label ID="lblFormTitulo" runat="server" Text="Nuevo Paciente"></asp:Label></h3>
            <hr />
            <asp:HiddenField ID="hfPacienteId" runat="server" Value="0" /> 
            
            <div class="mb-3">
                <asp:Label ID="Label1" runat="server" Text="Nombre:"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label2" runat="server" Text="Apellido:"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label3" runat="server" Text="DNI:"></asp:Label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label4" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label6" runat="server" Text="Teléfono:"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label7" runat="server" Text="Dirección:"></asp:Label>
                <asp:TextBox ID="txtDireccion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mt-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success me-2" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
            </div>
        </asp:Panel>

        <div class="table-responsive mt-4">
            <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" DataKeyNames="IdPaciente"
                CssClass="table table-striped table-bordered" EmptyDataText="No hay pacientes registrados."
                OnRowCommand="gvPacientes_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdPaciente" HeaderText="ID" ReadOnly="True" SortExpression="IdPaciente" />
                    <asp:BoundField DataField="Dni" HeaderText="DNI" SortExpression="Dni" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CommandArgument='<%# Eval("IdPaciente") %>' CssClass="btn btn-sm btn-info me-2">Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument='<%# Eval("IdPaciente") %>' CssClass="btn btn-sm btn-danger" 
                                OnClientClick="return confirm('¿Está seguro que desea eliminar este paciente?');">Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#343a40" ForeColor="White" />
                <RowStyle BackColor="#f8f9fa" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>