using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> Listar()
        {
            List<Especialidad> listaespecilidad = new List<Especialidad>();
            AccesoDatos d = new AccesoDatos();
            try
            {
                
                d.SetearConsulta("SELECT IdEspecialidad, Nombre FROM Especialidades ORDER BY Nombre");
                d.EjecutarLectura();

                while (d.Lector.Read())
                {
                    Especialidad e = new Especialidad();
                    e.IdEspecialidad = (int)d.Lector["IdEspecialidad"];
                    e.Nombre = (string)d.Lector["Nombre"];
                    listaespecilidad.Add(e);
                }
                return listaespecilidad;
            }
            finally
            {
                d.CerrarConexion();
            }
        }

       
        public Especialidad ObtenerEspecialidad(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT E.* from Especialidades as E where IdEspecialidad = @id");
                datos.SetearParametros("@id", id);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad();
                    especialidad.IdEspecialidad = (int)datos.Lector["IdEspecialidad"];
                    especialidad.Nombre = datos.Lector["Nombre"].ToString();
                    return especialidad;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar especialidad por ID: " + ex.Message);
            }
            finally { datos.CerrarConexion(); }
        }

     
        public void AgregarEspecialidad(Especialidad esp)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("INSERT INTO Especialidades (Nombre) VALUES (@n)");
                d.SetearParametros("@n", esp.Nombre);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        
        public void ModificarEspecilidad(Especialidad esp)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("UPDATE Especialidades SET Nombre=@n WHERE IdEspecialidad=@id");
                d.SetearParametros("@id", esp.IdEspecialidad);
                d.SetearParametros("@n", esp.Nombre);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        
        public void EliminarEspecilidad(int id)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("DELETE FROM Especialidades WHERE IdEspecialidad=@id");
                d.SetearParametros("@id", id);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        public bool ExisteEspecialidad(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta(
                    "SELECT COUNT(*) AS Cantidad " +
                    "FROM Especialidades " +
                    "WHERE LOWER(Nombre) = LOWER(@Nombre)"
                );

                datos.SetearParametros("@Nombre", nombre.Trim());
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector["Cantidad"];
                    return cantidad > 0;   // true = ya existe
                }

                return false;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


    }
}
