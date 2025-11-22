using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class CrearTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Setup Inicial
                pnlDatosPaciente.Visible = false;
                pnlAgregarPaciente.Visible = false;
                lblMensajeError.Visible = false;
                btnIrAgregarPaciente.Visible = false;
                btnGuardar.Enabled = false;
                dtFechaTurno.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");

                //Deshabilitar Medico, Fecha y hora hasta seleccionar una Especialidad
                ddlMedicoDisponible.Enabled = false;
                dtFechaTurno.Enabled = false;
                ddlHorario.Enabled = false;

                //Cargar lista de Especialidades
                List<Especialidad> especialidades = new List<Especialidad>();
                EspeciladadNegocio espNegocio = new EspeciladadNegocio();
                especialidades = espNegocio.Listar();
                ddlEspecialidad.DataSource = especialidades;
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataBind();

            }
        }

        #region Pacientes Dentro De Turno
        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            pnlDatosPaciente.Visible = false;
            pnlAgregarPaciente.Visible = false;
            lblMensajeError.Visible = false;
            btnIrAgregarPaciente.Visible = false;
            lblPacienteEstado.CssClass = "badge bg-secondary";
            lblPacienteEstado.Text = "Buscando...";

            string dni = txtDocumento.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                lblMensajeError.Text = "Debe ingresar un DNI.";
                lblMensajeError.Visible = true;
                lblPacienteEstado.Text = "Error";
                lblPacienteEstado.CssClass = "badge bg-danger";
                return;
            }

            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente pacienteEncontrado = negocio.BuscarPorDni(dni);

                if (pacienteEncontrado != null)
                {
                    pnlDatosPaciente.Visible = true;
                    lblPacienteEstado.CssClass = "badge bg-success";
                    lblPacienteEstado.Text = "Paciente Encontrado";
                    txtNombrePaciente.Text = pacienteEncontrado.Nombre;
                    txtApellidoPaciente.Text = pacienteEncontrado.Apellido;
                    txtEmailPaciente.Text = pacienteEncontrado.Email;
                    txtTelefonoPaciente.Text = pacienteEncontrado.Telefono;
                    ViewState["IdPaciente"] = pacienteEncontrado.IdPaciente;
                    Session["IdPaciente"] = pacienteEncontrado.IdPaciente;
                }
                else
                {
                    lblPacienteEstado.CssClass = "badge bg-danger";
                    lblPacienteEstado.Text = "Paciente no registrado";
                    lblMensajeError.Text = "El DNI ingresado no pertenece a un paciente. Puede agregarlo.";
                    lblMensajeError.Visible = true;
                    btnIrAgregarPaciente.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblPacienteEstado.CssClass = "badge bg-danger";
                lblPacienteEstado.Text = "Error";
                lblMensajeError.Text = "Error al buscar en la base de datos: " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        protected void btnIrAgregarPaciente_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = true;
            pnlDatosPaciente.Visible = false;
            lblMensajeError.Visible = false;
            btnIrAgregarPaciente.Visible = false;
            txtNuevoDni.Text = txtDocumento.Text.Trim();
        }

        protected void btnGuardarNuevoPaciente_Click(object sender, EventArgs e)
        {

            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                Paciente pac = new Paciente();
                pac.Dni = txtNuevoDni.Text.Trim();
                pac.Nombre = txtNuevoNombre.Text.Trim();
                pac.Apellido = txtNuevoApellido.Text.Trim();
                pac.Email = txtNuevoEmail.Text.Trim();
                pac.Telefono = txtNuevoTelefono.Text.Trim();
                pac.Direccion = txtNuevoDireccion.Text.Trim();
                if (!string.IsNullOrEmpty(txtNuevoFechaNacimiento.Text))
                {
                    pac.FechaNacimiento = Convert.ToDateTime(txtNuevoFechaNacimiento.Text);
                }

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.GuardarNuevo(pac);

                Paciente pacienteGuardado = negocio.BuscarPorDni(pac.Dni);

                pnlAgregarPaciente.Visible = false;
                pnlDatosPaciente.Visible = true;
                lblPacienteEstado.CssClass = "badge bg-success";
                lblPacienteEstado.Text = "Guardado y Seleccionado";
                txtNombrePaciente.Text = pacienteGuardado.Nombre;
                txtApellidoPaciente.Text = pacienteGuardado.Apellido;
                txtEmailPaciente.Text = pacienteGuardado.Email;
                txtTelefonoPaciente.Text = pacienteGuardado.Telefono;
                ViewState["IdPaciente"] = pacienteGuardado.IdPaciente;
                Session["IdPaciente"] = pacienteGuardado.IdPaciente;
            }
            catch (Exception ex)
            {
                lblMensajeNuevoPaciente.Text = ex.Message;
                lblMensajeNuevoPaciente.Visible = true;
            }
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = false;
            lblMensajeError.Visible = true;
            btnIrAgregarPaciente.Visible = true;
        }

        #endregion

        #region Metodos del Turno
        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
            {
                return;
            }

            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = false;
            }
            RevertirMuted();

            Especialidad especialidadSeleccionada = new Especialidad();
            especialidadSeleccionada.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            especialidadSeleccionada.Nombre = ddlEspecialidad.SelectedItem.Text;

            dtFechaTurno.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");
            ddlMedicoDisponible.Items.Clear();
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            List<Medico> medicosConEspecialidad = new List<Medico>();
            medicosConEspecialidad = medicoNegocio.ListarPorEspecialidad(especialidadSeleccionada.IdEspecialidad);

            //Cargar el ddlMedicoDisponible
            ddlMedicoDisponible.DataSource = medicosConEspecialidad;
            ddlMedicoDisponible.DataTextField = "NombreCompleto";
            ddlMedicoDisponible.DataValueField = "IdMedico";
            ddlMedicoDisponible.DataBind();
            //Habilitar el ddlMedicoDisponible
            ddlMedicoDisponible.Enabled = true;
            //Habilitar el dtFechaTurno
            dtFechaTurno.Enabled = true;

            if (medicosConEspecialidad.Count == 1)
            {
                ddlMedicoDisponible.SelectedIndex = 0;
                ddlMedicoDisponible_SelectedIndexChanged(sender, e);
            }
            //Modificar el mensaje EspecialidadMuted
            EspecialidadMuted.InnerHtml = $"Se encontraron {medicosConEspecialidad.Count} médicos con la especialidad de {especialidadSeleccionada.Nombre}.";
            EspecialidadMuted.Attributes["class"] = "text-success d-block mt-2";


        }

        protected void ddlMedicoDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedicoDisponible.SelectedValue))
            {
                return;
            }

            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = false;
            }
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medicoSeleccionado = new Medico();
            int aux = int.Parse(ddlMedicoDisponible.SelectedValue);
            medicoSeleccionado = medicoNegocio.BuscarMedicoPorIdSimple(aux);
            System.Diagnostics.Debug.WriteLine(medicoSeleccionado.TurnoTrabajo.DiaSemana);
            //Validar las fechas no disponibles para ese medico
            //TODO: Implementar la lógica para ocultar fechas no disponibles en el dtFechaTurno

            //Modificar el mensaje MedicoMuted
            MedicoMuted.InnerHtml = $"Médico seleccionado: {ddlMedicoDisponible.SelectedItem.Text}.";
            MedicoMuted.Attributes["class"] = "text-success d-block mt-2";
        }

        protected void dtFechaTurno_Changed(object sender, EventArgs e)
        {
            if (btnGuardar.Enabled)
            {
                btnGuardar.Enabled = false;
            }

            MedicoNegocio medicoNegocio = new MedicoNegocio();
            DateTime fechaSeleccionada = DateTime.Parse(dtFechaTurno.Text);
            int idMedico = int.Parse(ddlMedicoDisponible.SelectedValue);
            List<TimeSpan> horariosDisponibles = medicoNegocio.ObtenerHorariosLibres(idMedico, fechaSeleccionada);
            ddlHorario.Items.Clear();
            foreach (TimeSpan horario in horariosDisponibles)
            {
                ddlHorario.Items.Add(new ListItem(horario.ToString(@"hh\:mm"), horario.ToString()));
            }
            ddlHorario.Enabled = true;

            //Modificar el mensaje FechaMuted
            var horarios = HorariosRecomendados();
            FechaMuted.InnerHtml = $"Horarios recomendados: " + string.Join(", ",horarios) + ".";
            FechaMuted.Attributes["class"] = "text-success d-block mt-2";
            HorarioMuted.InnerHtml = "4. Seleccione un horario disponible.";

        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnGuardar.Enabled = true;

            //Validar dias de semana
            


            //Modificar el mensaje HorarioMuted
            HorarioMuted.InnerHtml = $"Horario seleccionado: {ddlHorario.SelectedItem.Text}.";
            HorarioMuted.Attributes["class"] = "text-success d-block mt-2";
        }

        #endregion

        #region Modals de confirmacion

        private void MostrarModalConfirmacion(string titulo, string cuerpoHtml)
        {
            lblTituloModal.Text = titulo;
            litCuerpoModal.Text = cuerpoHtml;

            string script =
                "var myModal = new bootstrap.Modal(document.getElementById('modalConfirmacion')); " +
                "myModal.show();";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModalConfirmacion",
                script,
                true
            );
        }

        private void MostrarModalMensajeExito(string titulo, string cuerpoHtml)
        {
            lblTituloMensaje.Text = titulo;
            litCuerpoMensaje.Text = cuerpoHtml;

            string script =
                "var myModal = new bootstrap.Modal(document.getElementById('modalMensaje')); " +
                "myModal.show();";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModalMensaje",
                script,
                true
            );
        }

        protected void btnConfirmarModal_Click(object sender, EventArgs e)
        {
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            try
            {
                Turno nuevoTurno = new Turno();
                nuevoTurno.NumeroTurno = (turnoNegocio.ObtenerUltimoID() + 1).ToString();
                nuevoTurno.FechaInicio = DateTime.Parse(dtFechaTurno.Text);
                nuevoTurno.FechaFin = nuevoTurno.FechaInicio;
                nuevoTurno.HoraInicio = TimeSpan.Parse(ddlHorario.SelectedValue);
                nuevoTurno.HoraFin = nuevoTurno.HoraInicio + TimeSpan.FromHours(1);
                nuevoTurno.ObservacionesSolicitud = txtObservaciones.Text;
                nuevoTurno.ObservacionesDiagnostico = "";
                nuevoTurno.IdMedico = int.Parse(ddlMedicoDisponible.SelectedValue);
                nuevoTurno.IdPaciente = Session["IdPaciente"] != null ? (int)Session["IdPaciente"] : (int)ViewState["IdPaciente"];
                nuevoTurno.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
                nuevoTurno.Motivo = txtMotivo.Text;
                nuevoTurno.Estado = 2;
                nuevoTurno.Paciente = null;
                nuevoTurno.Medico = null;

                turnoNegocio.AgregarTurno(nuevoTurno);
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al guardar el turno: " + ex.Message;
                lblMensajeError.Visible = true;
            }

            string titulo = "Turno guardado";
            string cuerpo =
                "<strong>El turno se guardó correctamente.</strong><br/>" +
                "Podés consultar tus turnos en la sección correspondiente.";

            MostrarModalMensajeExito(titulo, cuerpo);
        }

        protected void btnExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Turnos/GestionTurno.aspx");
        }

        #endregion

        #region Botones footers guardar /cancelar   
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ViewState["IdPaciente"] == null)
            {
                lblMensajeError.Text = "Debe buscar y encontrar un paciente válido antes de guardar.";
                lblMensajeError.Visible = true;
                return;
            }

            string titulo = "Verifique los Datos antes de Confirmar";
            string cuerpo = $"<strong>DNI</strong>: {txtDocumento.Text.Trim()}<br />" +
                            $"<strong>Nombre de Paciente</strong>: {txtNombrePaciente.Text} {txtApellidoPaciente.Text}<br />" +
                            $"<strong>Especialidad</strong>: {ddlEspecialidad.SelectedItem.Text}<br />" +
                            $"<strong>Doctor asignado</strong>: {ddlMedicoDisponible.SelectedItem.Text}<br />" +
                            $"<strong>Fecha del Turno</strong>: {dtFechaTurno.Text}<br />" +
                            $"<strong>Horario del Turno</strong>: {ddlHorario.SelectedItem.Text}<br /><br />" +
                            $"Desea confirmar el turno?";

            MostrarModalConfirmacion(titulo, cuerpo);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mainmenu.aspx");
        }
        #endregion

        #region Helpers
        private void RevertirMuted()
        {
            EspecialidadMuted.InnerHtml = "1. Comience seleccionando una especialidad.";
            EspecialidadMuted.Attributes["class"] = "text-muted d-block mt-2";
            MedicoMuted.InnerHtml = "2. Seleccione una especialidad para ver los medicos disponibles.";
            MedicoMuted.Attributes["class"] = "text-muted d-block mt-2";
            FechaMuted.InnerHtml = "3. Seleccione un medico para ver las fechas disponibles.";
            FechaMuted.Attributes["class"] = "text-muted d-block mt-2";
            HorarioMuted.InnerHtml = "4. Seleccione una fecha para ver los horarios disponibles.";
            HorarioMuted.Attributes["class"] = "text-muted d-block mt-2";
        }

        private List<string> HorariosRecomendados()
        {
            List<string> horarios = new List<string>
            {
                "09:00",
                "10:00",
                "11:00",
            };
            return horarios;
        }

        #endregion

    }
}