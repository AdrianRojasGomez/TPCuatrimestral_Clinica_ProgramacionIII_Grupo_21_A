using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class MedicoPorGuardiaNegocio
    {

        public List<MedicoPorGuardia> Listar()
        {
            List<MedicoPorGuardia> lista = new List<MedicoPorGuardia>();
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta(@"
            SELECT 
                IdGuardia AS IdTurnoTrabajo,  
                Nombre,
                HoraInicio,
                HoraFin
            FROM Guardias
            ORDER BY IdGuardia");

                d.EjecutarLectura();

                while (d.Lector.Read())
                {
                    var t = new MedicoPorGuardia();
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



        public MedicoPorGuardia Obtener(int id)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("SELECT IdTurnoTrabajo, Nombre, HoraInicio, HoraFin ,DiaSemana FROM TurnoTrabajo WHERE IdTurnoTrabajo=@id");
                d.SetearParametros("@id", id);
                d.EjecutarLectura();

                if (d.Lector.Read())
                {
                    return new MedicoPorGuardia
                    {
                        IdTurnoTrabajo = (int)d.Lector["IdTurnoTrabajo"],
                        Nombre = (string)d.Lector["Nombre"],
                        HoraInicio = (TimeSpan)d.Lector["HoraInicio"],
                        HoraFin = (TimeSpan)d.Lector["HoraFin"],
                        DiaSemana = (string)d.Lector["DiaSemana"]
                    };
                }
                return null;
            }
            finally { d.CerrarConexion(); }
        }

        public void Agregar(MedicoPorGuardia turno)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta("INSERT INTO TurnoTrabajo (Nombre, HoraInicio, HoraFin,DiaSemana) VALUES (@n, @hi, @hf,ds)");
                d.SetearParametros("@n", turno.Nombre);
                d.SetearParametros("@hi", turno.HoraInicio);
                d.SetearParametros("@hf", turno.HoraFin);
                d.SetearParametros("@ds" ,turno.DiaSemana);
                d.EjecutarAccion();
            }
            finally { d.CerrarConexion(); }
        }

        public void Modificar(MedicoPorGuardia turno)
        {
            AccesoDatos d = new AccesoDatos();
            try
            {
                d.SetearConsulta(@"UPDATE TurnoTrabajo
                               SET Nombre=@n, HoraInicio=@hi, HoraFin=@hf ,DiaSemana=@df
                               WHERE IdTurnoTrabajo=@id");
                d.SetearParametros("@id", turno.IdTurnoTrabajo);
                d.SetearParametros("@n", turno.Nombre);
                d.SetearParametros("@hi", turno.HoraInicio);
                d.SetearParametros("@hf", turno.HoraFin);
                d.SetearParametros("@df", turno.DiaSemana);
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
