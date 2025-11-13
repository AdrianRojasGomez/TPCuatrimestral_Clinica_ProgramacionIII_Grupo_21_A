using Dominio;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class PacienteNegocio
    {
        public DataTable Listar(string filtro = "")
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = @"
            SELECT 
                IdPaciente, Dni, Apellido, Nombre, Email, Telefono, Estado
            FROM Pacientes";

                if (!string.IsNullOrEmpty(filtro))
                {
                    query += " WHERE (Estado = 1 AND (Dni LIKE @FiltroLike OR Apellido LIKE @FiltroLike OR Nombre LIKE @FiltroLike))" +
                             "    OR (Estado = 0 AND Dni = @FiltroExacto)";

                    datos.SetearParametros("@FiltroLike", "%" + filtro + "%"); 
                    datos.SetearParametros("@FiltroExacto", filtro); 
                }
                else
                {
                    query += " WHERE Estado = 1";
                }

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

        public Paciente BuscarPorDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.SetearConsulta("SELECT * FROM Pacientes WHERE Dni = @Dni");
                datos.SetearParametros("@Dni", dni);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente pac = new Paciente();
                    pac.IdPaciente = (int)datos.Lector["IdPaciente"];
                    pac.Nombre = (string)datos.Lector["Nombre"];
                    pac.Apellido = (string)datos.Lector["Apellido"];
                    pac.Dni = (string)datos.Lector["Dni"];
                    pac.Email = (string)datos.Lector["Email"];
                    pac.Telefono = (string)datos.Lector["Telefono"];
                    pac.Direccion = (string)datos.Lector["Direccion"];

                    if (datos.Lector["FechaNacimiento"] != DBNull.Value)
                    {
                        pac.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    }
                    return pac;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar paciente por DNI: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Paciente BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT * FROM Pacientes WHERE IdPaciente = @IdPaciente");
                datos.SetearParametros("@IdPaciente", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Paciente pac = new Paciente();
                    pac.IdPaciente = (int)datos.Lector["IdPaciente"];
                    pac.Nombre = (string)datos.Lector["Nombre"];
                    pac.Apellido = (string)datos.Lector["Apellido"];
                    pac.Dni = (string)datos.Lector["Dni"];
                    pac.Email = (string)datos.Lector["Email"];
                    pac.Telefono = (string)datos.Lector["Telefono"];
                    pac.Direccion = (string)datos.Lector["Direccion"];

                    if (datos.Lector["FechaNacimiento"] != DBNull.Value)
                    {
                        pac.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    }
                    return pac;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar paciente: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void GuardarNuevo(Paciente nuevo)
        {
            CheckDni(nuevo.Dni, 0); 

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"INSERT INTO Pacientes (Dni, Apellido, Nombre, FechaNacimiento, Email, Telefono, Direccion, Estado)
                                       VALUES (@Dni, @Apellido, @Nombre, @FechaNacimiento, @Email, @Telefono, @Direccion, @Estado)");

                datos.SetearParametros("@Dni", nuevo.Dni);
                datos.SetearParametros("@Apellido", nuevo.Apellido);
                datos.SetearParametros("@Nombre", nuevo.Nombre);
                datos.SetearParametros("@FechaNacimiento", (object)nuevo.FechaNacimiento ?? DBNull.Value);
                datos.SetearParametros("@Email", nuevo.Email);
                datos.SetearParametros("@Telefono", nuevo.Telefono);
                datos.SetearParametros("@Direccion", nuevo.Direccion);
                datos.SetearParametros("@Estado", true);


                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(Paciente paciente)
        {

            CheckDni(paciente.Dni, paciente.IdPaciente);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(@"UPDATE Pacientes
                                       SET Dni = @Dni, Apellido = @Apellido, Nombre = @Nombre,
                                           FechaNacimiento = @FechaNacimiento, Email = @Email,
                                           Telefono = @Telefono, Direccion = @Direccion
                                       WHERE IdPaciente = @IdPaciente");

                datos.SetearParametros("@Dni", paciente.Dni);
                datos.SetearParametros("@Apellido", paciente.Apellido);
                datos.SetearParametros("@Nombre", paciente.Nombre);
                datos.SetearParametros("@FechaNacimiento", (object)paciente.FechaNacimiento ?? DBNull.Value);
                datos.SetearParametros("@Email", paciente.Email);
                datos.SetearParametros("@Telefono", paciente.Telefono);
                datos.SetearParametros("@Direccion", paciente.Direccion);
                datos.SetearParametros("@IdPaciente", paciente.IdPaciente);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void EliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {                
                datos.SetearConsulta("UPDATE Pacientes SET Estado = 0 WHERE IdPaciente = @IdPaciente");
                datos.SetearParametros("@IdPaciente", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de baja lógica al paciente: " + ex.Message);
            }
        }

        public void ReactivarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Pacientes SET Estado = 1 WHERE IdPaciente = @IdPaciente");
                datos.SetearParametros("@IdPaciente", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reactivar el paciente: " + ex.Message);
            }
        }

        private void CheckDni(string dni, int idPacienteActual)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT IdPaciente, Nombre, Apellido FROM Pacientes WHERE Dni = @Dni");
                datos.SetearParametros("@Dni", dni);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    int idEncontrado = (int)datos.Lector["IdPaciente"];
                    string nombre = (string)datos.Lector["Nombre"];
                    string apellido = (string)datos.Lector["Apellido"];

                    if (idEncontrado != idPacienteActual)
                    {
                        throw new Exception($"Error: El DNI '{dni}' ya está registrado a nombre de: {nombre} {apellido}.");
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
        }
    }
}