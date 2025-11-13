using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; 
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TurnoNegocio
    {
        public DataTable Listar(string filtro = "")
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = @"
                SELECT 
                    T.IdTurno, 
                    T.FechaInicio, 
                    T.HoraInicio,
                    P.Dni AS PacienteDNI,
                    CONCAT(P.Nombre, ' ', P.Apellido) AS PacienteNombre, 
                    CONCAT(M.Nombre, ' ', M.Apellido) AS MedicoNombre, 
                    E.Nombre AS EspecialidadNombre,
                    T.Motivo,
                    T.Estado
                FROM Turnos T
                LEFT JOIN Pacientes P ON T.IdPaciente = P.IdPaciente
                LEFT JOIN Medicos M ON T.IdMedico = M.IdMedico
                LEFT JOIN Especialidades E ON T.IdEspecialidad = E.IdEspecialidad
            ";


                if (!string.IsNullOrEmpty(filtro))
                {
                    string filtroLike = "'%" + filtro + "%'";
                    query += " WHERE P.Dni LIKE " + filtroLike +
                             " OR CONCAT(P.Nombre, ' ', P.Apellido) LIKE " + filtroLike +
                             " OR CONCAT(M.Nombre, ' ', M.Apellido) LIKE " + filtroLike;
                }

                datos.SetearConsulta(query);
                datos.EjecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar turnos: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public Turno BuscarPorDNI(string dni)
            {
                AccesoDatos datos = new AccesoDatos();
                PacienteNegocio pacNegocio = new PacienteNegocio();
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                

                try
                {
                    
                    datos.SetearConsulta(@"
                    SELECT T.* FROM Turnos AS T
                    INNER JOIN Pacientes AS P ON T.IdPaciente = P.IdPaciente
                    WHERE P.Dni = @Dni
                ");
                    datos.SetearParametros("@Dni", dni);
                    datos.EjecutarLectura();

                    if (datos.Lector.Read())
                    {
                        Turno turno = new Turno();
                        turno.IdTurno = (int)datos.Lector["IdTurno"];
                        turno.NumeroTurno = (string)datos.Lector["NumeroTurno"];
                        turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                        turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                        turno.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                        turno.ObservacionesSolicitud = (string)datos.Lector["ObservacionesSolicitud"];
                        turno.ObservacionesDiagnostico = (string)datos.Lector["ObservacionesDiagnostico"];
                                            
                        turno.IdPaciente = (int)datos.Lector["IdPaciente"];
                        turno.IdMedico = (int)datos.Lector["IdMedico"];

                        turno.Paciente = pacNegocio.BuscarPorId(turno.IdPaciente);
                        turno.Medico = medicoNegocio.BuscarMedicoPorIdSimple(turno.IdMedico); 

                        
                        turno.Motivo = (string)datos.Lector["Motivo"];
                        turno.Estado = (bool)datos.Lector["Estado"];
                        return turno;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar Turno por DNI: " + ex.Message);
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }

        public Turno BuscarPorId(int id)
            {
                AccesoDatos datos = new AccesoDatos();
                PacienteNegocio pacNegocio = new PacienteNegocio();
                MedicoNegocio medicoNegocio = new MedicoNegocio();
              
                try
                {
                    
                    datos.SetearConsulta("SELECT T.* FROM Turnos as T WHERE IdTurno = @IdTurno");
                    datos.SetearParametros("@IdTurno", id);
                    datos.EjecutarLectura();

                    if (datos.Lector.Read())
                    {
                        Turno turno = new Turno();
                        turno.IdTurno = (int)datos.Lector["IdTurno"];
                        turno.NumeroTurno = (string)datos.Lector["NumeroTurno"];
                        turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                        turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                        turno.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                        turno.ObservacionesSolicitud = (string)datos.Lector["ObservacionesSolicitud"];
                        turno.ObservacionesDiagnostico = (string)datos.Lector["ObservacionesDiagnostico"];

                        turno.IdPaciente = (int)datos.Lector["IdPaciente"];
                        turno.IdMedico = (int)datos.Lector["IdMedico"];

                        turno.Paciente = pacNegocio.BuscarPorId(turno.IdPaciente);
                        turno.Medico = medicoNegocio.BuscarMedicoPorIdSimple(turno.IdMedico); 

                        turno.Motivo = (string)datos.Lector["Motivo"];
                        turno.Estado = (bool)datos.Lector["Estado"];
                        return turno;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar Turno por ID: " + ex.Message);
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
             
        public void ModificarFecha(Turno turnoModificado)
        {
            
            throw new NotImplementedException();
        }

        public void Cancelar(int id)
        {
           AccesoDatos datos = new AccesoDatos();
            try
            {                
                datos.SetearConsulta("UPDATE Turnos SET Estado = 2 WHERE IdTurno = @IdTurno"); 
                datos.SetearParametros("@IdTurno", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cancelar el turno: " + ex.Message);
            }
            
        }

        public void Reactivar(int id) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Turnos SET Estado = 0 WHERE IdTurno = @IdTurno");
                datos.SetearParametros("@IdTurno", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reactivar el turno: " + ex.Message);
            }
        }
    }
}