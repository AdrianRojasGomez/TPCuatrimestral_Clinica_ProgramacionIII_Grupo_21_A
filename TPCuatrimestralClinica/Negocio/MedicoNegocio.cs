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
                t.IdGuardia AS IdTurnoTrabajo,
                t.Nombre    AS TurnoNombre,
                t.HoraInicio,
                t.HoraFin,
                 t.DiaSemana,
                e.IdEspecialidad,
                e.Nombre    AS EspecialidadNombre
            FROM Medicos m
            LEFT JOIN MedicosPorGuardia mt        ON mt.IdMedico   = m.IdMedico
            LEFT JOIN Guardias         t          ON t.IdGuardia   = mt.IdGuardia
            LEFT JOIN MedicosPorEspecialidad me   ON me.IdMedico   = m.IdMedico
            LEFT JOIN Especialidades   e          ON e.IdEspecialidad = me.IdEspecialidad

                WHERE m.Estado = 1
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
                        aux.TurnoTrabajo.DiaSemana = (string)accesoDatos.Lector["DiaSemana"];
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

        public int AgregarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();

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

                int idMedicoNuevo = Convert.ToInt32(datos.EjecutarEscalar());



                datos.CerrarConexion();


                if (medico.TurnoTrabajo != null)
                {
                    datos = new AccesoDatos();
                    datos.SetearConsulta(
                        "INSERT INTO MedicosPorGuardia (IdMedico, IdGuardia) " +
                        "VALUES (@IdMedico, @IdGuardia)"
                    );
                    datos.SetearParametros("@IdMedico", idMedicoNuevo);
                    datos.SetearParametros("@IdGuardia", medico.TurnoTrabajo.IdTurnoTrabajo);
                    datos.EjecutarAccion();
                    datos.CerrarConexion();
                }

                if (medico.Especialidades != null && medico.Especialidades.Count > 0)
                {
                    foreach (var esp in medico.Especialidades)
                    {
                        datos = new AccesoDatos();
                        datos.SetearConsulta(
                            "INSERT INTO MedicosPorEspecialidad (IdMedico, IdEspecialidad) " +
                            "VALUES (@IdMedico, @IdEspecialidad)"
                        );
                        datos.SetearParametros("@IdMedico", idMedicoNuevo);
                        datos.SetearParametros("@IdEspecialidad", esp.IdEspecialidad);
                        datos.EjecutarAccion();
                        datos.CerrarConexion();
                    }
                }
                return idMedicoNuevo;
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


            if (medico.TurnoTrabajo != null)
            {
                AccesoDatos datosGuardia = new AccesoDatos();
                try
                {
                    datosGuardia.SetearConsulta(@"
                DELETE FROM MedicosPorGuardia 
                 WHERE IdMedico = @IdMedico;

                INSERT INTO MedicosPorGuardia (IdMedico, IdGuardia)
                VALUES (@IdMedico, @IdGuardia);");

                    datosGuardia.SetearParametros("@IdMedico", medico.IdMedico);

                    datosGuardia.SetearParametros("@IdGuardia", medico.TurnoTrabajo.IdTurnoTrabajo);
                    datosGuardia.EjecutarAccion();
                }
                finally
                {
                    datosGuardia.CerrarConexion();
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
                datos.SetearConsulta("UPDATE Medicos SET Estado = 0 WHERE IdMedico = @Id");
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
                   G.IdGuardia,
                   G.DiaSemana,
                   G.Nombre AS NombreGuardia
            FROM Medicos M
            LEFT JOIN MedicosPorGuardia MG ON MG.IdMedico = M.IdMedico
            LEFT JOIN Guardia G ON G.IdGuardia = MG.IdGuardia
            WHERE M.IdMedico = @IdMedico");

                datosMedico.SetearParametros("@IdMedico", idMedico);
                datosMedico.EjecutarLectura();

                if (datosMedico.Lector.Read())
                {
                    medico = new Medico();
                    medico.IdMedico = (int)datosMedico.Lector["IdMedico"];
                    medico.Nombre = datosMedico.Lector["Nombre"].ToString();
                    medico.Apellido = datosMedico.Lector["Apellido"].ToString();
                    medico.Matricula = datosMedico.Lector["Matricula"].ToString();


                    if (!(datosMedico.Lector["IdGuardia"] is DBNull))
                    {
                        medico.TurnoTrabajo = new TurnoTrabajo();
                        medico.TurnoTrabajo.IdTurnoTrabajo = (int)datosMedico.Lector["IdGuardia"];
                        medico.TurnoTrabajo.Nombre = datosMedico.Lector["NombreGuardia"].ToString();
                        medico.TurnoTrabajo.DiaSemana = (string)datosMedico.Lector["DiaSemana"];
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
                INNER JOIN Especialidad E ON E.IdEspecialidad = ME.IdEspecialidad
                WHERE ME.IdMedico = @IdMedico");

                    datosEsp.SetearParametros("@IdMedico", idMedico);
                    datosEsp.EjecutarLectura();

                    while (datosEsp.Lector.Read())
                    {
                        Especialidad esp = new Especialidad();
                        esp.IdEspecialidad = (int)datosEsp.Lector["IdEspecialidad"];
                        esp.Nombre = datosEsp.Lector["Nombre"].ToString();

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
    }


}

