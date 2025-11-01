using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MedicoNegocio
    {


        public List<Medico> ListarMedicos()
        {

            List<Medico> lista = new List<Medico>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta(@"
            SELECT 
                m.IdMedico,
                m.Nombre,
                m.Apellido,
                m.Matricula,
                t.IdTurnoTrabajo,
                t.Nombre AS TurnoNombre,
                t.HoraInicio,
                t.HoraFin,
                e.IdEspecialidad,
                e.Nombre AS EspecialidadNombre
            FROM Medico m
            LEFT JOIN TurnoTrabajo t ON t.IdTurnoTrabajo = m.IdTurnoTrabajo
            LEFT JOIN MedicoEspecialidad me ON me.IdMedico = m.IdMedico
            LEFT JOIN Especialidad e ON e.IdEspecialidad = me.IdEspecialidad
            ORDER BY m.Apellido, m.Nombre, e.Nombre");
                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.IdMedico = (int)accesoDatos.Lector["IdMedico"];
                    aux.Nombre = (string)accesoDatos.Lector["Nombre"];
                    aux.Apellido = (string)accesoDatos.Lector["Apellido"];
                    aux.Matricula = (string)accesoDatos.Lector["Matricula"];


                    aux.TurnoTrabajo = new TurnoTrabajo();
                    if (accesoDatos.Lector["IdTurnoTrabajo"] != DBNull.Value)
                    {
                        aux.TurnoTrabajo.IdTurnoTrabajo = (int)accesoDatos.Lector["IdTurnoTrabajo"];
                        aux.TurnoTrabajo.Nombre = accesoDatos.Lector["TurnoNombre"].ToString();
                        aux.TurnoTrabajo.HoraInicio = (TimeSpan)accesoDatos.Lector["HoraInicio"];
                        aux.TurnoTrabajo.HoraFin = (TimeSpan)accesoDatos.Lector["HoraFin"];
                    }


                    aux.Especialidades = new List<Especialidad>();
                    if (accesoDatos.Lector["IdEspecialidad"] != DBNull.Value)
                    {
                        Especialidad esp = new Especialidad();
                        esp.IdEspecialidad = (int)accesoDatos.Lector["IdEspecialidad"];
                        esp.Nombre = (string)accesoDatos.Lector["EspecialidadNombre"];
                        aux.Especialidades.Add(esp);
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

        public void AgregarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.SetearConsulta(
              "INSERT INTO Medico (Nombre, Apellido, Matricula) " +
              "VALUES (@Nombre, @Apellido, @Matricula); " +
              "SELECT CAST(SCOPE_IDENTITY() AS INT);"
          );
                datos.SetearParametros("@Nombre", medico.Nombre);
                datos.SetearParametros("@Apellido", medico.Apellido);
                datos.SetearParametros("@Matricula", medico.Matricula);

               
                int idMedicoNuevo = Convert.ToInt32(datos.EjecutarEscalar());
                datos.CerrarConexion();

                datos = new AccesoDatos();
                datos.SetearConsulta(
                    "INSERT INTO MedicoTurno (IdMedico, IdTurnoTrabajo) " +
                    "VALUES (@IdMedico, @IdTurnoTrabajo)"
                );
                datos.SetearParametros("@IdMedico", idMedicoNuevo);
                datos.SetearParametros("@IdTurnoTrabajo", medico.TurnoTrabajo.IdTurnoTrabajo);
                datos.EjecutarAccion();
                datos.CerrarConexion();

                
                datos = new AccesoDatos();
                datos.SetearConsulta(
                    "INSERT INTO MedicoEspecialidad (IdMedico, IdEspecialidad) " +
                    "VALUES (@IdMedico, @IdEspecialidad)"
                );
                datos.SetearParametros("@IdMedico", idMedicoNuevo);
                datos.SetearParametros("@IdEspecialidad", medico.Especialidades[0].IdEspecialidad);
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

        public void ModificarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
            UPDATE Medico
               SET Nombre = @Nombre,
                   Apellido = @Apellido,
                   Matricula = @Matricula,
                   IdTurnoTrabajo = @IdTurnoTrabajo
             WHERE IdMedico = @IdMedico");

                datos.SetearParametros("@IdMedico", medico.IdMedico);
                datos.SetearParametros("@Nombre", medico.Nombre);
                datos.SetearParametros("@Apellido", medico.Apellido);
                datos.SetearParametros("@Matricula", medico.Matricula);
                datos.SetearParametros("@IdTurnoTrabajo", medico.TurnoTrabajo.IdTurnoTrabajo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void EliminarMedico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM MedicoEspecialidad WHERE IdMedico = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarAccion();
                datos.CerrarConexion();


                datos = new AccesoDatos();
                datos.SetearConsulta("DELETE FROM Medico WHERE IdMedico = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }


}

