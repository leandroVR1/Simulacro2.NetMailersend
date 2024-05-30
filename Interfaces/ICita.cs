using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

namespace Simulacro2.Interfaces
{
    public interface ICita
    {
        Task <IEnumerable<Cita>> GetAllCitas();
        Task<Cita> GetCitaById(int Id);
        Task<Cita> AddCita(Cita cita);
        Task<Cita> UpdateCita(Cita cita);
        Task<Cita> DeleteCita(int Id);
        Task<IEnumerable<Cita>> GetDeletedCita();
        
    }
}