using Dominio;
using System;
using System.Collections.Generic;
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
            var listaMedicos = new List<Medico>
    {
        new Medico
        {
            IdMedico = 1,
            Nombre = "Ana",
            Apellido = "López",
            Matricula = "M1234",
            TurnoTrabajo = new TurnoTrabajo
            {
                IdTurnoTrabajo = 1,
                Nombre = "Mañana",
                HoraInicio = new TimeSpan(8, 0, 0),
                HoraFin = new TimeSpan(12, 0, 0)
            },
            Especialidades = new List<Especialidad>
            {
                new Especialidad { IdEspecialidad = 1, Nombre = "Clínica" },
                new Especialidad { IdEspecialidad = 2, Nombre = "Pediatría" }
            }
        },
        new Medico
        {
            IdMedico = 2,
            Nombre = "Matías",
            Apellido = "Gómez",
            Matricula = "M5678",
            TurnoTrabajo = new TurnoTrabajo
            {
                IdTurnoTrabajo = 2,
                Nombre = "Tarde",
                HoraInicio = new TimeSpan(14, 0, 0),
                HoraFin = new TimeSpan(18, 0, 0)
            },
            Especialidades = new List<Especialidad>
            {
                new Especialidad { IdEspecialidad = 3, Nombre = "Cardiología" }
            }
        }
    };

            // Asignar al GridView
            dvMedicos.DataSource = listaMedicos;
            dvMedicos.DataBind();


        }

        protected void btnBotonLimpiarMedico_Click(object sender, EventArgs e)
        {

        }

        protected void dvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}