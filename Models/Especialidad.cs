using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulacro2.Models
{
    public class Especialidad
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public EstadoEnum Estado { get; set; }
    public ICollection<Medico> Medicos { get; set; }
}
}