using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            panelGrillaMedico.Visible = false;

            try
            {
                if (!IsPostBack)
                {


                    TurnoTrabajoNegocio turnoTrabajo = new TurnoTrabajoNegocio();
                    EspeciladadNegocio especiladadNegocio = new EspeciladadNegocio();
                    List<TurnoTrabajo> listaturnotrabajo = turnoTrabajo.Listar();
                    List<Especialidad> listaespecialidad = especiladadNegocio.Listar();

                    ddllistTurnoTrabajo.DataSource = listaturnotrabajo;
                    ddllistTurnoTrabajo.DataValueField = "IdTurnoTrabajo";
                    ddllistTurnoTrabajo.DataTextField = "Nombre";
                    ddllistTurnoTrabajo.DataBind();
                    ddllistTurnoTrabajo.Items.Insert(0, new ListItem("-- Seleccionar turno --", "0"));

                    DdlistEspecilidad.DataSource = listaespecialidad;
                    DdlistEspecilidad.DataValueField = "IdEspecialidad";
                    DdlistEspecilidad.DataTextField = "Nombre";
                    DdlistEspecilidad.DataBind();

                    DdlistEspecilidad.Items.Insert(0, new ListItem("-- Seleccione turno --", "0"));




                }

            }
            catch (Exception ex)
            {

                throw ex;
            }



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
            panelGrillaMedico.Visible = true;

            lblError.Visible = false;
            lblError.CssClass = "text-danger";

            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {

                Medico medico = new Medico();
                MedicoNegocio medicoNegocio = new MedicoNegocio();


                string nombre = (txtNombreMedico.Text ?? "").Trim();
                string apellido = (txtApellidoMedico.Text ?? "").Trim();
                string matricula = (txtMatriculaMedico.Text ?? "").Trim();
                string idTurno = ddllistTurnoTrabajo.SelectedValue ?? "0";
                string idEsp = DdlistEspecilidad.SelectedValue ?? "0";



                if (string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrWhiteSpace(matricula))
                {
                    lblError.Text = "❌ Completá todos los campos obligatorios: Nombre, Apellido y Matrícula.";
                    lblError.Visible = true;
                    return;
                }


                if (nombre.Length > 50 || apellido.Length > 50)
                {
                    lblError.Text = "❌ El Nombre y el Apellido no pueden superar los 50 caracteres.";
                    lblError.Visible = true;
                    return;
                }


                if (matricula.Length < 3)
                {
                    lblError.Text = "❌ La matrícula debe tener al menos 3 caracteres.";
                    lblError.Visible = true;
                    return;
                }


                if (idTurno == "0")
                {
                    lblError.Text = "❌ Seleccioná un turno de trabajo.";
                    lblError.Visible = true;
                    return;
                }


                if (idEsp == "0")
                {
                    lblError.Text = "❌ Seleccioná una especialidad.";
                    lblError.Visible = true;
                    return;
                }


                medico.Nombre = nombre;
                medico.Apellido = apellido;
                medico.Matricula = matricula;

                medico.TurnoTrabajo = new TurnoTrabajo();
                medico.TurnoTrabajo.IdTurnoTrabajo = int.Parse(idTurno);


                medico.Especialidades = new List<Especialidad>();
                Especialidad esp = new Especialidad();
                esp.IdEspecialidad = int.Parse(idEsp);
                medico.Especialidades.Add(esp);


                medicoNegocio.AgregarMedico(medico);


                lblError.CssClass = "text-success";
                lblError.Text = "✅ Médico agregado correctamente.";
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.CssClass = "text-danger";
                lblError.Text = "❌ Ocurrió un error inesperado: " + ex.Message;
                lblError.Visible = true;
            }

            finally
            {
                accesoDatos.CerrarConexion();

            }










        }

        protected void btnBotonLimpiarMedico_Click(object sender, EventArgs e)
        {
            txtNombreMedico.Text = "";
            txtApellidoMedico.Text = "";
            txtMatriculaMedico.Text = "";
            ddllistTurnoTrabajo.SelectedIndex = 0;
            DdlistEspecilidad.SelectedIndex = 0;

            lblError.Visible = false;
            panelGrillaMedico.Visible = true;


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
            panelGrillaMedico.Visible = false;

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