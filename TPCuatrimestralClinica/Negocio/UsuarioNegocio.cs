using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                accesoDatos.SetearParametros("@pass", usuario.Password);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {

                    usuario.IdUsuario = (int)accesoDatos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)(accesoDatos.Lector["TipoUusario"]) == 1 ? TipoUsuario.Admin : (int)(accesoDatos.Lector["TipoUusario"]) == 2 ? TipoUsuario.Medico : TipoUsuario.Recepcion;


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

        public void GuardarUsuario(Usuario usuario)
        {


             AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(
                    "INSERT INTO UsuariosApp (NombreUsuario, Clave, TipoUusario, IdMedico) " +
                    "VALUES (@NombreUsuario, @Clave, @TipoUsuario, @IdMedico)"
                );

                datos.SetearParametros("@NombreUsuario", usuario.NombreUsuario);
                datos.SetearParametros("@Clave", usuario.Password);
                datos.SetearParametros("@TipoUsuario", (int)usuario.TipoUsuario);

                if (usuario.IdMedicoAsociado > 0)
                    datos.SetearParametros("@IdMedico", usuario.IdMedicoAsociado);
                else
                    datos.SetearParametros("@IdMedico", DBNull.Value);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }




        }


     






    }
}
