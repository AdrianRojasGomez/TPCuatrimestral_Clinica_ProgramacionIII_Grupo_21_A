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
                LblCargar.Text = "Cargue usuario y contraseña, por favor.";
                return; 
            }
            try
            {


                usuario.NombreUsuario = TxtUsuario.Text;
                usuario.Password = TxtPassword.Text;

                if (usuarioNegocio.Loguear(usuario))
                {
                    usuario.Password = null;
                    Session.Add("usuario", usuario);
                   

                    if (usuario.TipoUsuario == TipoUsuario.Medico)
                    {

                        Response.Redirect("Medicos/MenuMedico.aspx");

                    }
                    else if (usuario.TipoUsuario == TipoUsuario.Admin)
                    {

                        Response.Redirect("MainMenu.aspx");

                    }

                    else if (usuario.TipoUsuario == TipoUsuario.Recepcion) 
                    { 
                         
                        
                    
                    } 
                  


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