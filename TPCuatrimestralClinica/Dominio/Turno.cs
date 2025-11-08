using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    //    IdTurno INT IDENTITY(1,1) PRIMARY KEY,
    //     NumeroTurno VARCHAR(50) NOT NULL,
    //     FechaInicio DATE NOT NULL,
    //    FechaFin DATE NOT NULL, -- Lo completo igual que FechaInicio, asumiendo que el turno es en un día

    //  HoraInicio TIME NOT NULL,
    //  HoraFin TIME NOT NULL,
    //    ObservacionesSolicitud NVARCHAR(400) NULL,
    //    ObservacionesDiagnostico NVARCHAR(400) NULL,
    //    IdMedico INT NOT NULL,
    //  IdPaciente INT NOT NULL,
    //    Motivo VARCHAR(50) NOT NULL,
    //  Estado BIT NOT NULL, 
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
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public Especialidad Especialidad { get; set; }
        public string Motivo { get; set; }
        public bool Estado { get; set; }

    }
}
