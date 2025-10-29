using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{

    public partial class Pacientes : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                // CargarPacientes(); 
            }
        }

       
        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            LimpiarFormulario(); 
            lblFormTitulo.Text = "Nuevo Paciente"; 
            pnlFormulario.Visible = true;          
            gvPacientes.Visible = false;           
            divMensaje.Visible = false;          
        }

        
        private void LimpiarFormulario()
        {
            hfPacienteId.Value = "0"; 
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
          
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            // pnlFormulario.Visible = false;
            // gvPacientes.Visible = true;
            // CargarPacientes();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            LimpiarFormulario();
            pnlFormulario.Visible = false;
            gvPacientes.Visible = true;
            // CargarPacientes();
        }

        protected void gvPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
            // pnlFormulario.Visible = true;
            // gvPacientes.Visible = false;
        }

        protected void gvPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            // CargarPacientes();
        }

        
        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            divMensaje.Visible = true;
            divMensaje.Attributes["class"] = "alert alert-" + tipo;
        }

        
        // private void CargarPacientes()
        // {
        //     // Implementación con PacienteDAO
        // }
    }


}