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
                // Configuración inicial de visibilidad
                pnlDatosPaciente.Visible = false;
                pnlAgregarPaciente.Visible = false;
                lblMensajeError.Visible = false;
                btnIrAgregarPaciente.Visible = false;
                btnGuardar.Enabled = false;

                // Deshabilitar controles dependientes
                ddlMedicoDisponible.Enabled = false;
                ddlFechaTurno.Enabled = false;
                ddlHorario.Enabled = false;

                CargarEspecialidades();
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                EspecialidadNegocio espNegocio = new EspecialidadNegocio();
                ddlEspecialidad.DataSource = espNegocio.Listar();
                ddlEspecialidad.DataTextField = "Nombre";
                ddlEspecialidad.DataValueField = "IdEspecialidad";
                ddlEspecialidad.DataBind();

                ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", ""));
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al cargar especialidades: " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        #region Flujo de Selección de Turno

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
                return;

            ResetearSelectores(1);

            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            MedicoNegocio medicoNegocio = new MedicoNegocio();
            List<Medico> medicos = medicoNegocio.ListarPorEspecialidad(idEspecialidad);

            ddlMedicoDisponible.DataSource = medicos;
            ddlMedicoDisponible.DataTextField = "NombreCompleto";
            ddlMedicoDisponible.DataValueField = "IdMedico";
            ddlMedicoDisponible.DataBind();

            ddlMedicoDisponible.Items.Insert(0, new ListItem("-- Seleccione Médico --", ""));
            ddlMedicoDisponible.Enabled = true;

            EspecialidadMuted.Attributes["class"] = "text-success d-block mt-1";
            EspecialidadMuted.InnerText = "Especialidad seleccionada.";
        }

        protected void ddlMedicoDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedicoDisponible.SelectedValue)) return;

            ResetearSelectores(2); // Limpiar Fecha y Hora

            int idMedico = int.Parse(ddlMedicoDisponible.SelectedValue);
            MedicoNegocio medicoNegocio = new MedicoNegocio();

            //días de la semana que trabaja el médico
            List<DayOfWeek> diasTrabajo = medicoNegocio.ObtenerDiasQueTrabaja(idMedico);

            ddlFechaTurno.Items.Clear();

            if (diasTrabajo.Count > 0)
            {
                ddlFechaTurno.Items.Add(new ListItem("-- Seleccione una Fecha Disponible --", ""));

                //fechas para los próximos 30 días
                DateTime fechaBase = DateTime.Today;
                for (int i = 0; i < 30; i++)
                {
                    DateTime fecha = fechaBase.AddDays(i);

                    //si coincide los días de trabajo
                    if (diasTrabajo.Contains(fecha.DayOfWeek))
                    {
                        string nombreDia = medicoNegocio.ObtenerNombreDiaEnEspanol(fecha);
                        string textoMostrar = $"{nombreDia} {fecha:dd/MM/yyyy}";
                        string valor = fecha.ToString("yyyy-MM-dd");

                        ddlFechaTurno.Items.Add(new ListItem(textoMostrar, valor));
                    }
                }

                ddlFechaTurno.Enabled = true;
                MedicoMuted.Attributes["class"] = "text-success d-block mt-1";
                MedicoMuted.InnerText = "Médico seleccionado.";
            }
            else
            {
                ddlFechaTurno.Enabled = false;
                MedicoMuted.Attributes["class"] = "text-danger d-block mt-1";
                MedicoMuted.InnerText = "Este médico no tiene días asignados.";
            }
        }

        protected void ddlFechaTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlFechaTurno.SelectedValue)) 
                return;

            try
            {
                DateTime fechaSeleccionada = DateTime.Parse(ddlFechaTurno.SelectedValue);
                int idMedico = int.Parse(ddlMedicoDisponible.SelectedValue);

                MedicoNegocio medicoNegocio = new MedicoNegocio();

                List<TimeSpan> horariosLibres = medicoNegocio.ObtenerHorariosLibres(idMedico, fechaSeleccionada);

                //si la fecha seleccionada es hoy y horarios que sea mayor a la hora actual
                if (fechaSeleccionada.Date == DateTime.Today)
                {
                    TimeSpan horaActual = DateTime.Now.TimeOfDay;

                    var horariosFiltrados = new List<TimeSpan>();

                    foreach (TimeSpan horario in horariosLibres)
                    {
                        if (horario > horaActual)
                        {
                            horariosFiltrados.Add(horario);
                        }
                    }

                    horariosLibres = horariosFiltrados;
                }

                ddlHorario.Items.Clear();

                if (horariosLibres.Count > 0)
                {
                    foreach (TimeSpan horario in horariosLibres)
                    {
                        ddlHorario.Items.Add(new ListItem(horario.ToString(@"hh\:mm"), horario.ToString()));
                    }

                    ddlHorario.Items.Insert(0, new ListItem("-- Seleccione Hora --", ""));
                    ddlHorario.Enabled = true;

                    FechaMuted.Attributes["class"] = "text-success d-block mt-1";
                    FechaMuted.InnerText = $" Fecha válida. {horariosLibres.Count} horarios disponibles.";
                    HorarioMuted.InnerText = "4. Seleccione un horario.";
                    lblMensajeError.Visible = false;
                }
                else
                {
                    ddlHorario.Enabled = false;
                    FechaMuted.Attributes["class"] = "text-danger d-block mt-1";
                    
                    if (fechaSeleccionada.Date == DateTime.Today)
                        FechaMuted.InnerText = " Ya no quedan horarios disponibles por hoy.";
                    else
                        FechaMuted.InnerText = " Agenda completa para esta fecha.";
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al calcular horarios: " + ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlHorario.SelectedValue))
            {
                HorarioMuted.Attributes["class"] = "text-success d-block mt-1";
                HorarioMuted.InnerText = " Horario seleccionado.";
                VerificarHabilitarGuardado();
            }
        }

        private void ResetearSelectores(int nivel)
        {
            ///Al Cambiar especialidad
            if (nivel <= 1)
            {
                ddlMedicoDisponible.Items.Clear();
                ddlMedicoDisponible.Enabled = false;
                MedicoMuted.Attributes["class"] = "text-muted d-block mt-1";
            }
            ///Al Cambiar médico
            if (nivel <= 2)
            {
                ddlFechaTurno.Items.Clear();
                ddlFechaTurno.Enabled = false;
                FechaMuted.Attributes["class"] = "text-muted d-block mt-1";
                FechaMuted.InnerText = "3. Elija una fecha de la lista.";
            }

            ddlHorario.Items.Clear();
            ddlHorario.Enabled = false;
            HorarioMuted.Attributes["class"] = "text-muted d-block mt-1";
            btnGuardar.Enabled = false;
        }

        private void VerificarHabilitarGuardado()
        {
            bool hayHorario = !string.IsNullOrEmpty(ddlHorario.SelectedValue);
            bool hayPaciente = Session["IdPaciente"] != null || ViewState["IdPaciente"] != null;
            btnGuardar.Enabled = hayHorario && hayPaciente;
        }

        #endregion

        #region Paciente

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            string dni = txtDocumento.Text.Trim();
            if (string.IsNullOrEmpty(dni)) return;

            try
            {
                PacienteNegocio negocio = new PacienteNegocio();
                Paciente pacienteEncontrado = negocio.BuscarPorDni(dni);

                if (pacienteEncontrado != null)
                {
                    pnlDatosPaciente.Visible = true;
                    pnlAgregarPaciente.Visible = false;
                    btnIrAgregarPaciente.Visible = false;
                    lblMensajeError.Visible = false;

                    txtNombrePaciente.Text = pacienteEncontrado.Nombre;
                    txtApellidoPaciente.Text = pacienteEncontrado.Apellido;
                    txtEmailPaciente.Text = pacienteEncontrado.Email;
                    txtTelefonoPaciente.Text = pacienteEncontrado.Telefono;

                    Session["IdPaciente"] = pacienteEncontrado.IdPaciente;
                    lblPacienteEstado.Text = "Encontrado";
                    lblPacienteEstado.CssClass = "badge bg-success";

                    VerificarHabilitarGuardado();
                }
                else
                {
                    pnlDatosPaciente.Visible = false;
                    lblPacienteEstado.Text = "No encontrado";
                    lblPacienteEstado.CssClass = "badge bg-danger";
                    lblMensajeError.Text = "Paciente no encontrado. Regístrelo.";
                    lblMensajeError.Visible = true;
                    btnIrAgregarPaciente.Visible = true;
                    btnGuardar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        protected void btnIrAgregarPaciente_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = true;
            pnlDatosPaciente.Visible = false;
            txtNuevoDni.Text = txtDocumento.Text;
        }

        protected void btnGuardarNuevoPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente pac = new Paciente();
                pac.Dni = txtNuevoDni.Text;
                pac.Nombre = txtNuevoNombre.Text;
                pac.Apellido = txtNuevoApellido.Text;
                pac.Email = txtNuevoEmail.Text;
                pac.Telefono = txtNuevoTelefono.Text;
                pac.Direccion = txtNuevoDireccion.Text;
                if (!string.IsNullOrEmpty(txtNuevoFechaNacimiento.Text))
                    pac.FechaNacimiento = DateTime.Parse(txtNuevoFechaNacimiento.Text);
                pac.Estado = true;

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.GuardarPaciente(pac);

                Paciente guardado = negocio.BuscarPorDni(pac.Dni);
                Session["IdPaciente"] = guardado.IdPaciente;

                pnlAgregarPaciente.Visible = false;
                pnlDatosPaciente.Visible = true;
                txtNombrePaciente.Text = guardado.Nombre;
                txtApellidoPaciente.Text = guardado.Apellido;

                VerificarHabilitarGuardado();
            }
            catch (Exception ex)
            {
                lblMensajeNuevoPaciente.Text = "Error: " + ex.Message;
                lblMensajeNuevoPaciente.Visible = true;
            }
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            pnlAgregarPaciente.Visible = false;
        }

        #endregion

        #region Guardado del Turno

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string resumen = $"<strong>Paciente:</strong> {txtNombrePaciente.Text} {txtApellidoPaciente.Text}<br/>" +
                             $"<strong>Médico:</strong> {ddlMedicoDisponible.SelectedItem.Text}<br/>" +
                             $"<strong>Fecha:</strong> {ddlFechaTurno.SelectedItem.Text} - <strong>Hora:</strong> {ddlHorario.SelectedItem.Text}";

            lblTituloModal.Text = "Confirmar Turno";
            litCuerpoModal.Text = resumen;

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal",
                "var myModal = new bootstrap.Modal(document.getElementById('modalConfirmacion')); myModal.show();", true);
        }

        protected void btnConfirmarModal_Click(object sender, EventArgs e)
        {
            try
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                Turno nuevoTurno = new Turno();

                nuevoTurno.NumeroTurno = (turnoNegocio.ObtenerUltimoID() + 1).ToString();
                nuevoTurno.FechaInicio = DateTime.Parse(ddlFechaTurno.SelectedValue);
                nuevoTurno.FechaFin = nuevoTurno.FechaInicio;

                TimeSpan horaInicio = TimeSpan.Parse(ddlHorario.SelectedValue);
                nuevoTurno.HoraInicio = horaInicio;
                nuevoTurno.HoraFin = horaInicio.Add(TimeSpan.FromHours(1));

                nuevoTurno.ObservacionesSolicitud = txtObservaciones.Text;
                nuevoTurno.ObservacionesDiagnostico = "";
                nuevoTurno.IdMedico = int.Parse(ddlMedicoDisponible.SelectedValue);
                nuevoTurno.IdPaciente = (int)Session["IdPaciente"];
                nuevoTurno.IdEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

                nuevoTurno.Motivo = txtMotivo.Text;
                nuevoTurno.Estado = 1;

                turnoNegocio.AgregarTurno(nuevoTurno);

                string mensajeExtra = "";

                // Enviar Correo Electrónico
                try
                {
                    string emailPaciente = txtEmailPaciente.Text;

                    if (!string.IsNullOrEmpty(emailPaciente))
                    {
                        EmailService emailService = new EmailService();

                        string asunto = $"Confirmación del turno para - {ddlEspecialidad.SelectedItem.Text}";
                        string cuerpo = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                                <h2 style='color: #0d6efd;'>Turno Registrado</h2>
                                <p>Hola <strong>{txtNombrePaciente.Text} {txtApellidoPaciente.Text}</strong>,</p>
                                <p>Su turno ha sido Agendado con éxito en nuestra clínica.</p>
                                <hr />
                                <h3>Detalles del Turno:</h3>
                                <ul>
                                    <li><strong>Especialidad:</strong> {ddlEspecialidad.SelectedItem.Text}</li>
                                    <li><strong>Médico:</strong> {ddlMedicoDisponible.SelectedItem.Text}</li>
                                    <li><strong>Fecha:</strong> {ddlFechaTurno.SelectedItem.Text}</li>
                                    <li><strong>Horario:</strong> {ddlHorario.SelectedItem.Text}</li>
                                </ul>
                                <p>Por favor, preséntese 10 minutos antes.</p>
                                <p style='color: #888; font-size: 12px;'>Este es un mensaje automático, no responda a este correo.</p>
                            </div>
                        ";

                        emailService.EnviarEmail(emailPaciente, asunto, cuerpo);
                    }
                    else
                    {
                        mensajeExtra = "<br/><small class='text-warning'>(No se envió email: Paciente sin correo registrado).</small>";
                    }
                }
                catch (Exception exEmail)
                {
                    // Si falla el email, avisa.
                    mensajeExtra = $"<br/><small class='text-danger'>(El turno se guardó, pero hubo un error enviando el email: {exEmail.Message})</small>";
                }

                //Éxito
                lblTituloMensaje.Text = "¡Éxito!";
                litCuerpoMensaje.Text = "El turno ha sido agendado correctamente." + mensajeExtra;
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalExito",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalMensaje')); myModal.show();", true);
            }
            catch (Exception ex)
            {
                lblTituloModal.Text = "Error";
                litCuerpoModal.Text = "Hubo un error al guardar en base de datos: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalError",
                    "var myModal = new bootstrap.Modal(document.getElementById('modalConfirmacion')); myModal.show();", true);
            }
        }
        

        protected void btnExito_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MainMenu.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MainMenu.aspx");
        }
        #endregion



    }

}