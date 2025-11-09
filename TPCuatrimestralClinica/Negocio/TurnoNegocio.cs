using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
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
                string query = "SELECT T.IdTurno ,T.NumeroTurno, T.FechaInicio, T.HoraInicio, CONCAT(p.Nombre, ' ', p.Apellido) AS PacienteNombreCompleto, CONCAT(M.Nombre, ' ', M.Apellido) AS MedicoNombreCompleto E.Nombre AS EspecialidadNombre,  " +
                               "FROM Turnos T " +
                               "JOIN Pacientes P ON T.IdPaciente = P.IdPaciente " +
                               "JOIN Medicos M ON T.IdMedico = M.IdMedico " +
                               "JOIN Especialidades E ON T.IdEspecialidad = E.IdEspecialidad ";
                // Agregar filtro si se proporciona

                datos.SetearConsulta(query);
                datos.EjecutarLectura();

                DataTable dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar pacientes: " + ex.Message);
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
            EspeciladadNegocio especialidadNegocio = new EspeciladadNegocio();
            try
            {
                datos.SetearConsulta("SELECT T.* FROM Turnos as T WHERE DNI = @Dni");
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
                    turno.Paciente = pacNegocio.BuscarPorId((int)datos.Lector["IdPaciente"]);
                    //turno.Medico = medicoNegocio.BuscarPorId((int)datos.Lector["IdMedico"]);
                    turno.Especialidad = especialidadNegocio.ObtenerEspecialidad((int)datos.Lector["IdEspecialidad"]);
                    turno.Motivo = (string)datos.Lector["Motivo"];
                    turno.Estado = (bool)datos.Lector["Estado"];
                    return turno;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al buscar Turno " + ex.Message);
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
            EspeciladadNegocio especialidadNegocio = new EspeciladadNegocio();
            try
            {
                datos.SetearConsulta("SELECT T.* FROM Turnos as T WHERE ID = @ID");
                datos.SetearParametros("@Dni", id);
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
                    turno.Paciente = pacNegocio.BuscarPorId((int)datos.Lector["IdPaciente"]);
                    //turno.Medico = medicoNegocio.BuscarPorId((int)datos.Lector["IdMedico"]);
                    turno.Especialidad = especialidadNegocio.ObtenerEspecialidad((int)datos.Lector["IdEspecialidad"]);
                    turno.Motivo = (string)datos.Lector["Motivo"];
                    turno.Estado = (bool)datos.Lector["Estado"];
                    return turno;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al buscar Turno " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Turno nuevoTurno)
        {
            //HITO 3 IMPLEMENTACION PRINCIPAL 
            throw new NotImplementedException();
        }

        public void ModificarFecha(Turno turnoModificado)
        {
            //HITO 3 IMPLEMENTACION PRINCIPAL 
            throw new NotImplementedException();
        }

        public void Cancelar(int id)
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
                throw new Exception("Error al cancelar el turno: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }



    }
}
