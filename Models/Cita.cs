using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoEnum Estado { get; set; }
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
        
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        
        [JsonIgnore]
        public ICollection<Tratamiento> Tratamientos { get; set; }
    }
}
