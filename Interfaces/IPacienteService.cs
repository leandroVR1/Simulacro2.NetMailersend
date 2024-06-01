using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;


namespace Simulacro2.Interfaces
{
    public interface IPacienteService
    {
        Task <IEnumerable<Paciente>> GetAllPacientes();
        Task<Paciente> GetPacienteById(int Id);
        Task<Paciente> CreatePaciente(Paciente paciente);
        Task<Paciente> UpdatePaciente(int Id ,Paciente paciente);
        Task<Paciente> DeletePaciente(int Id);
        Task<IEnumerable<Paciente>> GetDeletedPaciente();
        Task<int> GetCitasCountByPacienteId(int pacienteId);
        Task<IEnumerable<Cita>> GetHistorialMedico(int pacienteId);
        

        

        
    }
}