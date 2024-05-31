using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

namespace Simulacro2.Data
{
    public interface IEspecialidadService
    {
        Task <IEnumerable<Especialidad>> GetAllEspecialidades();
        Task<Especialidad> GetEspecialidadById(int Id);
        Task<Especialidad> CreateEspecialidad(Especialidad especialidad);
        Task<Especialidad> UpdateEspecialidad(int Id, Especialidad especialidad);
        Task<Especialidad> DeleteEspecialidad(int Id);
        Task<IEnumerable<Especialidad>> GetDeletedEspecialidad();
        
    }
}