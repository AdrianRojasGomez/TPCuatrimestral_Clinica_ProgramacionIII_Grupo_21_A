<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Especilidades.aspx.cs" Inherits="WebApplicationClinica.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    <asp:Panel ID="pnlEspecialidad" runat="server" Visible="false">



            <div class="card-header bg-primary text-white">
                <h5 class="mb-0">
                    <asp:Label ID="lblTituloPanel" runat="server" Text="Agregar Especialidad"></asp:Label>
                </h5>
            </div>



    </asp:Panel>



        <div class="card shadow-sm border-0 mb-4">

            <!-- TÍTULO -->

            <!-- CUERPO DEL PANEL -->
            <div class="card-body">

                <!-- Campo: Nombre -->
                <div class="mb-3">
                    <label for="TxtNombreEspecialidad" class="form-label">Nombre de la especialidad</label>
                    <asp:TextBox ID="TxtNombreEspecialidad" runat="server"
                        CssClass="form-control" placeholder="Ej: Cardiología"></asp:TextBox>
                </div>

                <!-- Mensaje -->
                <asp:Label ID="lblMensajePanel" runat="server"
                    CssClass="fw-semibold"></asp:Label>

                <!-- BOTONES -->
                <div class="mt-3 d-flex">
                    <asp:Button ID="btnGuardarEspecialidad" runat="server"
                        Text="Guardar"
                        CssClass="btn btn-success me-3" />

                    <asp:Button ID="btnCancelar" runat="server"
                        Text="Cancelar"
                        CssClass="btn btn-outline-secondary" />
                </div>

            </div>
        </div>
    </asp:Panel>
</asp:Content>
