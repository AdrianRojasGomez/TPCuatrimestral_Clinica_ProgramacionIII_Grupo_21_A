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
            public string NumeroTurno { get; set; }
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public TimeSpan HoraInicio { get; set; }
            public TimeSpan HoraFin { get; set; }
            public string ObservacionesSolicitud { get; set; }
            public string ObservacionesDiagnostico { get; set; }

            public int IdMedico { get; set; }
            public int IdPaciente { get; set; }
            public string Motivo { get; set; }
            public int Estado { get; set; }

            public Paciente Paciente { get; set; }
            public Medico Medico { get; set; }

    }
}
