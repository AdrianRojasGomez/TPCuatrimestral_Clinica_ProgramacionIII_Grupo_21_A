using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            panelGrillaMedico.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void txtNombreMedico_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtApellidoMedico_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMatriculaMedico_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnGuardarMedico_Click(object sender, EventArgs e)
        {

            
            string nombre = (txtNombreMedico.Text ?? "").Trim();
            string apellido = (txtApellidoMedico.Text ?? "").Trim();
            string matricula = (txtMatriculaMedico.Text ?? "").Trim();
            string idTurno = ddllistTurnoTrabajo.SelectedValue ?? "0";
            string idEsp = DdlistEspecilidad.SelectedValue ?? "0";

          
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(matricula))
            {
                lblError.Text = "Completá Nombre, Apellido y Matrícula.";
                lblError.Visible = true;
                panelGrillaMedico.Visible = true;   
                return;
            }

            
            if (nombre.Length > 50 || apellido.Length > 50)
            {
                lblError.Text = "Nombre y Apellido no pueden superar 50 caracteres.";
                lblError.Visible = true;
                panelGrillaMedico.Visible = true;
                return;
            }

            
            if (!long.TryParse(matricula, out _))
            {
                lblError.Text = "La matrícula debe ser numérica.";
                lblError.Visible = true;
                panelGrillaMedico.Visible = true;
                return;
            }

            
            if (idTurno == "0")
            {
                lblError.Text = "Seleccioná un turno de trabajo.";
                lblError.Visible = true;
                panelGrillaMedico.Visible = true;
                return;
            }

            if (idEsp == "0")
            {
                lblError.Text = "Seleccioná una especialidad.";
                lblError.Visible = true;
                panelGrillaMedico.Visible = true;
                return;
            }


        }

        protected void btnBotonLimpiarMedico_Click(object sender, EventArgs e)
        {
            txtNombreMedico.Text = "";
            txtApellidoMedico.Text = "";
            txtMatriculaMedico.Text = "";
            ddllistTurnoTrabajo.SelectedIndex = 0;
            DdlistEspecilidad.SelectedIndex = 0;


        }

        protected void dvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGuardarMedico_Click1(object sender, EventArgs e)
        {

        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            panelGrillaMedico.Visible = true;

            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
             panelGrillaMedico.Visible=false;
           
        }

        protected void gvMedicos_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gvMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvMedicos_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void txtFiltrarMedico_TextChanged(object sender, EventArgs e)
        {

        }
    }
}