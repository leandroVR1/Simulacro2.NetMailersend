using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulacro2.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public EstadoEnum Estado { get; set; }
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
        public Paciente Pacientes { get; set; }
        public Medico Medicos { get; set; }
        public ICollection<Tratamiento> Tratamientos { get; set; }

    }
}