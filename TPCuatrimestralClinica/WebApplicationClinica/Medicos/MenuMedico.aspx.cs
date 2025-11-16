using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                Medico medico = medicoNegocio.BuscarMedicoPorIdSimple(idMedico);
                                if (medico != null)
                {
                    lblNombreDoctor.Text = "Dr. " + medico.Nombre + " " + medico.Apellido;
                    lblEspecialidad.Text = medico.Especialidades != null && medico.Especialidades.Count > 0
                        ? medico.Especialidades[0].Nombre
                        : "Sin especialidad";
                }


            }
        }

        protected void txtNombreDoctor_TextChanged(object sender, EventArgs e)
        {

        }

        protected void NombreConsultorio_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txteNombreConsultorio_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtNombreEspecialidad_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnLLamarPaciente_Click(object sender, EventArgs e)
        {

        }

        protected void btnAtender_Click(object sender, EventArgs e)
        {

        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnReprogramar_Click(object sender, EventArgs e)
        {

        }

        protected void btnSiguientePaciente_Click(object sender, EventArgs e)
        {

        }

        protected void txteBuscarPaciente_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnLlamarProxPaciente_Click(object sender, EventArgs e)
        {

        }
    }
}