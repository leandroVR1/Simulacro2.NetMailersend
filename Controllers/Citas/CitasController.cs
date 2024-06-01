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

        [HttpGet("{Id}")]
        public async Task<ActionResult<Cita>> GetCita(int Id)
        {
            try
            {
                var cita = await _citaService.GetCitaById(Id);
                if (cita == null)
                {
                    return NotFound();
                }
                return Ok(cita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cita>> CreateCita(Cita cita)
        {
            try
            {
                var createCita = await _citaService.CreateCita(cita);
                return CreatedAtAction(nameof(GetCita), new { id = createCita.Id }, createCita);
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