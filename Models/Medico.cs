using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    public class Medico
    {
        // Propiedad que representa el ID del médico (clave primaria)
        public int Id { get; set; }

        // Nombre del médico, es requerido
        [Required]
        public string? Nombre { get; set; }

        // Correo electrónico del médico, es requerido y debe tener formato de email
        [Required]
        [EmailAddress]
        public string? Correo { get; set; }

        // Teléfono del médico, es requerido y debe tener formato de teléfono
        [Required]
        [Phone]
        public string? Telefono { get; set; }

        // Estado del médico, es un enumerado convertido a cadena en JSON
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoEnum? Estado { get; set; }

        // ID de la especialidad, clave foránea que referencia a la tabla Especialidad
        public int EspecialidadId { get; set; }
        
        // Objeto Especialidad, define la relación con la entidad Especialidad
        public Especialidad? Especialidad { get; set; }

        // Colección de citas del médico, no se incluye en la serialización JSON
        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
