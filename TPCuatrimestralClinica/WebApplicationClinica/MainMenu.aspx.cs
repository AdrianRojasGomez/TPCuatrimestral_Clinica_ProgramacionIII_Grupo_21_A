using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica
{
    public partial class MainMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
               return;
            }


            if (Login.PuedeVerTurnos(Session) == TipoUsuario.Recepcion)
            {

                btnCrearTurno.Visible = true;

                BtnCrear.Visible = false;

            }

            else if (Login.PuedeVerTurnos(Session) == TipoUsuario.Medico)
            {
                btnCrearTurno.Visible = false;

                BtnCrear.Visible = false;

                BtnAltaPaciente.Visible = false;


            }
            else if (Login.PuedeVerTurnos(Session) == TipoUsuario.SinDefinir) {


                Response.Redirect("Login.aspx");

                return;
            
            }*/


                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        }

        protected void BtnAltaPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes/Pacientes.aspx");
        }

        protected void btnAgregarMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Medicos/AgregarMedico.aspx");
        }
    }
}