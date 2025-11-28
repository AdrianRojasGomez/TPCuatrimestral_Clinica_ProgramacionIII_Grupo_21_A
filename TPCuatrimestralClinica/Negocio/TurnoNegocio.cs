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

        public DataTable ListarTurnosDashboard()
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
            WHERE 
                T.FechaInicio >= CAST(GETDATE() AS DATE) 
                AND T.Estado != 2  -- No mostrar turnos cancelados (Estado = 2)
            ORDER BY
                T.FechaInicio, T.HoraInicio";

                datos.SetearConsulta(query);
                datos.EjecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar turnos del dashboard: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Turno> ObtenerTodosLosTurnos()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            PacienteNegocio pacNegocio = new PacienteNegocio();
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            try
            {
                datos.SetearConsulta("SELECT T.* FROM Turnos as T ORDER BY T.FechaInicio DESC");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.IdTurno = (int)datos.Lector["IdTurno"];
                    turno.NumeroTurno = (string)datos.Lector["NumeroTurno"];
                    turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                    turno.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                    turno.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                    turno.ObservacionesSolicitud = datos.Lector["ObservacionesSolicitud"] as string;
                    turno.ObservacionesDiagnostico = datos.Lector["ObservacionesDiagnostico"] as string;
                    turno.IdPaciente = (int)datos.Lector["IdPaciente"];
                    turno.IdMedico = (int)datos.Lector["IdMedico"];
                    turno.Paciente = pacNegocio.BuscarPorId(turno.IdPaciente);
                    turno.Medico = medicoNegocio.BuscarMedicoPorIdSimple(turno.IdMedico); 
                    turno.Motivo = datos.Lector["Motivo"] as string;
                    turno.Estado = (int)datos.Lector["Estado"];
                    lista.Add(turno);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los turnos: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public int ObtenerUltimoID()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT MAX(IdTurno) AS UltimoID FROM Turnos");
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return datos.Lector["UltimoID"] != DBNull.Value ? (int)datos.Lector["UltimoID"] : 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último ID de Turno: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void AgregarTurno(Turno nuevoTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"
                INSERT INTO Turnos 
                (NumeroTurno, FechaInicio, FechaFin, HoraInicio, HoraFin, ObservacionesSolicitud, ObservacionesDiagnostico, IdMedico, IdPaciente, IdEspecialidad, Motivo, Estado) 
                VALUES 
                (@NumeroTurno, @FechaInicio, @FechaFin, @HoraInicio, @HoraFin, @ObservacionesSolicitud, @ObservacionesDiagnostico, @IdMedico, @IdPaciente, @IdEspecialidad, @Motivo, @Estado)
                ");
                datos.SetearParametros("@NumeroTurno", nuevoTurno.NumeroTurno);
                datos.SetearParametros("@FechaInicio", nuevoTurno.FechaInicio);
                datos.SetearParametros("@FechaFin", nuevoTurno.FechaFin);
                datos.SetearParametros("@HoraInicio", nuevoTurno.HoraInicio);
                datos.SetearParametros("@HoraFin", nuevoTurno.HoraFin);
                datos.SetearParametros("@ObservacionesSolicitud", nuevoTurno.ObservacionesSolicitud);
                datos.SetearParametros("@ObservacionesDiagnostico", nuevoTurno.ObservacionesDiagnostico);
                datos.SetearParametros("@IdMedico", nuevoTurno.IdMedico);
                datos.SetearParametros("@IdPaciente", nuevoTurno.IdPaciente);
                datos.SetearParametros("@IdEspecialidad", nuevoTurno.IdEspecialidad);
                datos.SetearParametros("@Motivo", nuevoTurno.Motivo);
                datos.SetearParametros("@Estado", nuevoTurno.Estado);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar nuevo Turno: " + ex.Message);
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
                        turno.NumeroTurno = datos.Lector["NumeroTurno"] as string;
                        turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                        turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                        turno.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                        turno.ObservacionesSolicitud = datos.Lector["ObservacionesSolicitud"] as string;
                        turno.ObservacionesDiagnostico = datos.Lector["ObservacionesDiagnostico"] as string;
                                            
                        turno.IdPaciente = (int)datos.Lector["IdPaciente"];
                        turno.IdMedico = (int)datos.Lector["IdMedico"];

                        turno.Paciente = pacNegocio.BuscarPorId(turno.IdPaciente);
                        turno.Medico = medicoNegocio.BuscarMedicoPorIdSimple(turno.IdMedico); 

                        
                        turno.Motivo = datos.Lector["Motivo"] as string;
                        turno.Estado = (int)datos.Lector["Estado"];
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
                        turno.NumeroTurno = datos.Lector["NumeroTurno"] as string;
                        turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                        turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                        turno.HoraInicio = (TimeSpan)datos.Lector["HoraInicio"];
                        turno.HoraFin = (TimeSpan)datos.Lector["HoraFin"];
                        turno.ObservacionesSolicitud = datos.Lector["ObservacionesSolicitud"] as string;
                        turno.ObservacionesDiagnostico = datos.Lector["ObservacionesDiagnostico"] as string;

                        turno.IdPaciente = (int)datos.Lector["IdPaciente"];
                        turno.IdMedico = (int)datos.Lector["IdMedico"];

                        turno.Paciente = pacNegocio.BuscarPorId(turno.IdPaciente);
                        turno.Medico = medicoNegocio.BuscarMedicoPorIdSimple(turno.IdMedico); 

                        turno.Motivo = datos.Lector["Motivo"] as string;
                        turno.Estado = (int)datos.Lector["Estado"];
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

        public void ModificarEstado(int id, int nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Turnos SET Estado = @Estado WHERE IdTurno = @IdTurno");
                datos.SetearParametros("@Estado", nuevoEstado);
                datos.SetearParametros("@IdTurno", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el estado del turno: " + ex.Message);
            }

        }

        public void ModificarDiagnostico(int id, string diagnostico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Turnos SET ObservacionesDiagnostico = @Diagnostico WHERE IdTurno = @IdTurno");
                datos.SetearParametros("@Diagnostico", diagnostico);
                datos.SetearParametros("@IdTurno", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el diagnóstico del turno: " + ex.Message);
            }
        }
    }
}