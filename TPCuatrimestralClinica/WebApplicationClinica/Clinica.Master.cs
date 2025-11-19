using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebApplicationClinica
{
    public partial class Clinica : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesionYPermisos();
            }
        }

        private void ValidarSesionYPermisos()
        {
            // Si NO hay usuario logueado
            if (Session["usuario"] == null)
            {
                MiBarraNavegacion.Visible = false;
                return; 
            }

           
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            
            MiBarraNavegacion.Visible = true;
            PanelUsuario.Visible = true; 
            LblUser.Text = $"{usuarioLogueado.TipoUsuario} {usuarioLogueado.NombreUsuario}";
            LiMenu.Visible = true;

            switch (usuarioLogueado.TipoUsuario)
            {
                case TipoUsuario.Admin:
                    LiCrearTurno.Visible = true;
                    LiGestionTurnos.Visible = true;
                    LiPacientes.Visible = true;
                    LiMenuMedico.Visible = true;
                    LiGestionMedicos.Visible = true;
                    LiEspecialidades.Visible = true;
                    LiCrearUsuario.Visible = true;

                    break;

                case TipoUsuario.Recepcion:
                    LiCrearTurno.Visible = true;
                    LiGestionTurnos.Visible = true;
                    LiPacientes.Visible = true;
                    LiGestionMedicos.Visible = true;

                    LiMenuMedico.Visible = false;
                    LiEspecialidades.Visible = false;
                    LiCrearUsuario.Visible = false;
                    break;

                case TipoUsuario.Medico:
                    LiMenuMedico.Visible = true;
                    LiGestionMedicos.Visible = true;

                    LiMenu.Visible = false;
                    LiCrearTurno.Visible = false;
                    LiPacientes.Visible = false;
                    LiGestionTurnos.Visible = false;
                    LiEspecialidades.Visible = false;
                    LiCrearUsuario.Visible = false;
                    break;
            }
        }
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            // cerrar sesión
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login y Usuarios/Login.aspx");
        }
    }
}