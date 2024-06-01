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

        public CitaService(BaseContext context)
        {
            _context = context;
        }

       public async Task<Cita> CreateCita(int medicoId, int pacienteId, int tratamientoId, DateTime fecha)
{
    var medico = await _context.Medicos.FindAsync(medicoId);
    var paciente = await _context.Pacientes.FindAsync(pacienteId);
    var tratamiento = await _context.Tratamientos.FindAsync(tratamientoId);

    if (medico == null || paciente == null || tratamiento == null)
    {
        // Alguno de los objetos no se encontró en la base de datos
        // Puedes manejar esto de acuerdo a tus necesidades
        return null;
    }

    var cita = new Cita
    {
        Medico = medico,
        Paciente = paciente,
        Fecha = fecha,
        Estado = EstadoEnum.Disponible
    };

    _context.Citas.Add(cita);
    await _context.SaveChangesAsync();

    // Llamar al método para enviar el correo electrónico
    await EnviarCorreoCita(cita);

    return cita;
}



        public async Task<Cita> DeleteCita(int Id)
        {
            var cita = await _context.Citas.FindAsync(Id);
            if (cita == null)
            {
                return null;
            }

            cita.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task<IEnumerable<Cita>> GetDeletedCita()
        {
            return await _context.Citas.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        public async Task<IEnumerable<Cita>> GetAllCitas()
        {
            return await _context.Citas
                                 .Where(m => m.Estado == EstadoEnum.Disponible)
                                 .Include(m => m.Medico)
                                 .Include(m => m.Paciente)
                                 .ToListAsync();
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

        public async Task<int> GetCitasCountByDay(DateTime fecha)
        {

            return await _context.Citas.CountAsync(c => c.Fecha.HasValue && c.Fecha.Value.Date == fecha.Date);
        }

        public async Task<IEnumerable<Cita>> GetCitasMedicoByDay(int medicoId, DateTime fecha)
        {
            return await _context.Citas
                .Where(c => c.MedicoId == medicoId && c.Fecha.HasValue && c.Fecha.Value.Date == fecha.Date)
                .ToListAsync();
        }

    
            // Constructor y otros métodos

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