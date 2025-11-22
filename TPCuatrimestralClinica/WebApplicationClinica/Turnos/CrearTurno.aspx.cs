using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
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


                //Deshabilitar Medico, Fecha y hora hasta seleccionar una Especialidad
                ddlMedicoDisponible.Enabled = false;
                DeshabilitarDatepicker();
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


            RevertirMuted();
            DeshabilitarDatepicker();
            ddlHorario.Items.Clear();
            ddlHorario.Enabled = false;
            btnGuardar.Enabled = false;
            Session["FechaTurno"] = null;


            Especialidad especialidadSeleccionada = new Especialidad();
            especialidadSeleccionada.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            especialidadSeleccionada.Nombre = ddlEspecialidad.SelectedItem.Text;

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


            if (medicosConEspecialidad.Count == 1)
            {
                ddlMedicoDisponible.SelectedIndex = 0;
                ddlMedicoDisponible_SelectedIndexChanged(sender, e);
            }
            //Modificar el mensaje EspecialidadMuted
            EspecialidadMuted.InnerHtml = $"Se encontraron {medicosConEspecialidad.Count} médicos con la especialidad de {especialidadSeleccionada.Nombre}.";
            EspecialidadMuted.Attributes["class"] = "text-success d-block mt-2 mt-auto";


        }

        protected void ddlMedicoDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedicoDisponible.SelectedValue))
            {
                return;
            }

            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medicoSeleccionado = new Medico();
            int aux = int.Parse(ddlMedicoDisponible.SelectedValue);
            medicoSeleccionado = medicoNegocio.BuscarMedicoPorIdSimple(aux);
            ///Validar las fechas no disponibles para ese medico
            List<DayOfWeek> diasDisponibles = medicoNegocio.ObtenerDiasQueTrabaja(medicoSeleccionado.IdMedico);

            ActualizarDiasSegunMedico(diasDisponibles);
            ///DEBUG : Mostrar los dias disponibles en consola
            string diasTexto = string.Join(", ", diasDisponibles.Select(d => d.ToString()));
            MedicoMuted.InnerHtml = $"Días disponibles: {diasTexto}.";
            ///System.Diagnostics.Debug.WriteLine(medicoSeleccionado.TurnoTrabajo.DiaSemana);

            ///Habilitar el datepicker
            ddlHorario.Items.Clear();
            ddlHorario.Enabled = false;
            btnGuardar.Enabled = false;
            HabilitarDatepicker();

            ///Modificar el mensaje MedicoMuted
            //MedicoMuted.InnerHtml = $"Médico seleccionado: {ddlMedicoDisponible.SelectedItem.Text}.";
            MedicoMuted.Attributes["class"] = "text-success d-block mt-2 mt-auto";
        }

        protected void lnkFechaSeleccionada_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hdnFechaTurno.Value))
                return;

            DateTime fecha;
            MedicoNegocio medicoNegocio = new MedicoNegocio();

            try
            {
                fecha = DateTime.Parse(hdnFechaTurno.Value);
            }
            catch
            {
                FechaMuted.InnerHtml = "Fecha inválida.";
                FechaMuted.Attributes["class"] = "text-danger d-block mt-2 mt-auto";
                hdnFechaTurno.Value = "";
                return;
            }

            if (fecha.Date < DateTime.Today)
            {
                FechaMuted.InnerHtml = "No se pueden seleccionar fechas anteriores a hoy.";
                FechaMuted.Attributes["class"] = "text-danger d-block mt-2 mt-auto";
                hdnFechaTurno.Value = "";
                return;
            }



            //Cargar los horarios recomendados
            int idMedico = int.Parse(ddlMedicoDisponible.SelectedValue);
            List<TimeSpan> horariosDisponibles = medicoNegocio.ObtenerHorariosLibres(idMedico, fecha);

            ddlHorario.Items.Clear();
            foreach (TimeSpan horario in horariosDisponibles)
            {
                ddlHorario.Items.Add(new ListItem(horario.ToString(@"hh\:mm"), horario.ToString()));
            }
            ddlHorario.Enabled = true;
            Session["FechaTurno"] = fecha;

            FechaMuted.InnerHtml = $"Fecha seleccionada: {fecha:yyyy-MM-dd}.";
            FechaMuted.Attributes["class"] = "text-success d-block mt-2 mt-auto";
            MostrarHorariosRecomendadosEnLabel();
        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnGuardar.Enabled = true;

            //Modificar el mensaje HorarioMuted
            HorarioMuted.InnerHtml = $"Horario seleccionado: {ddlHorario.SelectedItem.Text}.";
            HorarioMuted.Attributes["class"] = "text-success d-block mt-2 mt-auto";
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
            DateTime fechaTurno = (DateTime)Session["FechaTurno"];

            try
            {
                Turno nuevoTurno = new Turno();
                nuevoTurno.NumeroTurno = (turnoNegocio.ObtenerUltimoID() + 1).ToString();
                nuevoTurno.FechaInicio = fechaTurno;
                nuevoTurno.FechaFin = fechaTurno;
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
                            $"<strong>Fecha del Turno</strong>: {Session["FechaTurno"]}<br />" +
                            $"<strong>Horario del Turno</strong>: {ddlHorario.SelectedItem.Text}<br /><br />" +
                            $"Desea confirmar el turno?";

            MostrarModalConfirmacion(titulo, cuerpo);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mainmenu.aspx");
        }
        #endregion

        #region Datepicker Metodos
        private void DeshabilitarDatepicker()
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "DisableCalendarFlag",
                "deshabilitarCalendario();",
                true);
        }

        private void HabilitarDatepicker()
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "EnableCalendarFlag",
                "habilitarCalendario();",
                true);

            ConfigurarMinimoHoy();
        }

        private void DeshabilitarDiasSemana(List<DayOfWeek> dias)
        {

            var indices = dias.Select(d => (int)d);
            string jsArray = string.Join(",", indices);

            string script = $@"
        $('#calendarioTurnos').datepicker('setDaysOfWeekDisabled', [{jsArray}]);";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "DisableDaysOfWeek",
                script,
                true);
        }

        private void ConfigurarMinimoHoy()
        {
            string script = "$('#calendarioTurnos').datepicker('setStartDate', new Date());";

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "SetStartDateToday",
                script,
                true
            );
        }


        #endregion

        #region Helpers
        private void RevertirMuted()
        {
            EspecialidadMuted.InnerHtml = "1. Comience seleccionando una especialidad.";
            EspecialidadMuted.Attributes["class"] = "text-muted d-block mt-2 mt-auto";
            MedicoMuted.InnerHtml = "2. Seleccione una especialidad para ver los medicos disponibles.";
            MedicoMuted.Attributes["class"] = "text-muted d-block mt-2 mt-auto";
            FechaMuted.InnerHtml = "3. Seleccione un medico para ver las fechas disponibles.";
            FechaMuted.Attributes["class"] = "text-muted d-block mt-2 mt-auto";
            HorarioMuted.InnerHtml = "4. Seleccione una fecha para ver los horarios disponibles.";
            HorarioMuted.Attributes["class"] = "text-muted d-block mt-2 mt-auto";
        }

        private List<string> ObtenerHorariosDesdeDdl()
        {
            List<string> horarios = new List<string>();

            foreach (ListItem item in ddlHorario.Items)
            {
                horarios.Add(item.Text);
            }

            return horarios;
        }

        private void MostrarHorariosRecomendadosEnLabel()
        {
            List<string> horarios = ObtenerHorariosDesdeDdl();

            // Tomo solo los primeros 3 (o menos si no hay tantos)
            var primeros = horarios.Take(3).ToList();

            if (primeros.Count == 0)
            {
                HorarioMuted.InnerHtml = "No hay horarios recomendados disponibles.";
            }
            else
            {
                string lista = string.Join(", ", primeros);
                HorarioMuted.InnerHtml = $"Horarios recomendados: {lista}";
            }

            HorarioMuted.Attributes["class"] = "text-muted d-block mt-2 mt-auto";
        }

        private void ActualizarDiasSegunMedico(List<DayOfWeek> diasQueTrabaja)
        {

            var todosLosDias = Enum.GetValues(typeof(DayOfWeek))
                                   .Cast<DayOfWeek>();

            // Días que NO trabaja = todos - los que sí trabaja
            var diasQueNoTrabaja = todosLosDias
                .Except(diasQueTrabaja)
                .ToList();


            DeshabilitarDiasSemana(diasQueNoTrabaja);
        }
        #endregion



    }

}