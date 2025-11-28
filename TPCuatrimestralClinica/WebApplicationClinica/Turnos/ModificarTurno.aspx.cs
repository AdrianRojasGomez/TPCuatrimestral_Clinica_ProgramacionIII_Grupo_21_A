using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebApplicationClinica.Turnos
{
    public partial class ModificarTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdAModificar"] == null)
                {
                    Response.Redirect("~/Turnos/GestionTurno.aspx");
                }
                //Session["IdAModificar"] = 77; // PARA PRUEBAS, QUITAR DESPUÉS
            }

            int idTurno = (int)Session["IdAModificar"];

            TurnoNegocio negocio = new TurnoNegocio();
            PacienteNegocio negocioPaciente = new PacienteNegocio();
            MedicoNegocio negocioMedico = new MedicoNegocio();
            EspecialidadNegocio negocioEspecialidad = new EspecialidadNegocio();


            Turno turno = negocio.BuscarPorId(idTurno);
            txtDocumento.Text = turno.IdEspecialidad.ToString();
            Paciente paciente = negocioPaciente.BuscarPorId(turno.IdPaciente);
            Medico medico = negocioMedico.BuscarMedicoPorIdSimple(turno.IdMedico);
            Especialidad especialidad = negocioEspecialidad.ObtenerEspecialidad(turno.IdEspecialidad);
            
            

            //LLENAR CAMPOS
            LlenarDatosPaciente(paciente);
            LlenarDatosTurno(turno, medico, especialidad);



        }
        #region Llenar Datos
        private void LlenarDatosPaciente(Paciente paciente)
        {
            txtDocumento.Text = paciente.Dni;
            txtNombrePaciente.Text = paciente.Nombre;
            txtApellidoPaciente.Text = paciente.Apellido;
            txtTelefonoPaciente.Text = paciente.Telefono;
            txtEmailPaciente.Text = paciente.Email;
            txtDireccionPaciente.Text = paciente.Direccion;
        }
        private void LlenarDatosTurno(Turno turno, Medico medico, Especialidad especialidad)
        {
            ddlEspecialidad.DataSource = null;
            ddlEspecialidad.Items.Clear();
            ddlEspecialidad.Items.Add(
                new ListItem(especialidad.Nombre, especialidad.IdEspecialidad.ToString())
            );
            ddlEspecialidad.Enabled = false;

            MedicoNegocio negocioMedico = new MedicoNegocio();
            List<Medico> medicosDisponibles = negocioMedico.ListarPorEspecialidad(especialidad.IdEspecialidad);

            ddlMedicoDisponible.Items.Clear();
            ddlMedicoDisponible.DataSource = medicosDisponibles;
            ddlMedicoDisponible.DataTextField = "NombreCompleto";
            ddlMedicoDisponible.DataValueField = "IdMedico";
            ddlMedicoDisponible.DataBind();
            

            txtMotivo.Text = turno.Motivo;
            txtMotivo.Enabled = false;
            txtObservaciones.Text = turno.ObservacionesSolicitud;

        }

        #endregion

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///No se puede cambiar la especialidad al modificar el turno
        }

        protected void ddlMedicoDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedicoDisponible.SelectedValue))
                return;

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
                
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Turnos/GestionTurno.aspx");
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            // TODO: validar datos y abrir modal de confirmación
            // Por ahora no hace nada para que no rompa
        }

        protected void btnConfirmarModal_Click(object sender, EventArgs e)
        {
            // TODO: hacer el UPDATE del turno en la base de datos
            // y luego mostrar el modal de éxito
        }

        protected void btnExito_Click(object sender, EventArgs e)
        {
            // Después de confirmar, volver a la página de gestión
            Response.Redirect("~/Turnos/GestionTurno.aspx");
        }
    }
}