using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
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



            


        }

        public static bool PuedeVerTurnos(HttpSessionState session)
        {

            if (session["usuario"] != null && ((Dominio.Usuario)session["usuario"]).TipoUsuario == Dominio.TipoUsuario.Admin)
            {
                
                return true;
            }
            else
            {
               
                return false;
            }


        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if (TxtPassword.Text == "" || TxtUsuario.Text == "")
            {


                LblCargar.Text = "Cargue usuario y contraseña porfavor";
            }
            try
            {


                usuario.NombreUsuario = TxtUsuario.Text;
                usuario.Password = TxtPassword.Text;

                if (usuarioNegocio.Loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("MainMenu.aspx");

                }
                else
                {

                    Response.Redirect("Error.aspx");
                }




            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}