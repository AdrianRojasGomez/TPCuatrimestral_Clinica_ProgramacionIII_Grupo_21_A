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

        public List<Usuario> listarusuario()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta(@"
            SELECT 
                u.IdUsuario,
                u.NombreUsuario,
                u.Clave,
                u.TipoUsuario,
                u.Estado,
                um.IdMedico,
                m.Nombre AS NombreMedico,
                m.Apellido,
                m.Matricula
            FROM UsuariosApp u
            LEFT JOIN UsuariosAppxMedico um ON um.IdUsuario = u.IdUsuario
            LEFT JOIN Medicos m ON m.IdMedico = um.IdMedico
            ORDER BY u.IdUsuario DESC");

                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.IdUsuario = (int)accesoDatos.Lector["IdUsuario"];
                    aux.NombreUsuario = (string)accesoDatos.Lector["NombreUsuario"];
                    aux.Password = (string)accesoDatos.Lector["Clave"];
                    aux.TipoUsuario = (TipoUsuario)(int)accesoDatos.Lector["TipoUsuario"];
                    aux.Activo = (bool)accesoDatos.Lector["Estado"];

                   
                    if (accesoDatos.Lector["IdMedico"] != DBNull.Value)
                    {
                        aux.IdMedicoAsociado = (int)accesoDatos.Lector["IdMedico"];

                        aux.Medico = new Medico();
                        aux.Medico.IdMedico = (int)accesoDatos.Lector["IdMedico"];
                        aux.Medico.Nombre = accesoDatos.Lector["NombreMedico"].ToString();
                        aux.Medico.Apellido = accesoDatos.Lector["Apellido"].ToString();
                        aux.Medico.Matricula = accesoDatos.Lector["Matricula"].ToString();
                    }

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }





        }

        public bool Loguear(Usuario usuario)
        {

            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta("select IdUsuario,TipoUsuario from UsuariosApp where  NombreUsuario = @user and Clave = @pass");

                accesoDatos.SetearParametros("@user", usuario.NombreUsuario);
                accesoDatos.SetearParametros("@pass", usuario.Password);
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {

                    usuario.IdUsuario = (int)accesoDatos.Lector["IdUsuario"];
                    usuario.TipoUsuario = (int)(accesoDatos.Lector["TipoUsuario"]) == 1 ? TipoUsuario.Admin : TipoUsuario.Recepcion;


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

        public bool loguearmedico(Usuario usuario) 
        {


            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta(@"
        SELECT u.IdUsuario, 
               u.TipoUsuario,
               um.IdMedico   
        FROM UsuariosApp u
        INNER JOIN UsuariosAppxMedico um ON u.IdUsuario = um.IdUsuario
        WHERE u.NombreUsuario = @user 
          AND u.Clave = @pass 
          AND u.TipoUsuario = 2 
          AND u.Estado = 1");

                accesoDatos.SetearParametros("@user", usuario.NombreUsuario);
                accesoDatos.SetearParametros("@pass", usuario.Password);
                accesoDatos.EjecutarLectura();




                if (accesoDatos.Lector.Read())
                {
                    usuario.IdUsuario = (int)accesoDatos.Lector["IdUsuario"];
                    usuario.TipoUsuario = TipoUsuario.Medico;

                    
                    usuario.IdMedicoAsociado = (int)accesoDatos.Lector["IdMedico"];

                

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
                    "INSERT INTO UsuariosApp (NombreUsuario, Clave, TipoUsuario, Estado) " +
                    "VALUES (@NombreUsuario, @Clave, @TipoUsuario, 1); " +
                    "SELECT SCOPE_IDENTITY();"
                );

                datos.SetearParametros("@NombreUsuario", usuario.NombreUsuario);
                datos.SetearParametros("@Clave", usuario.Password);
                datos.SetearParametros("@TipoUsuario", (int)usuario.TipoUsuario);


                int idUsuarioNuevo = Convert.ToInt32(datos.EjecutarEscalar());
                datos.CerrarConexion();


                if (usuario.IdMedicoAsociado > 0)
                {
                    AccesoDatos datosRelacion = new AccesoDatos();
                    try
                    {
                        datosRelacion.SetearConsulta(
                            "INSERT INTO UsuariosAppxMedico (IdUsuario, IdMedico) " +
                            "VALUES (@IdUsuario, @IdMedico)"
                        );

                        datosRelacion.SetearParametros("@IdUsuario", idUsuarioNuevo);
                        datosRelacion.SetearParametros("@IdMedico", usuario.IdMedicoAsociado);

                        datosRelacion.EjecutarAccion();
                    }
                    finally
                    {
                        datosRelacion.CerrarConexion();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            datos.CerrarConexion();
        }
    







        public void CambiarEstadoUsuario(int idUsuario, bool activar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE UsuariosApp SET Estado = @Estado WHERE IdUsuario = @IdUsuario");
                datos.SetearParametros("@Estado", activar);
                datos.SetearParametros("@IdUsuario", idUsuario);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public int ObtenerIdUsuarioPorMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "SELECT TOP 1 IdUsuario " +
                    "FROM UsuariosAppxMedico " +
                    "WHERE IdMedico = @IdMedico"
                );

                datos.SetearParametros("@IdMedico", idMedico);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["IdUsuario"];

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }








    }
}
