using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Medico
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        //Adri: Agrego propiedad NombreCompleto para mostrar en dropdowns
        public string NombreCompleto
        {
            get
            {
                return $"Dr. {Apellido}, {Nombre}";
            }
        }

        public string Matricula { get; set; }

        public MedicoPorGuardia TurnoTrabajo { get; set; }
        public List<MedicoPorGuardia> turnoTrabajos { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public bool  TieneUsuario { get; set; }
        public bool UsuarioActivo { get; set; }
        public Medico()
        {
            turnoTrabajos = new List<MedicoPorGuardia>();

            Especialidades = new List<Especialidad>();
        }

        public string DiasResumen
        {
            get
            {
                if (turnoTrabajos == null || turnoTrabajos.Count == 0)
                    return string.Empty;

                return string.Join(", ",
                    turnoTrabajos
                        .Select(t => DiaEnEspanol(t.DiaSemana))
                        .Distinct()
                        .OrderBy(d => d));  
            }
        }
        public static string DiaEnEspanol(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return "";
            }
        }



    }
}
