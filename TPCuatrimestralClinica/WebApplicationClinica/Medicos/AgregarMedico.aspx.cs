using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm2 : System.Web.UI.Page
    {

      
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

                
                string horaInicioStr = (txtHoraInicio.Text ?? "").Trim();
                string horaFinStr = (txtHoraFin.Text ?? "").Trim();

             
                if (string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrWhiteSpace(matricula))
                {
                    lblError.Text = "❌ Completá Nombre, Apellido y Matrícula.";
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

                if (string.IsNullOrWhiteSpace(horaInicioStr) || string.IsNullOrWhiteSpace(horaFinStr))
                {
                    lblError.Text = "❌ Ingresá la Hora Inicio y la Hora Fin.";
                    lblError.Visible = true;
                    return;
                }

                TimeSpan horaInicio;
                TimeSpan horaFin;

                
                if (!TimeSpan.TryParse(horaInicioStr, out horaInicio) ||
                    !TimeSpan.TryParse(horaFinStr, out horaFin))
                {
                    lblError.Text = "❌ El horario debe tener un formato válido (ejemplo 08:00).";
                    lblError.Visible = true;
                    return;
                }

              
                medico.Nombre = nombre;
                medico.Apellido = apellido;
                medico.Matricula = matricula;

                medico.TurnoTrabajo = new TurnoTrabajo();
                medico.TurnoTrabajo.IdTurnoTrabajo = int.Parse(idTurno);
                medico.TurnoTrabajo.HoraInicio = horaInicio;
                medico.TurnoTrabajo.HoraFin = horaFin;

                medico.Especialidades = new List<Especialidad>();
                Especialidad esp = new Especialidad();
                esp.IdEspecialidad = int.Parse(idEsp);
                medico.Especialidades.Add(esp);

               
                
                int idNuevoMedico = medicoNegocio.AgregarMedico(medico);
                Session["idMedicoCreado"] = idNuevoMedico;
                



                lblError.CssClass = "text-success";
                lblError.Text = "✅ Médico agregado correctamente.";
                lblError.Visible = true;

                
                CargarGrillaMedicos();

                
                txtNombreMedico.Text = "";
                txtApellidoMedico.Text = "";
                txtMatriculaMedico.Text = "";
                txtHoraInicio.Text = "";
                txtHoraFin.Text = "";
                ddllistTurnoTrabajo.SelectedIndex = 0;
                DdlistEspecilidad.SelectedIndex = 0;
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
            gvMedicos.EditIndex = e.NewEditIndex;

            CargarGrillaMedicos();
        }

        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            
            panelEliminar.Visible = false;
            lblEliminarLogicamente.Visible = false;
            lblEliminado.Visible = false;
            lblEminadoEror.Visible = false;
            lblModificao.Visible = false;
            lblEroorGuardar.Visible = false;
            txtEliminarLgocimante.Visible = false;
            txtNoeleiminarlogicamente.Visible = false;
           

            try
            {
                int idMedico = Convert.ToInt32(gvMedicos.DataKeys[e.RowIndex].Value);
                GridViewRow fila = gvMedicos.Rows[e.RowIndex];

                TextBox txtNombreEdit = (TextBox)fila.FindControl("txtNombreEdit");
                TextBox txtApellidoEdit = (TextBox)fila.FindControl("txtApellidoEdit");
                TextBox txtMatriculaEdit = (TextBox)fila.FindControl("txtMatriculaEdit");
                DropDownList ddlTurnoEdit = (DropDownList)fila.FindControl("ddlTurnoEdit");
                TextBox txtHoraInicioEdit = (TextBox)fila.FindControl("txtHoraInicioEdit");
                TextBox txtHoraFinEdit = (TextBox)fila.FindControl("txtHoraFinEdit");
                CheckBoxList cblEspEdit = (CheckBoxList)fila.FindControl("cblEspEdit");

               
                if (string.IsNullOrWhiteSpace(txtNombreEdit.Text) ||
                    string.IsNullOrWhiteSpace(txtApellidoEdit.Text) ||
                    string.IsNullOrWhiteSpace(txtMatriculaEdit.Text) ||
                    string.IsNullOrEmpty(ddlTurnoEdit.SelectedValue) ||
                    string.IsNullOrWhiteSpace(txtHoraInicioEdit.Text) ||
                    string.IsNullOrWhiteSpace(txtHoraFinEdit.Text))
                {
                    panelEliminar.Visible = true;
                    lblEroorGuardar.Visible = true;
                    lblEroorGuardar.Text = "Todos los campos son obligatorios. Verifique los datos.";
                    e.Cancel = true;
                    return;
                }

                TimeSpan horaInicio;
                TimeSpan horaFin;

                if (!TimeSpan.TryParse(txtHoraInicioEdit.Text, out horaInicio) ||
                    !TimeSpan.TryParse(txtHoraFinEdit.Text, out horaFin))
                {
                    panelEliminar.Visible = true;
                    lblEroorGuardar.Visible = true;
                    lblEroorGuardar.Text = "El horario debe tener formato válido (ejemplo: 08:00).";
                    e.Cancel = true;
                    return;
                }

                
                List<int> idsEspecialidades = new List<int>();
                foreach (ListItem item in cblEspEdit.Items)
                {
                    if (item.Selected)
                        idsEspecialidades.Add(int.Parse(item.Value));
                }

                if (idsEspecialidades.Count == 0)
                {
                    panelEliminar.Visible = true;
                    lblEroorGuardar.Visible = true;
                    lblEroorGuardar.Text = "Debe seleccionar al menos una especialidad.";
                    e.Cancel = true;
                    return;
                }

              
                Medico medico = new Medico();
                medico.IdMedico = idMedico;
                medico.Nombre = txtNombreEdit.Text.Trim();
                medico.Apellido = txtApellidoEdit.Text.Trim();
                medico.Matricula = txtMatriculaEdit.Text.Trim();

                medico.TurnoTrabajo = new TurnoTrabajo();
                medico.TurnoTrabajo.IdTurnoTrabajo = int.Parse(ddlTurnoEdit.SelectedValue);
                medico.TurnoTrabajo.Nombre = ddlTurnoEdit.SelectedItem.Text;
                medico.TurnoTrabajo.HoraInicio = horaInicio;
                medico.TurnoTrabajo.HoraFin = horaFin;

              
                MedicoNegocio negocio = new MedicoNegocio();
                negocio.ModificarMedico(medico, idsEspecialidades);

                
                gvMedicos.EditIndex = -1;
                CargarGrillaMedicos();

                panelEliminar.Visible = true;
                lblModificao.Visible = true;
                lblModificao.Text = "✅ Los cambios del médico se guardaron correctamente.";
                btnVolver.Visible = true;
            }
            catch (Exception)
            {
                
                panelEliminar.Visible = true;
                lblEroorGuardar.Visible = true;
                lblEroorGuardar.Text = "❌ Error al guardar los cambios. Verifique los datos.";
                e.Cancel = true;
            }

        }

        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;              
            CargarGrillaMedicos();
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
           
            panelEliminar.Visible = true;

            lblEminadoEror.Visible = false;
            lblEliminado.Visible = false;
            lblModificao.Visible = false;
            lblEliminarLogicamente.Visible = false;

            txtEliminarLgocimante.Visible = false;
            txtNoeleiminarlogicamente.Visible = false;
            


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            ViewState["IdMedicoEliminar"] = id;


            panelEliminar.Visible = true;

            lblEminadoEror.Visible = false;

            lblEliminado.Visible = false;
           
            lblModificao.Visible = false;

            btnVolver.Visible = false;

        }

        protected void txtNoeleiminarlogicamente_Click(object sender, EventArgs e)
        {

            panelEliminar.Visible = false;

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

        protected void txtModificarSeguro_Click(object sender, EventArgs e)
        {


        }

        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                Button btnEditar = (Button)e.Row.FindControl("btnEditar");
                Button btnModificar = (Button)e.Row.FindControl("btnModificar"); // Guardar Cambios
                Button btnCancelar = (Button)e.Row.FindControl("btnCancelar");  // Cancelar Edición
                Button btnEliminar = (Button)e.Row.FindControl("btnEliminar");

              
                bool esFilaEdit = (e.Row.RowIndex == gvMedicos.EditIndex);

               
                if (esFilaEdit)
                {
                
                    if (btnEditar != null) btnEditar.Visible = false;
                    if (btnModificar != null) btnModificar.Visible = true;
                    if (btnCancelar != null) btnCancelar.Visible = true;
                    if (btnEliminar != null) btnEliminar.Visible = false;
                }
                else
                {
               
                    if (btnEditar != null) btnEditar.Visible = true;
                    if (btnModificar != null) btnModificar.Visible = false;
                    if (btnCancelar != null) btnCancelar.Visible = false;
                    if (btnEliminar != null) btnEliminar.Visible = true;
                }

             
                if (esFilaEdit)
                {
                    Medico medico = (Medico)e.Row.DataItem;

                  
                    DropDownList ddlTurnoEdit = (DropDownList)e.Row.FindControl("ddlTurnoEdit");
                    if (ddlTurnoEdit != null)
                    {
                        TurnoTrabajoNegocio turnoNegocio = new TurnoTrabajoNegocio();
                        var turnos = turnoNegocio.Listar();

                        ddlTurnoEdit.DataSource = turnos;
                        ddlTurnoEdit.DataTextField = "Nombre";
                        ddlTurnoEdit.DataValueField = "IdTurnoTrabajo";
                        ddlTurnoEdit.DataBind();

                        if (medico.TurnoTrabajo != null)
                        {
                            ddlTurnoEdit.SelectedValue = medico.TurnoTrabajo.IdTurnoTrabajo.ToString();
                        }
                    }

                
                    CheckBoxList cblEspEdit = (CheckBoxList)e.Row.FindControl("cblEspEdit");
                    if (cblEspEdit != null)
                    {
                        EspeciladadNegocio espNegocio = new EspeciladadNegocio();
                        var todas = espNegocio.Listar();

                        cblEspEdit.DataSource = todas;
                        cblEspEdit.DataTextField = "Nombre";
                        cblEspEdit.DataValueField = "IdEspecialidad";
                        cblEspEdit.DataBind();

                        if (medico.Especialidades != null)
                        {
                            foreach (Especialidad esp in medico.Especialidades)
                            {
                                ListItem item = cblEspEdit.Items.FindByValue(esp.IdEspecialidad.ToString());
                                if (item != null)
                                    item.Selected = true;
                            }
                        }
                    }

                    TextBox txtHoraInicioEdit = (TextBox)e.Row.FindControl("txtHoraInicioEdit");
                    TextBox txtHoraFinEdit = (TextBox)e.Row.FindControl("txtHoraFinEdit");

                    if (medico.TurnoTrabajo != null)
                    {
                        if (txtHoraInicioEdit != null)
                            txtHoraInicioEdit.Text = medico.TurnoTrabajo.HoraInicio.ToString(@"hh\:mm");

                        if (txtHoraFinEdit != null)
                            txtHoraFinEdit.Text = medico.TurnoTrabajo.HoraFin.ToString(@"hh\:mm");
                    }
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            panelEliminar.Visible = false;

            lblEliminarLogicamente.Visible = false;
            lblEliminado.Visible = false;
            lblEminadoEror.Visible = false;
            lblModificao.Visible = false;
            lblEroorGuardar.Visible = false;

           
            btnVolver.Visible = false;

           
           
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CrearUsuario.aspx");
        }
    }
}
