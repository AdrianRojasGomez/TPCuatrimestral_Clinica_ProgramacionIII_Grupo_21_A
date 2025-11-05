using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class TurnoTrabajoNegocio
    {

        public List<TurnoTrabajo> Listar()
        {
            List<TurnoTrabajo> lista = new List<TurnoTrabajo>();
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta(@"
            SELECT 
                IdGuardia AS IdTurnoTrabajo,  -- alias para que el nombre coincida
                Nombre,
                HoraInicio,
                HoraFin
            FROM Guardias
            ORDER BY IdGuardia");

                d.EjecutarLectura();

                while (d.Lector.Read())
                {
                    var t = new TurnoTrabajo();
                    t.IdTurnoTrabajo = (int)d.Lector["IdTurnoTrabajo"];  
                    t.Nombre = (string)d.Lector["Nombre"];
                    t.HoraInicio = (TimeSpan)d.Lector["HoraInicio"];
                    t.HoraFin = (TimeSpan)d.Lector["HoraFin"];
                    lista.Add(t);
                }
                return lista;
            }
            finally
            {
                d.CerrarConexion();
            }
        }



        public TurnoTrabajo Obtener(int id)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("SELECT IdTurnoTrabajo, Nombre, HoraInicio, HoraFin FROM TurnoTrabajo WHERE IdTurnoTrabajo=@id");
                d.SetearParametros("@id", id);
                d.EjecutarLectura();

                if (d.Lector.Read())
                {
                    return new TurnoTrabajo
                    {
                        IdTurnoTrabajo = (int)d.Lector["IdTurnoTrabajo"],
                        Nombre = (string)d.Lector["Nombre"],
                        HoraInicio = (TimeSpan)d.Lector["HoraInicio"],
                        HoraFin = (TimeSpan)d.Lector["HoraFin"]
                    };
                }
                return null;
            }
            finally { d.CerrarConexion(); }
        }

        public void Agregar(TurnoTrabajo turno)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("INSERT INTO TurnoTrabajo (Nombre, HoraInicio, HoraFin) VALUES (@n, @hi, @hf)");
                d.SetearParametros("@n", turno.Nombre);
                d.SetearParametros("@hi", turno.HoraInicio);
                d.SetearParametros("@hf", turno.HoraFin);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        public void Modificar(TurnoTrabajo turno)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta(@"UPDATE TurnoTrabajo
                               SET Nombre=@n, HoraInicio=@hi, HoraFin=@hf
                               WHERE IdTurnoTrabajo=@id");
                d.SetearParametros("@id", turno.IdTurnoTrabajo);
                d.SetearParametros("@n", turno.Nombre);
                d.SetearParametros("@hi", turno.HoraInicio);
                d.SetearParametros("@hf", turno.HoraFin);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        public void Eliminar(int id)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("DELETE FROM TurnoTrabajo WHERE IdTurnoTrabajo=@id");
                d.SetearParametros("@id", id);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }





    }
}
