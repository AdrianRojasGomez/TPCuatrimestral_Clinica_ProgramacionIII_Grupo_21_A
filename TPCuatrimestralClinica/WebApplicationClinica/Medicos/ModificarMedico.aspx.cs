using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebApplicationClinica.Medicos
{
    public partial class WebForm3 : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();

        }

        protected void gvMedicos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMedicos_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMedicos.PageIndex = e.NewPageIndex;
            CargarGrid(txtBuscarMedico.Text); 


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

        public void CargarGrilla()
        {
            // === Turnos de ejemplo ===
            var turnoManiana = new TurnoTrabajo
            {
                IdTurnoTrabajo = 1,
                Nombre = "Mañana",
                HoraInicio = new TimeSpan(8, 0, 0),
                HoraFin = new TimeSpan(12, 0, 0)
            };

            var turnoTarde = new TurnoTrabajo
            {
                IdTurnoTrabajo = 2,
                Nombre = "Tarde",
                HoraInicio = new TimeSpan(14, 0, 0),
                HoraFin = new TimeSpan(18, 0, 0)
            };

            // === Lista de médicos de ejemplo ===
            var listaMedicos = new List<Medico>
            {
                new Medico
                {
                    IdMedico = 1,
                    Nombre = "Ana",
                    Apellido = "López",
                    Matricula = "M1234",
                    TurnoTrabajo = turnoManiana,
                    Especialidades = new List<Especialidad>
                    {
                        new Especialidad { IdEspecialidad = 1, Nombre = "Clínica" },
                        new Especialidad { IdEspecialidad = 2, Nombre = "Pediatría" }
                    }
                },
                new Medico
                {
                    IdMedico = 2,
                    Nombre = "Matias",
                    Apellido = "Gómez",
                    Matricula = "M5678",
                    TurnoTrabajo = turnoTarde,
                    Especialidades = new List<Especialidad>
                    {
                        new Especialidad { IdEspecialidad = 3, Nombre = "Cardiología" }
                    }
                }
            };
            Session["ListaMedicos"] = listaMedicos;

            // === Enlazar al GridView ===
            gvMedicos.DataSource = listaMedicos;
            gvMedicos.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarMedico.Text = "";
            gvMedicos.PageIndex = 0;
            CargarGrid("");
        }

        protected void btnBuscarMedico_Click(object sender, EventArgs e)
        {

        }

        public void CargarGrid(string filtro)
        {
            var lista = Session["ListaMedicos"] as List<Dominio.Medico>;
            if (lista == null) lista = new List<Dominio.Medico>();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                string f = filtro.Trim().ToLower();
                lista = lista
                    .Where(m =>
                        (!string.IsNullOrEmpty(m.Nombre) && m.Nombre.ToLower().Contains(f)) ||
                        (!string.IsNullOrEmpty(m.Apellido) && m.Apellido.ToLower().Contains(f))
                    )
                    .ToList();
            }

            gvMedicos.DataSource = lista;
            gvMedicos.DataBind();
        }

        protected void txtBuscarMedico_TextChanged(object sender, EventArgs e)
        {



            gvMedicos.PageIndex = 0;
            CargarGrid(txtBuscarMedico.Text);

        }
    }



}