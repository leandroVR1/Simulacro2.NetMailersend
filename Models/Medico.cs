using System.ComponentModel.DataAnnotations;


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

        public EstadoEnum? Estado { get; set; }

        public int EspecialidadId { get; set; }
        public Especialidad? Especialidad { get; set; }
         public ICollection<Cita>? Citas { get; set; }
    }
}
