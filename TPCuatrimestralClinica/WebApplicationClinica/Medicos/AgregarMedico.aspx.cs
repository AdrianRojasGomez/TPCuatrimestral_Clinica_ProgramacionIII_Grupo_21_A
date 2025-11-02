using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.Diagnostics.Eventing.Reader;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        public bool confirmar = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            panelGrillaMedico.Visible = false;

            MedicoNegocio medicoNegocio = new MedicoNegocio();

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



                    CargarGrillaMedicos();

                    txtFiltrarMedico.Attributes["oninput"] = $"liveFilter('{txtFiltrarMedico.UniqueID}')";
                    panelEliminar.Visible = false;

                    panelEliminar.Visible = false;
                    lblEliminado.Visible = false;
                    lblEminadoEror.Visible = false;



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
                medico.TurnoTrabajo.HoraInicio = TimeSpan.Parse(txtHoraFin.Text);
                medico.TurnoTrabajo.HoraFin = TimeSpan.Parse(txtHoraFin.Text);


                medico.Especialidades = new List<Especialidad>();
                Especialidad esp = new Especialidad();
                esp.IdEspecialidad = int.Parse(idEsp);
                medico.Especialidades.Add(esp);


                medicoNegocio.AgregarMedico(medico);


                lblError.CssClass = "text-success";
                lblError.Text = "✅ Médico agregado correctamente.";
                lblError.Visible = true;

                CargarGrillaMedicos();
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

            List<Medico> lista = (List<Medico>)Session["listamedicos"];
            string filtro = txtFiltrarMedico.Text.ToUpper();

            List<Medico> listafiltrada;

            if (string.IsNullOrWhiteSpace(filtro))
            {

                listafiltrada = lista;
            }
            else
            {

                listafiltrada = lista.FindAll(x =>
                    x.Nombre.ToUpper().Contains(filtro) ||
                    x.Apellido.ToUpper().Contains(filtro) ||
                    (x.TurnoTrabajo != null && x.TurnoTrabajo.Nombre.ToUpper().Contains(filtro)));
            }

            gvMedicos.DataSource = listafiltrada;
            gvMedicos.DataBind();

            txtFiltrarMedico.Focus();



        }

        public void CargarGrillaMedicos()
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            List<Medico> lista = medicoNegocio.ListarMedicos();
            Session.Add("listamedicos", lista);
            gvMedicos.DataSource = lista;
            gvMedicos.DataBind();
        }

        protected void txtEliminarFisicamemte_Click(object sender, EventArgs e)
        {
      


        }

        protected void gvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            ViewState["IdMedicoEliminar"] = id;


            panelEliminar.Visible = true;

            lblEminadoEror.Visible = false;

           lblEliminado.Visible=false;
        }

        protected void txtNoeleiminarlogicamente_Click(object sender, EventArgs e)
        {

            panelEliminar.Visible=false;
           
        }

        protected void txtEliminarLgocimante_Click(object sender, EventArgs e)
        {

            
                try
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();


                    int id = (int)ViewState["IdMedicoEliminar"];


                    medicoNegocio.EliminarMedico(id);

                    lblEliminado.Visible = true;
                    lblEminadoEror.Visible = false;


                    


                    CargarGrillaMedicos();


                }
                catch (Exception ex)
                {

                    lblEminadoEror.Visible = true;

                    throw ex;

                    
                }


            
           
        }
    }
}