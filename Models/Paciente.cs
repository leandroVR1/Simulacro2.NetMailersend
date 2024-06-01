using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
     
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoEnum? Estado { get; set; }
        
        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; }
    }
}
