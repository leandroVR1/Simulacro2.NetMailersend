using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

using System.Collections.Generic;

namespace Simulacro2.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public EstadoEnum Estado { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidades { get; set; }
        public ICollection<Cita> Citas { get; set; }
    }
}
