using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idMedico"] == null)
                {
                    Response.Redirect("~/Login%20y%20Usuarios/Login.aspx");
                    return;
                }

                int idMedico = (int)Session["idMedico"];


                CargarDatosDoctor(idMedico);

                CargarTurnosDelDia(idMedico);
            }
        }


        #region LOGICA DE CARGA DE DATOS
        private void CargarDatosDoctor(int idMedico)
        {
            try
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();

                Medico medico = medicoNegocio.BuscarMedicoPorIdSimple(idMedico);

                if (medico != null)
                {
                    lblNombreDoctor.Text = "Dr. " + medico.Nombre + " " + medico.Apellido;
                    if (medico.Especialidades != null && medico.Especialidades.Count > 0)
                        lblEspecialidad.Text = medico.Especialidades[0].Nombre;
                    else
                        lblEspecialidad.Text = "General";
                }
            }
            catch (Exception)
            {
                lblNombreDoctor.Text = "Médico no encontrado";
            }
        }

        private void CargarTurnosDelDia(int idMedico)
        {
            try
            {
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                List<Turno> todosLosTurnos = turnoNegocio.ObtenerTodosLosTurnos();

                //Solo de este médico Y Solo fecha de HOY
                DateTime hoy = DateTime.Today;

                var turnosFiltrados = todosLosTurnos.Where(t =>
                    t.IdMedico == idMedico &&
                    t.FechaInicio.Date == hoy
                ).OrderBy(t => t.HoraInicio).ToList();

                // Repeater
                var listaVisual = turnosFiltrados.Select(t => new
                {
                    IdTurno = t.IdTurno,
                    CodigoTurno = "#" + t.IdTurno,
                    NombrePaciente = t.Paciente != null ? (t.Paciente.Nombre + " " + t.Paciente.Apellido) : "Desconocido",
                    HoraTurno = t.HoraInicio.ToString(@"hh\:mm"),
                    Motivo = t.Motivo,
                    EstadoTurno = t.Estado,
                    FullTurno = t
                }).ToList();

                //datos al Repeater
                rptColaTurnos.DataSource = listaVisual;
                rptColaTurnos.DataBind();

                // mensaje si no hay turnos
                litSinTurnos.Visible = listaVisual.Count == 0;
            }
            catch (Exception ex)
            {
                litSinTurnos.Text = "<div class='alert alert-danger p-2'>Error: " + ex.Message + "</div>";
                litSinTurnos.Visible = true;
            }
        }
        #endregion

        #region EVENTOS DE CONTROLES (Repeater y Botones)

        //al hacer clic en un turno de la lista
        protected void rptColaTurnos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                // Recuperamos el ID del CommandArgument
                int idTurno = Convert.ToInt32(e.CommandArgument);
                CargarTurnoSeleccionado(idTurno);
            }
        }
        
        private void CargarTurnoSeleccionado(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            Turno turno = negocio.BuscarPorId(idTurno);

            if (turno != null)
            {
                //campos del panel izquierdo
                hdnIdTurnoSeleccionado.Value = turno.IdTurno.ToString();
                lblturnoActual.Text = "#" + turno.IdTurno;

                if (turno.Paciente != null)
                {
                    lblPaciente.Text = $"{turno.Paciente.Nombre} {turno.Paciente.Apellido} (DNI {turno.Paciente.Dni})";
                }
                else
                {
                    lblPaciente.Text = "Paciente no identificado";
                }

                lblMotivoConsulta.Text = turno.Motivo;

                // Cargamos observaciones existentes si las hay
                txtObservaciones.Text = turno.ObservacionesSolicitud;
                txtDiagnostico.Enabled = false;

                // Habilitamos el panel para editar
                pnlDetalleTurno.Enabled = true;

                // Feedback visual
                litMensajeSeleccion.Text = $"Turno cargado: <strong>{lblPaciente.Text}</strong>";
                pnlAlertaSeleccion.Visible = true;


                if (turno.Estado == (int)EstadoTurno.EstadoEnum.Atendiendo)
                {
                    // Habilitar diagnóstico
                    txtDiagnostico.Enabled = true;
                }
            }
        }

        #endregion

        #region Botones de acción 
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Session["idMedico"] != null)
            {
                CargarTurnosDelDia((int)Session["idMedico"]);
            }
        }
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdTurnoSeleccionado.Value))
            {
                int idTurno = int.Parse(hdnIdTurnoSeleccionado.Value);
                if(ObtenerEstadoActual(idTurno).Estado != EstadoTurno.EstadoEnum.Atendiendo)
                {
                    MostrarMensaje("Error","El turno debe estar 'Atendido' para poder finalizarlo.");
                    return;
                }

                try
                {
                    string diagnostico = txtDiagnostico.Text;
                    TurnoNegocio negocio = new TurnoNegocio();
                    negocio.ModificarDiagnostico(idTurno, diagnostico);
                    negocio.ModificarEstado(idTurno, (int)EstadoTurno.EstadoEnum.Completado);
                    MostrarMensaje("Turno Finalizado", "El turno ha sido finalizado correctamente.");

                    // Recargar lista para ver cambios 
                    CargarTurnosDelDia((int)Session["idMedico"]);

                    CargarTurnoSeleccionado(idTurno);

                }
                catch (Exception ex)
                {
                    throw ex;
                    // Manejar error
                }

            }
        }

        protected void btnAtender_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdTurnoSeleccionado.Value))
            {
                int idTurno = int.Parse(hdnIdTurnoSeleccionado.Value);
                if (ObtenerEstadoActual(idTurno).Estado != EstadoTurno.EstadoEnum.Pendiente)
                {
                    MostrarMensaje("Error", "El turno debe estar 'Pendiente' para poder atenderlo. ");
                    return;
                }

                try
                {
                    string diagnostico = txtDiagnostico.Text;
                    TurnoNegocio negocio = new TurnoNegocio();
                    negocio.ModificarDiagnostico(idTurno, diagnostico);
                    negocio.ModificarEstado(idTurno, (int)EstadoTurno.EstadoEnum.Atendiendo);
                    // Habilitar diagnóstico
                    txtDiagnostico.Enabled = true;

                    // Recargar lista para ver cambios 
                    CargarTurnosDelDia((int)Session["idMedico"]);
                }
                catch (Exception ex)
                {
                    throw ex;
                    // Manejar error
                }
            }
        }

        protected void btnNoAsistio_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdTurnoSeleccionado.Value))
            {
                int idTurno = int.Parse(hdnIdTurnoSeleccionado.Value);
                if (ObtenerEstadoActual(idTurno).Estado != EstadoTurno.EstadoEnum.Pendiente)
                {
                    MostrarMensaje("Error", "El turno debe estar 'Pendiente' para poder Cancelar por inasistencia.");
                    return;
                }
                try
                {
                    string diagnostico = txtDiagnostico.Text;
                    TurnoNegocio negocio = new TurnoNegocio();
                    negocio.ModificarDiagnostico(idTurno, diagnostico);
                    negocio.ModificarEstado(idTurno, (int)EstadoTurno.EstadoEnum.NoAsistio);
                    // Habilitar diagnóstico
                    txtDiagnostico.Enabled = true;


                    // Recargar lista para ver cambios 
                    CargarTurnosDelDia((int)Session["idMedico"]);
                }
                catch (Exception ex)
                {
                    throw ex;
                    // Manejar error
                }
            }
        }

        #endregion

        #region HELPERS 
        public string GetStatusBadgeClass(object estadoObj)
        {
            if (estadoObj == null) return "secondary";
            int estado = Convert.ToInt32(estadoObj);

            switch (estado)
            {
                case 0: return "danger";
                case 1: return "warning";
                case 2: return "primary";
                case 3: return "success";
                case 4: return "dark";
                default: return "secondary";
            }

        }
        public string GetNombreEstado(object estadoObj)
        {
            if (estadoObj == null) return "-";
            int estado = Convert.ToInt32(estadoObj);
            switch (estado)
            {
                case 0: return "Cancelado";
                case 1: return "Pendiente";
                case 2: return "Atendiendo";
                case 3: return "Completado";
                case 4: return "No asistió";
                default: return "Desconocido";
            }
        }

        protected void MostrarMensaje(string titulo, string mensaje)
        {
            litModalTitulo.Text = titulo;
            litModalCuerpo.Text = mensaje;

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "modalSistema",
                "var m = new bootstrap.Modal(document.getElementById('modalSistema')); m.show();",
                true
            );
        }

        private EstadoTurno ObtenerEstadoActual(int idTurno)
        {
            TurnoNegocio negocio = new TurnoNegocio();
            Turno turno = negocio.BuscarPorId(idTurno);
            if (turno != null)
            {
                EstadoTurno estadoTurno = new EstadoTurno
                {
                    Estado = (EstadoTurno.EstadoEnum)turno.Estado
                };
                return estadoTurno;
            }
            return null;
        }

        #endregion

    }
}