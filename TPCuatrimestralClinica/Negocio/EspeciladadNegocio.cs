using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EspeciladadNegocio
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
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("SELECT IdEspecialidad, Nombre FROM Especialidad WHERE IdEspecialidad=@id");
                d.SetearParametros("@id", id);
                d.EjecutarLectura();
                if (d.Lector.Read())
                {
                    return new Especialidad
                    {
                        IdEspecialidad = (int)d.Lector["IdEspecialidad"],
                        Nombre = (string)d.Lector["Nombre"]
                    };
                }
                return null;
            }
            finally { d.CerrarConexion(); }
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
                d.SetearConsulta("UPDATE Especialidad SET Nombre=@n WHERE IdEspecialidad=@id");
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
                d.SetearConsulta("DELETE FROM Especialidad WHERE IdEspecialidad=@id");
                d.SetearParametros("@id", id);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }


    }
}
