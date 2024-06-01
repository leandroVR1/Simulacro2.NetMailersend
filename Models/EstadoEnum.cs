using System.Text.Json.Serialization;

namespace Simulacro2.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EstadoEnum
    {
        Disponible,
        Eliminado
    }
}
