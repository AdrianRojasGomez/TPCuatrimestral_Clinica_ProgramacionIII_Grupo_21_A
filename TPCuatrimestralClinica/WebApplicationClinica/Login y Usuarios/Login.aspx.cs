using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;





namespace WebApplicationClinica
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                var navBar = Master.FindControl("MiBarraNavegacion");
                if (navBar != null)
                {
                    navBar.Visible = false;
                }
            }
        }

        public static Dominio.TipoUsuario PuedeVerTurnos(HttpSessionState session)
        {

            if (session["usuario"] != null )
            {
                return ((Dominio.Usuario)session["usuario"]).TipoUsuario;
            }

            else
            {
                  return Dominio.TipoUsuario.SinDefinir;
            }     
                         
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                     
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text) || string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                LblCargar.Visible = true;
                LblCargar.Text = "Cargue usuario y contraseña, por favor.";
                return; 
            }
            try
            {
                usuario.NombreUsuario = TxtUsuario.Text;
                usuario.Password = TxtPassword.Text;
                                
                if (usuarioNegocio.loguearmedico(usuario))
                {
                    usuario.Password = null;

                    Session["usuario"] = usuario;
                    Session["idMedico"] = usuario.IdMedicoAsociado; 

                    Response.Redirect("~/Medicos/MenuMedico.aspx");
                    return;
                }
                              
                if (usuarioNegocio.Loguear(usuario))
                {
                    usuario.Password = null;
                    Session["usuario"] = usuario;

                    if (usuario.TipoUsuario == TipoUsuario.Admin)
                    {
                        Response.Redirect("~/MainMenu.aspx");
                    }
                    else if (usuario.TipoUsuario == TipoUsuario.Recepcion)
                    {
                        Response.Redirect("~/MainMenu.aspx");
                    }

                    return;
                }

                LblCargar.Visible = true;
                Response.Redirect("~/Error.aspx");
               /// LblCargar.Text = "Usuario o contraseña incorrectos.";
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}