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

        public TurnoTrabajo TurnoTrabajo { get; set; }
        public List<Especialidad> Especialidades { get; set; }

        

        public Medico()
        {
            Especialidades = new List<Especialidad>();
        }


    }
}
