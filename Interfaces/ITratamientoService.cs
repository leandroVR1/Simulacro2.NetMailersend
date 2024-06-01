using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;


namespace Simulacro2.Interfaces
{
    public interface ITratamientoService
    {
        Task <IEnumerable<Tratamiento>> GetAllTratamientos();
        Task<Tratamiento> GetTratamientoById(int Id);
        Task<Tratamiento> CreateTratamiento(Tratamiento tratamiento);
        Task<Tratamiento> UpdateTratamiento(int Id ,Tratamiento tratamiento);
        Task<Tratamiento> DeleteTratamiento(int Id);
        Task<IEnumerable<Tratamiento>> GetDeletedTratamiento();
          
    }
}