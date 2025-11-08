using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TurnoNegocio
    {

        public DataTable Listar(string filtro = "")
        {
            var datos = new AccesoDatos();
            try
            {
                var sql = @"
SELECT
    t.IdTurno,
    t.NumeroTurno,
    t.FechaInicio,                 -- date
    t.FechaFin,                    -- date
    t.HoraInicio,                  -- time(7) -> TimeSpan en .NET (ok con {0:hh\:mm})
    t.HoraFin,                     -- time(7)
    t.ObservacionesSolicitud,
    t.ObservacionesDiagnostico,
    t.Motivo,
    t.Estado,
    t.IdPaciente,
    t.IdMedico,
    t.IdEspecialidad,
    (p.Apellido + ', ' + p.Nombre) AS PacienteNombre,
    (m.Apellido + ', ' + m.Nombre) AS MedicoNombre,
    e.Nombre                       AS EspecialidadNombre
FROM Turnos t
INNER JOIN Pacientes p      ON p.IdPaciente      = t.IdPaciente
INNER JOIN Medicos   m      ON m.IdMedico        = t.IdMedico
INNER JOIN Especialidades e ON e.IdEspecialidad  = t.IdEspecialidad
";
                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    sql += @"
WHERE
      CONVERT(varchar(20), t.NumeroTurno)      LIKE @Filtro
   OR t.ObservacionesSolicitud                 LIKE @Filtro
   OR t.ObservacionesDiagnostico               LIKE @Filtro
   OR t.Motivo                                 LIKE @Filtro
   OR p.Nombre                                 LIKE @Filtro
   OR p.Apellido                               LIKE @Filtro
   OR m.Nombre                                 LIKE @Filtro
   OR m.Apellido                               LIKE @Filtro
   OR e.Nombre                                 LIKE @Filtro
";
                }

                datos.SetearConsulta(sql);
                if (!string.IsNullOrWhiteSpace(filtro))
                    datos.SetearParametros("@Filtro", "%" + filtro + "%");

                datos.EjecutarLectura();
                var dt = new DataTable();
                dt.Load(datos.Lector);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar turnos: " + ex.Message, ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Turno BuscarPorDni(string DNI)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT T.* FROM Turnos T JOIN Pacientes P ON T.IdPaciente = P.IdPaciente WHERE P.Dni = @Dni");
                datos.SetearParametros("@Dni", DNI);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.IdTurno = (int)datos.Lector["IdTurno"];
                    turno.NumeroTurno = (int)datos.Lector["NumeroTurno"];
                    turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                    turno.HoraInicio = (DateTime)datos.Lector["HoraInicio"];
                    turno.HoraFin = (DateTime)datos.Lector["HoraFin"];
                    turno.ObservacionesSolicitud = (string)datos.Lector["ObservacionesSolicitud"];
                    turno.ObservacionesDiagnostico = (string)datos.Lector["ObservacionesDiagnostico"];
                    // Aquí se deberían cargar también las propiedades Medico, Paciente y Especialidad
                    turno.Estado = (bool)datos.Lector["Estado"];
                    return turno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar turno por DNI: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Turno BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM Turnos WHERE IdTurno = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.IdTurno = (int)datos.Lector["IdTurno"];
                    turno.NumeroTurno = (int)datos.Lector["NumeroTurno"];
                    turno.FechaInicio = (DateTime)datos.Lector["FechaInicio"];
                    turno.FechaFin = (DateTime)datos.Lector["FechaFin"];
                    turno.HoraInicio = (DateTime)datos.Lector["HoraInicio"];
                    turno.HoraFin = (DateTime)datos.Lector["HoraFin"];
                    turno.ObservacionesSolicitud = (string)datos.Lector["ObservacionesSolicitud"];
                    turno.ObservacionesDiagnostico = (string)datos.Lector["ObservacionesDiagnostico"];
                    // Aquí se deberían cargar también las propiedades Medico, Paciente y Especialidad
                    turno.Estado = (bool)datos.Lector["Estado"];
                    return turno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar turno por ID: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        /// <summary>
        /// Logica Principal de negocio para guardar un nuevo Turno
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void GuardarNuevo(Turno nuevo)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Turno turno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"UPDATE Turnos SET 
                                        NumeroTurno = @NumeroTurno,
                                        FechaInicio = @FechaInicio,
                                        FechaFin = @FechaFin,
                                        HoraInicio = @HoraInicio,
                                        HoraFin = @HoraFin,
                                        ObservacionesSolicitud = @ObservacionesSolicitud,
                                        ObservacionesDiagnostico = @ObservacionesDiagnostico,
                                        Estado = @Estado
                                      WHERE IdTurno = @IdTurno");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar turno: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        //Modificar nombre a Cancelar Turno cuando funcione el aspx
        public void Eliminar(int id)
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
                throw new Exception("Error al cancelar turno: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

    }
}
