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
    public class TratamientosController : ControllerBase
    {
        private readonly ITratamientoService _tratamientoService;

        public TratamientosController(ITratamientoService tratamientoService)
        {
            _tratamientoService = tratamientoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTratamientos()
        {
            return Ok(await _tratamientoService.GetAllTratamientos());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Tratamiento>> GetTratamiento(int Id)
        {
            try
            {
                var tratamiento = await _tratamientoService.GetTratamientoById(Id);
                if (tratamiento == null)
                {
                    return NotFound();
                }
                return Ok(tratamiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tratamiento>> CreateTratamiento(Tratamiento tratamiento)
        {
            try
            {
                var createTratamiento = await _tratamientoService.CreateTratamiento(tratamiento);
                return CreatedAtAction(nameof(GetTratamiento), new { id = createTratamiento.Id }, createTratamiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Tratamiento>> UpdateTratamiento(int Id, Tratamiento tratamiento)
        {
            try
            {
                var updateTratamiento = await _tratamientoService.UpdateTratamiento(Id, tratamiento);
                if (updateTratamiento == null)
                {
                    return NotFound();
                }
                return Ok(updateTratamiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Tratamiento>> DeleteTratamiento(int Id)
        {
            try
            {
                var deleteTratamiento = await _tratamientoService.DeleteTratamiento(Id);
                if (deleteTratamiento == null)
                {
                    return NotFound();
                }
                return Ok(deleteTratamiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedTratamientos()
        {
            try
            {
                var deletedTratamientos = await _tratamientoService.GetDeletedTratamiento();
                return Ok(deletedTratamientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
