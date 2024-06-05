using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simulacro2.Data;
using Simulacro2.Interfaces;
using Simulacro2.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simulacro2.Services
{
    public class CitaService : ICitaService
    {
        private readonly BaseContext _context;

        // Constructor que recibe la instancia de BaseContext
        public CitaService(BaseContext context)
        {
            _context = context;
        }

        // Método para crear una nueva cita
        public async Task<Cita> CreateCita(int medicoId, int pacienteId, int tratamientoId, DateTime fecha)
        {
            // Buscar los objetos relacionados en la base de datos
            var medico = await _context.Medicos.FindAsync(medicoId);
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            var tratamiento = await _context.Tratamientos.FindAsync(tratamientoId);

            // Verificar si alguno de los objetos no se encontró
            if (medico == null || paciente == null || tratamiento == null)
            {
                // Puedes manejar esto de acuerdo a tus necesidades
                return null;
            }

            // Crear una nueva instancia de Cita
            var cita = new Cita
            {
                Medico = medico,
                Paciente = paciente,
                Fecha = fecha,
                Estado = EstadoEnum.Disponible
            };

            // Añadir la cita a la base de datos y guardar los cambios
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            // Llamar al método para enviar el correo electrónico de recordatorio de cita
            await EnviarCorreoCita(cita);

            // Devolver la cita creada
            return cita;
        }

        // Método para eliminar una cita
        public async Task<Cita> DeleteCita(int Id)
        {
            // Buscar la cita en la base de datos
            var cita = await _context.Citas.FindAsync(Id);

            // Verificar si la cita se encontró
            if (cita == null)
            {
                return null;
            }

            // Cambiar el estado de la cita a Eliminado
            cita.Estado = EstadoEnum.Eliminado;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devolver la cita eliminada
            return cita;
        }

        // Método para obtener todas las citas eliminadas
        public async Task<IEnumerable<Cita>> GetDeletedCita()
        {
            // Consultar la base de datos y devolver las citas con estado Eliminado
            return await _context.Citas.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        // Método para obtener todas las citas disponibles
        public async Task<IEnumerable<Cita>> GetAllCitas()
        {
            // Consultar la base de datos y devolver las citas con estado Disponible
            // Incluir los objetos relacionados Medico y Paciente
            return await _context.Citas
                                .Where(m => m.Estado == EstadoEnum.Disponible)
                                .Include(m => m.Medico)
                                .Include(m => m.Paciente)
                                .ToListAsync();
        }

        // Método para obtener una cita por su Id
        public async Task<Cita> GetCitaById(int id)
        {
            // Buscar la cita en la base de datos
            return await _context.Citas.FindAsync(id);
        }

        // Método para actualizar una cita
        public async Task<Cita> UpdateCita(int Id, Cita cita)
        {
            // Buscar la cita en la base de datos
            var existingCita = await _context.Citas.FindAsync(Id);

            // Verificar si la cita se encontró
            if (existingCita == null)
            {
                return null;
            }

            // Actualizar los datos de la cita
            existingCita.Fecha = cita.Fecha;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devolver la cita actualizada
            return existingCita;
        }

        // Método para obtener el número de citas por día
        public async Task<int> GetCitasCountByDay(DateTime fecha)
        {
            // Consultar la base de datos y devolver el número de citas con fecha igual al parámetro
            return await _context.Citas.CountAsync(c => c.Fecha.HasValue && c.Fecha.Value.Date == fecha.Date);
        }

        // Método para obtener las citas de un médico por día
        public async Task<IEnumerable<Cita>> GetCitasMedicoByDay(int medicoId, DateTime fecha)
        {
            // Consultar la base de datos y devolver las citas del médico con fecha igual al parámetro
            return await _context.Citas
               .Where(c => c.MedicoId == medicoId && c.Fecha.HasValue && c.Fecha.Value.Date == fecha.Date)
               .ToListAsync();
        }

        // Método privado para enviar un correo electrónico de recordatorio de cita
        private async Task EnviarCorreoCita(Cita cita)
        {
            try
            {
                var fromEmail = "info@trial-pr9084zy1wxgw63d.mlsender.net";
                var toEmail = cita.Paciente.Correo; // Obtener el correo del paciente desde la cita
                var subject = "Recordatorio de cita médica";
                var body = $"Estimado/a {cita.Paciente.Nombre},\n\nLe recordamos que tiene una cita médica programada para el día {cita.Fecha} con el Dr. {cita.Medico.Nombre}.\n\nSaludos cordiales,\nEl equipo médico.";

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", "Tmlsn.ae6ea5d0a43ce3e392e48ce2b3ab851ac411114884699056b21cdc2850f5ef7b");

                var payload = new
                {
                    from = new { email = fromEmail },
                    to = new[] { new { email = toEmail } },
                    subject = subject,
                    text = body,
                    html = body // Puedes incluir un cuerpo HTML si lo deseas
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.mailersend.com/v1/email", content);

                response.EnsureSuccessStatusCode(); // Lanza una excepción si hay un error

                // Si llega aquí, el correo se envió correctamente
            }
            catch (Exception ex)
            {
                // Maneja cualquier error de envío de correo electrónico aquí
            }
        }
    }
}