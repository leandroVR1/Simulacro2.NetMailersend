// Simulacro2.Services/MedicoService.cs
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
    // Implementación del servicio de médicos que implementa la interfaz IMedicoService.
    public class MedicoService : IMedicoService
    {
        // Contexto de base de datos para interactuar con la base de datos.
        private readonly BaseContext _context;

        // Constructor que inicializa el contexto de base de datos.
        public MedicoService(BaseContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo médico en la base de datos.
        public async Task<Medico> CreateMedico(Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        // Método para eliminar un médico de la base de datos.
        public async Task<Medico> DeleteMedico(int Id)
        {
            var medico = await _context.Medicos.FindAsync(Id);
            if (medico == null)
            {
                return null;
            }

            medico.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return medico;
        }

        // Método para obtener todos los médicos eliminados de la base de datos.
        public async Task<IEnumerable<Medico>> GetDeletedMedico()
        {
            return await _context.Medicos.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        // Método para obtener todos los médicos disponibles de la base de datos, incluyendo la especialidad.
        public async Task<IEnumerable<Medico>> GetAllMedicos()
        {
            return await _context.Medicos
                                .Where(m => m.Estado == EstadoEnum.Disponible)
                                .Include(m => m.Especialidad)
                                .ToListAsync();
        }

        // Método para obtener un médico por su ID, siempre y cuando esté disponible.
        public async Task<Medico> GetMedicoById(int id)
        {
            return await _context.Medicos.FirstOrDefaultAsync(m => m.Id == id && m.Estado == EstadoEnum.Disponible);
        }

        // Método para actualizar un médico en la base de datos.
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

       // Método para obtener todos los pacientes asociados a un médico específico.
public async Task<IEnumerable<Paciente>> GetPacientesDeMedico(int medicoId)
{
    // Se seleccionan las citas donde el médico es el especificado y el estado es disponible.
    // Luego, se seleccionan los pacientes asociados a esas citas.
    // Se utiliza el método Distinct para eliminar cualquier paciente duplicado.
    // Finalmente, se devuelve la lista de pacientes.
    return await _context.Citas
        // Se seleccionan las citas donde el médico es el especificado y el estado es disponible.
        // Esto se logra con el método Where, que filtra las citas según las condiciones especificadas.
       .Where(c => c.MedicoId == medicoId && c.Estado == EstadoEnum.Disponible)
       .Select(c => c.Paciente)
       .Distinct()
       .ToListAsync();
}
    }
}