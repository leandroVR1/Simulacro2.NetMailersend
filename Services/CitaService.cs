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
    public class CitaService : ICitaService
    {
        private readonly BaseContext _context;

        public CitaService(BaseContext context)
        {
            _context = context;
        }

        public async Task<Cita> CreateCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<Cita> DeleteCita(int Id)
        {
            var cita = await _context.Citas.FindAsync(Id);
            if (cita == null)
            {
                return null;
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<IEnumerable<Cita>> GetDeletedCitas()
        {
            return await _context.Citas.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetAllCitas()
        {
            return await _context.Citas.ToListAsync();
        }

        public async Task<Cita> GetCitaById(int id)
        {
            return await _context.Citas.FindAsync(id);
        }

        public async Task<Cita> UpdateCita(int Id, Cita cita)
        {
            var existingCita = await _context.Citas.FindAsync(Id);
            if (existingCita == null)
            {
                return null;
            }

            existingCita.Fecha = cita.Fecha;
            await _context.SaveChangesAsync();
            return existingCita;
        }


        
    }
}