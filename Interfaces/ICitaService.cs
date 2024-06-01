using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

namespace Simulacro2.Interfaces
{
    public interface ICitaService
    {
        Task <IEnumerable<Cita>> GetAllCitas();
        Task<Cita> GetCitaById(int Id);
        Task<Cita> CreateCita(int medicoId, int pacienteId, int tratamientoId, DateTime fecha);

        Task<Cita> UpdateCita(int Id , Cita cita);
        Task<Cita> DeleteCita(int Id);
        Task<IEnumerable<Cita>> GetDeletedCita();
        Task<int> GetCitasCountByDay(DateTime fecha);
        Task<IEnumerable<Cita>> GetCitasMedicoByDay(int medicoId, DateTime fecha);
        

        
    }
}