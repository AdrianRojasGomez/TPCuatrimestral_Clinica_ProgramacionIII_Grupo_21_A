using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Turno
    {

        public int IdTurno { get; set; }
        public int NumeroTurno { get; set; }
        public DateTime FechaHora { get; set; }
        public string ObservacionesSolicitud { get; set; }
        public string ObservacionesDiagnostico { get; set; }

        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public Especialidad Especialidad { get; set; }
        public EstadoTurno Estado { get; set; }


    }
}
