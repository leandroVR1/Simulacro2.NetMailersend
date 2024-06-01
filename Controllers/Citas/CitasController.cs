using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;
using Simulacro2.Services;

namespace Simulacro2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            return Ok(await _citaService.GetAllCitas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCitaById(int id)
        {
            var cita = await _citaService.GetCitaById(id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }


        [HttpPost]
        public async Task<ActionResult<Cita>> CreateCita(int medicoId, int pacienteId, int tratamientoId, DateTime fecha)
        {
            try
            {
                var createCita = await _citaService.CreateCita(medicoId, pacienteId, tratamientoId, fecha);
                if (createCita == null)
                {
                    // Alguno de los objetos no se encontró en la base de datos
                    // Puedes manejar esto de acuerdo a tus necesidades
                    return BadRequest("No se pudo crear la cita. Uno o más objetos no se encontraron en la base de datos.");
                }
                return CreatedAtAction(nameof(GetCitaById), new { id = createCita.Id }, createCita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{Id}")]
        public async Task<ActionResult<Cita>> UpdateCita(int Id, Cita cita)
        {
            try
            {
                var updateCita = await _citaService.UpdateCita(Id, cita);
                if (updateCita == null)
                {
                    return NotFound();
                }
                return Ok(updateCita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Cita>> DeleteCita(int Id)
        {
            try
            {
                var deleteCita = await _citaService.DeleteCita(Id);
                if (deleteCita == null)
                {
                    return NotFound();
                }
                return Ok(deleteCita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedCitas()
        {
            try
            {
                var deletedCitas = await _citaService.GetDeletedCita();
                return Ok(deletedCitas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("citas/count")]
        public async Task<ActionResult<int>> GetCitasCountByDay(DateTime fecha)
        {
            try
            {
                var citasCount = await _citaService.GetCitasCountByDay(fecha);
                return Ok(citasCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{medicoId}/citas")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitasMedicoByDay(int medicoId, [FromQuery] DateTime fecha)
        {
            try
            {
                var citas = await _citaService.GetCitasMedicoByDay(medicoId, fecha);
                return Ok(citas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }








    }
}