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
            var dict = new Dictionary<int, Medico>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearConsulta(@"
            SELECT 
                m.IdMedico,
                m.Nombre,
                m.Apellido,
                m.Matricula,
                t.IdGuardia AS IdTurnoTrabajo,
                t.Nombre    AS TurnoNombre,
                t.HoraInicio,
                t.HoraFin,
                mt.DiaSemana,               
                e.IdEspecialidad,
                e.Nombre    AS EspecialidadNombre
            FROM Medicos m
            LEFT JOIN MedicosPorGuardia mt        ON mt.IdMedico   = m.IdMedico
            LEFT JOIN Guardias         t          ON t.IdGuardia   = mt.IdGuardia
            LEFT JOIN MedicosPorEspecialidad me   ON me.IdMedico   = m.IdMedico
            LEFT JOIN Especialidades   e          ON e.IdEspecialidad = me.IdEspecialidad


                WHERE m.Activo = 1
            ORDER BY m.Apellido, m.Nombre, e.Nombre");




                accesoDatos.EjecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    int idMedico = (int)accesoDatos.Lector["IdMedico"];


                    if (!dict.TryGetValue(idMedico, out Medico aux))
                    {
                        aux = new Medico();
                        aux.IdMedico = idMedico;
                        aux.Nombre = (string)accesoDatos.Lector["Nombre"];
                        aux.Apellido = (string)accesoDatos.Lector["Apellido"];
                        aux.Matricula = (string)accesoDatos.Lector["Matricula"];

                        aux.turnoTrabajos = new List<MedicoPorGuardia>();
                        aux.Especialidades = new List<Especialidad>();
                        aux.TurnoTrabajo = null;

                        dict.Add(idMedico, aux);
                    }


                    if (accesoDatos.Lector["IdTurnoTrabajo"] != DBNull.Value)
                    {
                        int idTurno = (int)accesoDatos.Lector["IdTurnoTrabajo"];
                        string diaSemana = (string)accesoDatos.Lector["DiaSemana"];

                        bool yaExisteTurno = aux.turnoTrabajos.Any(t =>
                            t.IdTurnoTrabajo == idTurno && t.DiaSemana == diaSemana);

                        if (!yaExisteTurno)
                        {
                            var turno = new MedicoPorGuardia
                            {
                                IdTurnoTrabajo = idTurno,
                                Nombre = accesoDatos.Lector["TurnoNombre"].ToString(),
                                HoraInicio = (TimeSpan)accesoDatos.Lector["HoraInicio"],
                                HoraFin = (TimeSpan)accesoDatos.Lector["HoraFin"],
                                DiaSemana = diaSemana
                            };

                            aux.turnoTrabajos.Add(turno);


                            if (aux.TurnoTrabajo == null)
                                aux.TurnoTrabajo = turno;
                        }
                    }


                    if (accesoDatos.Lector["IdEspecialidad"] != DBNull.Value)
                    {
                        int idEsp = (int)accesoDatos.Lector["IdEspecialidad"];

                        bool yaExisteEsp = aux.Especialidades.Any(e =>
                            e.IdEspecialidad == idEsp);

                        if (!yaExisteEsp)
                        {
                            Especialidad esp = new Especialidad
                            {
                                IdEspecialidad = idEsp,
                                Nombre = (string)accesoDatos.Lector["EspecialidadNombre"]
                            };

                            aux.Especialidades.Add(esp);
                        }
                    }
                }

                return dict.Values.ToList();
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

        public int AgregarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            int idMedicoNuevo = 0;

            try
            {

                datos.SetearConsulta(
                    "INSERT INTO Medicos (Nombre, Apellido, Matricula) " +
                    "VALUES (@Nombre, @Apellido, @Matricula); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT);"
                );
                datos.SetearParametros("@Nombre", medico.Nombre);
                datos.SetearParametros("@Apellido", medico.Apellido);
                datos.SetearParametros("@Matricula", medico.Matricula);

                idMedicoNuevo = Convert.ToInt32(datos.EjecutarEscalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }


            if (medico.turnoTrabajos != null && medico.turnoTrabajos.Count > 0)
            {
                foreach (var turno in medico.turnoTrabajos)
                {
                    AccesoDatos datosGuardia = new AccesoDatos();
                    try
                    {
                        datosGuardia.SetearConsulta(@"
                    IF NOT EXISTS (
                        SELECT 1 
                        FROM MedicosPorGuardia
                        WHERE IdMedico = @IdMedico
                          AND IdGuardia = @IdGuardia
                          AND DiaSemana = @DiaSemana
                    )
                    BEGIN
                        INSERT INTO MedicosPorGuardia (IdMedico, IdGuardia, DiaSemana)
                        VALUES (@IdMedico, @IdGuardia, @DiaSemana)
                    END
                ");

                        datosGuardia.SetearParametros("@IdMedico", idMedicoNuevo);
                        datosGuardia.SetearParametros("@IdGuardia", turno.IdTurnoTrabajo);
                        datosGuardia.SetearParametros("@DiaSemana", turno.DiaSemana);

                        datosGuardia.EjecutarAccion();
                    }
                    finally
                    {
                        datosGuardia.CerrarConexion();
                    }
                }
            }


            if (medico.Especialidades != null && medico.Especialidades.Count > 0)
            {
                foreach (var esp in medico.Especialidades)
                {
                    AccesoDatos datosEsp = new AccesoDatos();
                    try
                    {
                        datosEsp.SetearConsulta(@"
                    IF NOT EXISTS (
                        SELECT 1 
                        FROM MedicosPorEspecialidad 
                        WHERE IdMedico = @IdMedico AND IdEspecialidad = @IdEspecialidad
                    )
                    BEGIN
                        INSERT INTO MedicosPorEspecialidad (IdMedico, IdEspecialidad)
                        VALUES (@IdMedico, @IdEspecialidad)
                    END
                ");

                        datosEsp.SetearParametros("@IdMedico", idMedicoNuevo);
                        datosEsp.SetearParametros("@IdEspecialidad", esp.IdEspecialidad);
                        datosEsp.EjecutarAccion();
                    }
                    finally
                    {
                        datosEsp.CerrarConexion();
                    }
                }
            }

            return idMedicoNuevo;
        }

        public void ModificarMedico(Medico medico, List<int> idsEspecialidades)
        {
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.SetearConsulta(@"
            UPDATE Medicos
               SET Nombre   = @Nombre,
                   Apellido = @Apellido,
                   Matricula = @Matricula
             WHERE IdMedico = @IdMedico");

                datosMedico.SetearParametros("@IdMedico", medico.IdMedico);
                datosMedico.SetearParametros("@Nombre", medico.Nombre);
                datosMedico.SetearParametros("@Apellido", medico.Apellido);
                datosMedico.SetearParametros("@Matricula", medico.Matricula);
                datosMedico.EjecutarAccion();
            }
            finally
            {
                datosMedico.CerrarConexion();
            }


            AccesoDatos datosGuardia = new AccesoDatos();
            try
            {
                datosGuardia.SetearConsulta("DELETE FROM MedicosPorGuardia WHERE IdMedico = @IdMedico");
                datosGuardia.SetearParametros("@IdMedico", medico.IdMedico);
                datosGuardia.EjecutarAccion();
            }
            finally
            {
                datosGuardia.CerrarConexion();
            }


            if (medico.turnoTrabajos != null && medico.turnoTrabajos.Count > 0)
            {
                foreach (var turno in medico.turnoTrabajos)
                {
                    AccesoDatos datosInsGuardia = new AccesoDatos();
                    try
                    {
                        datosInsGuardia.SetearConsulta(@"
                    INSERT INTO MedicosPorGuardia (IdMedico, IdGuardia, DiaSemana)
                    VALUES (@IdMedico, @IdGuardia, @DiaSemana)");
                        datosInsGuardia.SetearParametros("@IdMedico", medico.IdMedico);
                        datosInsGuardia.SetearParametros("@IdGuardia", turno.IdTurnoTrabajo);
                        datosInsGuardia.SetearParametros("@DiaSemana", turno.DiaSemana);
                        datosInsGuardia.EjecutarAccion();
                    }
                    finally
                    {
                        datosInsGuardia.CerrarConexion();
                    }
                }
            }


            AccesoDatos datosEsp = new AccesoDatos();
            try
            {
                datosEsp.SetearConsulta("DELETE FROM MedicosPorEspecialidad WHERE IdMedico = @IdMedico");
                datosEsp.SetearParametros("@IdMedico", medico.IdMedico);
                datosEsp.EjecutarAccion();
            }
            finally
            {
                datosEsp.CerrarConexion();
            }


            foreach (int idEsp in idsEspecialidades)
            {
                AccesoDatos datosIns = new AccesoDatos();
                try
                {
                    datosIns.SetearConsulta(@"
                INSERT INTO MedicosPorEspecialidad (IdMedico, IdEspecialidad)
                VALUES (@IdMedico, @IdEspecialidad)");
                    datosIns.SetearParametros("@IdMedico", medico.IdMedico);
                    datosIns.SetearParametros("@IdEspecialidad", idEsp);
                    datosIns.EjecutarAccion();
                }
                finally
                {
                    datosIns.CerrarConexion();
                }
            }
        }

        public void EliminarMedico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Medicos SET Activo = 0 WHERE IdMedico = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Medico BuscarMedicoPorId(int idMedico, out List<int> idsEspecialidades)
        {
            Medico medico = null;
            idsEspecialidades = new List<int>();

          ///m
            AccesoDatos datosMedico = new AccesoDatos();
            try
            {
                datosMedico.SetearConsulta(@"
            SELECT M.IdMedico,
                   M.Nombre,
                   M.Apellido,
                   M.Matricula,
                   MG.IdGuardia,
                   MG.DiaSemana,
                   G.Nombre     AS NombreGuardia,
                   G.HoraInicio,
                   G.HoraFin
            FROM Medicos M
            LEFT JOIN MedicosPorGuardia MG ON MG.IdMedico = M.IdMedico
            LEFT JOIN Guardias G          ON G.IdGuardia = MG.IdGuardia
            WHERE M.IdMedico = @IdMedico");

                datosMedico.SetearParametros("@IdMedico", idMedico);
                datosMedico.EjecutarLectura();

                while (datosMedico.Lector.Read())
                {
                    if (medico == null)
                    {
                        medico = new Medico();
                        medico.IdMedico = (int)datosMedico.Lector["IdMedico"];
                        medico.Nombre = datosMedico.Lector["Nombre"].ToString();
                        medico.Apellido = datosMedico.Lector["Apellido"].ToString();
                        medico.Matricula = datosMedico.Lector["Matricula"].ToString();

                        medico.turnoTrabajos = new List<MedicoPorGuardia>();
                        medico.Especialidades = new List<Especialidad>();
                        medico.TurnoTrabajo = null; 
                    }

                  
                    if (!(datosMedico.Lector["IdGuardia"] is DBNull))
                    {
                        MedicoPorGuardia turno = new MedicoPorGuardia();
                        turno.IdTurnoTrabajo = (int)datosMedico.Lector["IdGuardia"];
                        turno.Nombre = datosMedico.Lector["NombreGuardia"].ToString();
                        turno.DiaSemana = (string)datosMedico.Lector["DiaSemana"];

                        if (!(datosMedico.Lector["HoraInicio"] is DBNull))
                            turno.HoraInicio = (TimeSpan)datosMedico.Lector["HoraInicio"];
                        if (!(datosMedico.Lector["HoraFin"] is DBNull))
                            turno.HoraFin = (TimeSpan)datosMedico.Lector["HoraFin"];

                       
                        bool existe = medico.turnoTrabajos.Any(t =>
                            t.IdTurnoTrabajo == turno.IdTurnoTrabajo &&
                            t.DiaSemana == turno.DiaSemana);

                        if (!existe)
                        {
                            medico.turnoTrabajos.Add(turno);

                          
                            if (medico.TurnoTrabajo == null)
                                medico.TurnoTrabajo = turno;
                        }
                    }
                }
            }
            finally
            {
                datosMedico.CerrarConexion();
            }

            
            if (medico != null)
            {
                AccesoDatos datosEsp = new AccesoDatos();
                try
                {
                    datosEsp.SetearConsulta(@"
                SELECT E.IdEspecialidad, E.Nombre
                FROM MedicosPorEspecialidad ME
                INNER JOIN Especialidades E ON E.IdEspecialidad = ME.IdEspecialidad
                WHERE ME.IdMedico = @IdMedico");

                    datosEsp.SetearParametros("@IdMedico", idMedico);
                    datosEsp.EjecutarLectura();

                    while (datosEsp.Lector.Read())
                    {
                        int idEsp = (int)datosEsp.Lector["IdEspecialidad"];
                        string nombreEsp = datosEsp.Lector["Nombre"].ToString();

                        idsEspecialidades.Add(idEsp); 

                        Especialidad esp = new Especialidad
                        {
                            IdEspecialidad = idEsp,
                            Nombre = nombreEsp
                        };

                        medico.Especialidades.Add(esp);
                    }
                }
                finally
                {
                    datosEsp.CerrarConexion();
                }
            }

            return medico;


        }

        public Medico BuscarMedicoPorIdSimple(int idMedico)
        {
            // Llama a tu método existente, pero descarta la lista de IDs
            List<int> idsDescartados;
            return BuscarMedicoPorId(idMedico, out idsDescartados);
        }
    }
}

