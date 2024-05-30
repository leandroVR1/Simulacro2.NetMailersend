using System;
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
            return await _context.Medicos.Where(a => a.Estado != Estado.Eliminado).Include(a => a.Especialidad).ToListAsync();
        }
        public async Task<Medico> GetMedicoById(int Id)
        {
            return await _context.Medicos.Include(a => a.Especialidad).FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<Medico> CreateMedico(Medico medico)
        {
            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> UpdateMedico(int Id, Medico medico)
        {
            var existingMedico = await _context.Medicos.FindAsync(Id);
            if (existingMedico == null)
            {
                return null;
            }
            existingMedico.Nombre = medico.Nombre;
            await _context.SaveChangesAsync();
            return existingMedico;
        }
        public async Task<Medico> DeleteMedico(int Id)
        {
            var existingMedico = await _context.Medicos.FindAsync(Id);
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
            return await _context.Medicos.Where(a => a.Estado == Enum.Estado.Eliminado).ToListAsync();
        }
      

    }
}
