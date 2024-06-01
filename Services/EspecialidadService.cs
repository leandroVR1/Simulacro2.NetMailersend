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
    public class EspecialidadService : IEspecialidadService

    {
        private readonly BaseContext _context;
        public EspecialidadService(BaseContext context)
        {
            _context = context;
        }
        public async Task<Especialidad> CreateEspecialidad(Especialidad especialidad)
        {
            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();
            return especialidad;
        }
        public async Task<Especialidad> DeleteEspecialidad(int Id)
        {
          {
            var especialidad = await _context.Especialidades.FindAsync(Id);
            if (especialidad == null)
            {
                return null;
            }

            especialidad.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return especialidad;
        }
        }
        public async Task<IEnumerable<Especialidad>> GetDeletedEspecialidad()
        {
            return await _context.Especialidades.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }
        public async Task<IEnumerable<Especialidad>> GetAllEspecialidades()
        {
           
            
                return await _context.Especialidades
                    .Where(m => m.Estado == EstadoEnum.Disponible)
                    .ToListAsync();

                
          

        }
        public async Task<Especialidad> GetEspecialidadById(int Id)
        {
            return await _context.Especialidades.FirstOrDefaultAsync(m => m.Id == Id && m.Estado == EstadoEnum.Disponible);
        }

        public async Task<Especialidad> UpdateEspecialidad(int Id, Especialidad especialidad)
        {
            var existingEspecialidad = await _context.Especialidades.FindAsync(Id);
            if (existingEspecialidad == null)
            {
                return null;
            }

            existingEspecialidad.Nombre = especialidad.Nombre;
            existingEspecialidad.Descripcion = especialidad.Descripcion;

            await _context.SaveChangesAsync();
            return existingEspecialidad;







        }
    }
}