using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simulacro2.Interfaces;
using Simulacro2.Models;
using Simulacro2.Data;

namespace Simulacro2.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly BaseContext _context;
        public MedicoService(BaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medico>> GetAllMedicos()
        {
            return await _context.Medicos
                .Where(a => a.Estado != EstadoEnum.Eliminado)
                .Include(a => a.Especialidades)
                .ToListAsync();
        }

        public async Task<Medico> GetMedicoById(int id)
        {
            return await _context.Medicos
                .Include(a => a.Especialidades)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Medico> CreateMedico(Medico medico)
        {
            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateMedico(int id, Medico medico)
        {
            var existingMedico = await _context.Medicos.FindAsync(id);
            if (existingMedico == null)
            {
                return null;
            }
            existingMedico.Nombre = medico.Nombre;
            existingMedico.Correo = medico.Correo;
            existingMedico.Telefono = medico.Telefono;
            existingMedico.Estado = medico.Estado;
            existingMedico.EspecialidadId = medico.EspecialidadId;

            await _context.SaveChangesAsync();
            return existingMedico;
        }

        public async Task<Medico> DeleteMedico(int id)
        {
            var existingMedico = await _context.Medicos.FindAsync(id);
            if (existingMedico == null)
            {
                return null;
            }
            _context.Medicos.Remove(existingMedico);
            await _context.SaveChangesAsync();
            return existingMedico;
        }

        public async Task<IEnumerable<Medico>> GetDeletedMedico()
        {
            return await _context.Medicos
                .Where(a => a.Estado == EstadoEnum.Eliminado)
                .Include(a => a.Especialidades)
                .ToListAsync();
        }
    }
}
