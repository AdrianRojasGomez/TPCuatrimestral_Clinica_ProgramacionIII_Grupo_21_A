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
        public string Matricula { get; set; }

        public MedicoPorGuardia TurnoTrabajo { get; set; }
        public List<MedicoPorGuardia> turnoTrabajos { get; set; }
        public List<Especialidad> Especialidades { get; set; }
      
              

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
                        .Select(t => t.DiaSemana)
                        .Distinct()
                        .OrderBy(d => d));  
            }
        }


    }
}
