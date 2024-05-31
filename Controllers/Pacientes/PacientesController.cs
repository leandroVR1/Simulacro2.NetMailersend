using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            return Ok(await _pacienteService.GetAllPacientes());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int Id)
        {
            try
            {
                var paciente = await _pacienteService.GetPacienteById(Id);
                if (paciente == null)
                {
                    return NotFound();
                }
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(Paciente paciente)
        {
            try
            {
                var createPaciente = await _pacienteService.CreatePaciente(paciente);
                return CreatedAtAction(nameof(GetPaciente), new { id = createPaciente.Id }, createPaciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Paciente>> UpdatePaciente(int Id, Paciente paciente)
        {
            try{
                var updatePaciente = await _pacienteService.UpdatePaciente(Id, paciente);
                if (updatePaciente == null)
                {
                    return NotFound();
                }
                return Ok(updatePaciente);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }


        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Medico>> DeletePaciente(int Id){
            try
            {
                var deletePaciente = await _pacienteService.DeletePaciente(Id);
                if (deletePaciente == null)
                {
                    return NotFound();
                }
                return Ok(deletePaciente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedPacientes()
        {
            try
            {
                var deletedPacientes = await _pacienteService.GetDeletedPaciente();
                return Ok(deletedPacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






    }
}