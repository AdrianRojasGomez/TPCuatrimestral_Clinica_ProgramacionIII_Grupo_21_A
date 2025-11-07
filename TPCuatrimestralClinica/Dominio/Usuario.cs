using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoUsuario 
    { 
        Admin = 1,
        Medico = 2,
        Recepcion = 3,
        SinDefinir = 0
          
    
    }
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public RolUsuario Rol { get; set; }

        public int IdMedicoAsociado { get; set; }

        public TipoUsuario TipoUsuario { get; set; } = new TipoUsuario();

        public Medico Medico { get; set; }

        public bool Activo {  get; set; }   



    }
}
