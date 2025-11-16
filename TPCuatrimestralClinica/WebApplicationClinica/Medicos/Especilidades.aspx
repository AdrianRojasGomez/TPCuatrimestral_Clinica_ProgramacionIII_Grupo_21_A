<%@ Page Title="" Language="C#" MasterPageFile="~/Clinica.Master" AutoEventWireup="true" CodeBehind="Especilidades.aspx.cs" Inherits="WebApplicationClinica.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   

    <asp:Panel ID="pnlEspecialidad" runat="server" Visible="true">





    



        <div class="card shadow-sm border-0 mb-4">

            <!-- TÍTULO -->

            <!-- CUERPO DEL PANEL -->
            <div class="card-body">

                <!-- Campo: Nombre -->
                <div class="mb-3">
                    <label for="TxtNombreEspecialidad" class="mb-1 text-success fw-bold">Nombre de la especialidad</label>
                    <asp:TextBox ID="TxtNombreEspecialidad" runat="server"
                        CssClass="form-control" placeholder="Ej: Cardiología" OnTextChanged="TxtNombreEspecialidad_TextChanged"></asp:TextBox>
                </div>

                <!-- Mensaje -->
                <asp:Label ID="lblMensajePanel" runat="server"
                    CssClass="fw-semibold"></asp:Label>

                <!-- BOTONES -->
                <div class="mt-3 d-flex">
                    <asp:Label ID="lblMensajeEspecialidad" runat="server"      CssClass="btn btn-outline-info btn-sm me-3"  Visible="false" />
                    <asp:Button ID="btnGuardarEspecialidad" runat="server"
                        Text="Guardar"
                        CssClass="btn btn-success me-3" OnClick="btnGuardarEspecialidad_Click" />

                    <asp:Button ID="btnCancelar" runat="server"
                        Text="Cancelar"
                      CssClass="btn btn-danger me-3" />

                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-sm btn-outline-secondary mx-1 shadow-sm" Text="🧹 Limpiar" OnClick="btnLimpiar_Click" />

                </div>

            </div>
        </div>
    </asp:Panel>

       <div class="card shadow-sm">
       <div class="card-header bg-light">
           <strong class="text-success fw-semibold fs-5">Resultados de la búsqueda</strong>
       </div>

       <div class="card-body p-0">

    <asp:GridView ID="gvEspecialidades" runat="server" AutoGenerateColumns="false" 
 CssClass="table table-striped" OnRowEditing="gvEspecilidades_RowEditing" OnRowCancelingEdit="gvEspecilidades_RowCancelingEdit"
   OnRowUpdating="gvEspecilidades_RowUpdating" OnRowDeleting="gvEspecilidades_RowDeleting">

      <columns>

               
        <asp:BoundField DataField="IdEspecialidad" HeaderText="ID" ReadOnly="True" />

       
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"
                          EditText="✏️ Editar"
                          UpdateText="💾 Guardar"
                          CancelText="↩️ Cancelar"
                          DeleteText="🗑️ Eliminar" />

      </columns> 

    </asp:GridView>
            </div>
            </div>

      

</asp:Content>
