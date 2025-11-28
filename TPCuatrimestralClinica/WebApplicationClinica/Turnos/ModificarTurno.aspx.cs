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
                //if (Session["IdAModificar"] == null)
                //{
                //    Response.Redirect("GestionTurno.aspx");
                //}
                Session["IdAModificar"] = 2; // PARA PRUEBAS, QUITAR DESPUÉS
            }

            int idTurno = (int)Session["IdAModificar"];

            TurnoNegocio negocio = new TurnoNegocio();
            PacienteNegocio negocioPaciente = new PacienteNegocio();
            MedicoNegocio negocioMedico = new MedicoNegocio();
            EspecialidadNegocio negocioEspecialidad = new EspecialidadNegocio();


            Turno turno = negocio.BuscarPorId(idTurno);
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
            







        }



        #endregion

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlMedicoDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: cargar fechas disponibles según médico
        }

        protected void ddlFechaTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: cargar horarios según fecha seleccionada
        }

        protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: opcional, mostrar algo cuando se selecciona horario
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Por ahora, redirigimos a la lista de turnos
            Response.Redirect("~/GestionTurnos.aspx");
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
            Response.Redirect("~/GestionTurnos.aspx");
        }
    }
}