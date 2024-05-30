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
        Task<Paciente> AddPaciente(Paciente paciente);
        Task<Paciente> UpdatePaciente(Paciente paciente);
        Task<Paciente> DeletePaciente(int Id);
        Task<IEnumerable<Paciente>> GetDeletedPaciente();
        

        
    }
}