using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulacro2.Models
{
    public class Tratamiento
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int CitaId { get; set; }
        public Enum? Estado { get; set; }
        

    }
}