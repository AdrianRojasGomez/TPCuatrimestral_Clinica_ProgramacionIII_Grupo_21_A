using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {


        public bool Loguear(Usuario usuario)
        {

            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta("select IdUsuario,TipoUusario from UsuariosApp where  NombreUsuario = @user and Clave = @pass");

                accesoDatos.SetearParametros("@user", usuario.NombreUsuario);
                accesoDatos.SetearParametros("@pass" ,usuario.Password);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {

                    usuario.IdUsuario = (int)accesoDatos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)(accesoDatos.Lector["TipoUusario"])== 1 ? TipoUsuario.Admin : (int)(accesoDatos.Lector["TipoUusario"]) == 2 ? TipoUsuario.Medico : TipoUsuario.Recepcion;


                    return true;


                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                accesoDatos.CerrarConexion();

            }

        }

    }
}
