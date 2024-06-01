using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    public class Medico
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string? Correo { get; set; }

        [Required]
        [Phone]
        public string? Telefono { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoEnum? Estado { get; set; }

        public int EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }

        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
