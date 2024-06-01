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
    public class PacienteService : IPacienteService
    {
        private readonly BaseContext _context;
        public PacienteService(BaseContext context)
        {
            _context = context;
        }

        public async Task<Paciente> CreatePaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> DeletePaciente(int Id)
        {
            var paciente = await _context.Pacientes.FindAsync(Id);
            if (paciente == null)
            {
                return null;
            }

            paciente.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return paciente;
        }


        public async Task<IEnumerable<Paciente>> GetAllPacientes()
        {
            return await _context.Pacientes.Where(m => m.Estado == EstadoEnum.Disponible).ToListAsync();
        }

        public async Task<Paciente> GetPacienteById(int Id)
        {
            return await _context.Pacientes.FindAsync(Id);
        }

        public async Task<Paciente> UpdatePaciente(int Id, Paciente paciente)
        {
            var existingPaciente = await _context.Pacientes.FindAsync(paciente.Id);
            if (existingPaciente == null)
            {
                return null;
            }

            existingPaciente.Nombre = paciente.Nombre;
            existingPaciente.Apellido = paciente.Apellido;
            existingPaciente.Telefono = paciente.Telefono;
            existingPaciente.Direccion = paciente.Direccion;
            await _context.SaveChangesAsync();
            return existingPaciente;
        }

        public async Task<IEnumerable<Paciente>> GetDeletedPaciente()
        {
            return await _context.Pacientes.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        public async Task<int> GetCitasCountByPacienteId(int pacienteId)
        {
            var paciente = await _context.Pacientes.Include(p => p.Citas).FirstOrDefaultAsync(p => p.Id == pacienteId);
            if (paciente != null)
            {
                return paciente.Citas.Count;
            }
            return 0;
        }

        public async Task<IEnumerable<Cita>> GetHistorialMedico(int pacienteId)
        {
            return await _context.Citas
                .Where(c => c.PacienteId == pacienteId && c.Estado == EstadoEnum.Disponible)
                .ToListAsync();
        }

        





    }
}