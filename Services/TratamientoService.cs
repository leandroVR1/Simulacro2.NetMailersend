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
    public class TratamientoService : ITratamientoService
    {
        private readonly BaseContext _context;

        public TratamientoService(BaseContext context)
        {
            _context = context;
        }

        public async Task<Tratamiento> CreateTratamiento(Tratamiento tratamiento)
        {
            _context.Tratamientos.Add(tratamiento);
            await _context.SaveChangesAsync();
            return tratamiento;
        }

        public async Task<Tratamiento> DeleteTratamiento(int Id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(Id);
            if (tratamiento == null)
            {
                return null;
            }

            tratamiento.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return tratamiento;
        }

        public async Task<IEnumerable<Tratamiento>> GetDeletedTratamiento()
        {
            return await _context.Tratamientos.Where(t => t.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        public async Task<IEnumerable<Tratamiento>> GetAllTratamientos()
        {
            return await _context.Tratamientos
                                 .Where(t => t.Estado == EstadoEnum.Disponible)
                                 .Include(t => t.Cita)
                                    .ThenInclude(c => c.Medico)
                                 .ToListAsync();
        }



        public async Task<Tratamiento> GetTratamientoById(int id)
        {
            return await _context.Tratamientos.FirstOrDefaultAsync(t => t.Id == id && t.Estado == EstadoEnum.Disponible);
        }

        public async Task<Tratamiento> UpdateTratamiento(int id, Tratamiento tratamiento)
        {
            var existingTratamiento = await _context.Tratamientos.FindAsync(id);
            if (existingTratamiento == null)
            {
                return null;
            }

            existingTratamiento.Descripcion = tratamiento.Descripcion;
            existingTratamiento.CitaId = tratamiento.CitaId;
            existingTratamiento.Estado = tratamiento.Estado;

            await _context.SaveChangesAsync();
            return existingTratamiento;
        }
    }
}
