using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;





namespace WebApplicationClinica
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario usuario = new Usuario();    

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();


            try
            {

                usuario.NombreUsuario = TxtUsuario.Text;
                usuario.Password = TxtPassword.Text;

               if(usuarioNegocio.Loguear(usuario)) 
                {
                    Response.Redirect("MainMenu.aspx");
                
                }




            }
            catch (Exception)
            {

                throw;
            }


            


        }
    }
}