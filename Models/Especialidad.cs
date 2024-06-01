using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    public class Especialidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstadoEnum Estado { get; set; }
        
        [JsonIgnore]
        public ICollection<Medico> Medicos { get; set; }
    }
}
