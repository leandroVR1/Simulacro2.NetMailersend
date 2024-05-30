using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

namespace Simulacro2.Data
{
    public interface IEspecialidad
    {
        Task <IEnumerable<Especialidad>> GetAllEspecialidades();
        Task<Especialidad> GetEspecialidadById(int Id);
        Task<Especialidad> AddEspecialidad(Especialidad especialidad);
        Task<Especialidad> UpdateEspecialidad(Especialidad especialidad);
        Task<Especialidad> DeleteEspecialidad(int Id);
        Task<IEnumerable<Especialidad>> GetDeletedEspecialidad();
        
    }
}