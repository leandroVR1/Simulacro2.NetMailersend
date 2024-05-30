using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulacro2.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Especialidad { get; set; }
        public Enum? Estado { get; set; }
        public int? EspecialidadId { get; set; }

    }
    
}