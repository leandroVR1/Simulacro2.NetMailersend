using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simulacro2.Data;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly BaseContext _context;

        public MedicoService(BaseContext context)
        {
            _context = context;
        }

        public async Task<Medico> CreateMedico(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task<Medico> DeleteMedico(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return null;
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task<IEnumerable<Medico>> GetDeletedMedico()
        {
            return await _context.Medicos.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        public async Task<IEnumerable<Medico>> GetAllMedicos()
        {
            return await _context.Medicos.Where(m => m.Estado == EstadoEnum.Disponible).ToListAsync();
            
        }



        public async Task<Medico> GetMedicoById(int id)
        {
            return await _context.Medicos.FirstOrDefaultAsync(m => m.Id == id && m.Estado == EstadoEnum.Disponible);
        }

        public async Task<Medico> UpdateMedico(int Id, Medico medico)
        {
            var existingMedico = await _context.Medicos.FindAsync(Id);
            if (existingMedico == null)
            {
                return null;
            }

            existingMedico.Nombre = medico.Nombre;
            existingMedico.Correo = medico.Correo;
            existingMedico.Telefono = medico.Telefono;
            existingMedico.EspecialidadId = medico.EspecialidadId;

            await _context.SaveChangesAsync();
            return existingMedico;
        }
    }
}
