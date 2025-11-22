using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EstadoTurno
    {
        public enum EstadoEnum
        {
            Pendiente = 1,
            Confirmado = 2,
            Completado = 3,
            NoAsistio = 4,
            Cancelado = 0
        }

        public EstadoEnum Estado { get; set; }
    }
}
