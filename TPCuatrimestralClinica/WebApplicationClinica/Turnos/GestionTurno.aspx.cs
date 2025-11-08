using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationClinica.Turnos
{
    public partial class GestionTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTurnos();
        }

        void CargarTurnos(string filtro = "")
        {
            try
            {
                TurnoNegocio negocio = new TurnoNegocio();
                var dt = negocio.Listar(filtro);
                gvTurnos.DataSource = dt;
                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pacientes: " + ex.Message);
            }
        }

    }
}