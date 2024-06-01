using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulacro2.Models;

namespace Simulacro2.Interfaces
{
    public interface IMedicoService
    {
        Task<IEnumerable<Medico>> GetAllMedicos();
        Task<Medico> GetMedicoById(int Id);
        Task<Medico> CreateMedico(Medico medico);
        Task<Medico> UpdateMedico(int Id, Medico medico);
        Task<Medico> DeleteMedico(int Id);
        Task<IEnumerable<Medico>> GetDeletedMedico();
        Task<IEnumerable<Paciente>> GetPacientesDeMedico(int medicoId);

    }
}