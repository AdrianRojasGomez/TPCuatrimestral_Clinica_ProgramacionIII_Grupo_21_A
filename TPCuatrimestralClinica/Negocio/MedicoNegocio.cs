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
        e.Nombre    AS EspecialidadNombre,

        -- ⚠️ NUEVO: info de usuario
        CASE 
            WHEN um.IdUsuario IS NULL THEN 0 
            ELSE 1 
        END AS TieneUsuario,

        CASE 
            WHEN um.IdUsuario IS NOT NULL AND u.Estado = 1 THEN 1
            ELSE 0
        END AS UsuarioActivo

    FROM Medicos m
    LEFT JOIN MedicosPorGuardia      mt ON mt.IdMedico      = m.IdMedico
    LEFT JOIN Guardias               t  ON t.IdGuardia      = mt.IdGuardia
    LEFT JOIN MedicosPorEspecialidad me ON me.IdMedico      = m.IdMedico
    LEFT JOIN Especialidades         e  ON e.IdEspecialidad = me.IdEspecialidad
    LEFT JOIN UsuariosAppxMedico     um ON um.IdMedico      = m.IdMedico
    LEFT JOIN UsuariosApp            u  ON u.IdUsuario      = um.IdUsuario
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
                        aux.TieneUsuario = Convert.ToBoolean(accesoDatos.Lector["TieneUsuario"]);
                        aux.UsuarioActivo = Convert.ToBoolean(accesoDatos.Lector["UsuarioActivo"]);

                        aux.turnoTrabajos = new List<MedicoPorGuardia>();
                        aux.Especialidades = new List<Especialidad>();
                        aux.TurnoTrabajo = null;

                        dict.Add(idMedico, aux);
                    }


                    if (accesoDatos.Lector["IdTurnoTrabajo"] != DBNull.Value)
                    {
                        int idTurno = (int)accesoDatos.Lector["IdTurnoTrabajo"];
                        byte diaNumero = (byte)accesoDatos.Lector["DiaSemana"];


                        string diaSemana = ConvertirDiaSemanaTexto(diaNumero);

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
                    "INSERT INTO Medicos (Nombre, Apellido, Matricula , Activo) " +
                    "VALUES (@Nombre, @Apellido, @Matricula,1); " +
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
                        int diaSemanaNumero = ConvertirDiaSemana(turno.DiaSemana);
                        datosGuardia.SetearParametros("@DiaSemana", diaSemanaNumero);

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
                        int diaNumero = ConvertirDiaSemana(turno.DiaSemana);
                        datosInsGuardia.SetearParametros("@DiaSemana", diaNumero);
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
                        byte diaNumero = (byte)datosMedico.Lector["DiaSemana"];
                        string diaSemana = ConvertirDiaSemanaTexto(diaNumero);
                        turno.DiaSemana = diaSemana;

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
            List<int> idsDescartados;
            return BuscarMedicoPorId(idMedico, out idsDescartados);
        }

        public int ConvertirDiaSemana(string dia)
        {
            switch (dia)
            {
                case "Lunes": return 1;
                case "Martes": return 2;
                case "Miércoles": return 3;
                case "Jueves": return 4;
                case "Viernes": return 5;
                case "Sábado": return 6;
                case "Domingo": return 7;
                default: return 1;
            }
        }

        public string ObtenerNombreDiaEnEspanol(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return string.Empty;
            }
        }

        public string ConvertirDiaSemanaTexto(byte dia)
        {
            switch (dia)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miércoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sábado";
                case 7: return "Domingo";
                default: return "Lunes";
            }
        }

        //Adri: Nuevo metodo para listar medicos por especialidad
        public List<Medico> ListarPorEspecialidad(int idEspecialidad)
        {
            List<Medico> medicos = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(@" SELECT IdMedico
                                        FROM MedicosPorEspecialidad
                                        WHERE IdEspecialidad = @IdEspecialidad;");
                datos.SetearParametros("@IdEspecialidad", idEspecialidad);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    int idMedico = (int)datos.Lector["IdMedico"];
                    Medico medico = BuscarMedicoPorIdSimple(idMedico);
                    if (medico != null)
                    {
                        medicos.Add(medico);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }


            return medicos;
        }

        //Adri: Nuevo metodo para obtener Horarios de un medico en una fecha en particular
        public List<TimeSpan> ObtenerHorariosDeTrabajoDelMedico(int idMedico, DateTime fecha)
        {
            List<TimeSpan> resultadoHorario = new List<TimeSpan>();

            Medico medico = BuscarMedicoPorIdSimple(idMedico);

            if (medico.turnoTrabajos == null || medico.turnoTrabajos.Count == 0)
                return resultadoHorario;

            MedicoPorGuardia guardiaDelDia = null;

            TimeSpan inicio = medico.TurnoTrabajo.HoraInicio;
            TimeSpan fin = medico.TurnoTrabajo.HoraFin;
            foreach (var guardia in medico.turnoTrabajos)
            {
                if (fin > inicio)
                {
                    var horaActual = inicio;
                    while (horaActual < fin)
                    {
                        resultadoHorario.Add(horaActual);
                        horaActual = horaActual.Add(TimeSpan.FromHours(1));
                    }
                }

                else
                {
                    //tengo que pensar que verga quiero hacer si lo quiero agregar al otro dia o que onda, hubiesemos hecho una 
                    // clinica de ortodoncia y esto no pasaba jajaja
                    var hora = inicio;
                    var finDia = TimeSpan.FromHours(24); // 24:00

                    while (hora < finDia)
                    {
                        resultadoHorario.Add(hora);
                        hora = hora.Add(TimeSpan.FromHours(1));
                    }
                }
            }

            return resultadoHorario;
        }
        //Adri: Nuevo metodo para obtener los Horarios Libres de un medico en una fecha en particular
        public List<TimeSpan> ObtenerHorariosLibres(int idMedico, DateTime fecha)
        { 
            TurnoNegocio turnoNegocio = new TurnoNegocio();
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            List<Turno> turnos = turnoNegocio.ObtenerTodosLosTurnos();
            Medico medico = medicoNegocio.BuscarMedicoPorIdSimple(idMedico);
            var horariosTrabajo = ObtenerHorariosDeTrabajoDelMedico(medico.IdMedico, fecha);
            var horariosLibres = new List<TimeSpan>();

            if(horariosTrabajo.Count == 0)
                return horariosLibres;

            var horariosOcupados = new List<TimeSpan>();

            foreach (var turno in turnos)
            {
                if (turno.IdMedico == medico.IdMedico && turno.FechaInicio.Date == fecha.Date && turno.Estado != 0)
                {
                    horariosOcupados.Add(turno.HoraInicio);
                }
            }

            foreach (var hora in horariosTrabajo)
            {
                bool estaOcupada = false;

                foreach (var horaOcupada in horariosOcupados)
                {
                    if (hora == horaOcupada)
                    {
                        estaOcupada = true;
                        break;
                    }
                }

                if (!estaOcupada)
                {
                    horariosLibres.Add(hora);
                }
            }

            return horariosLibres;
        }
        //Adri: Nuevo metodo para verificar si un medico tiene horarios libres en una fecha en particular
        public bool MedicoTieneHorariosLibres(int idMedico, DateTime fecha)
        {
            var horariosLibres = ObtenerHorariosLibres(idMedico, fecha);
            return horariosLibres.Count > 0;
        }

    }
}

