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
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMedicos()
        {
            return Ok(await _medicoService.GetAllMedicos());
        }



        [HttpGet("{Id}")]
        public async Task<ActionResult<Medico>> GetMedico(int Id)
        {
            try
            {
                var medico = await _medicoService.GetMedicoById(Id);
                if (medico == null)
                {
                    return NotFound();
                }
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CreateMedico(Medico medico)
        {
            try
            {
                var createMedico = await _medicoService.CreateMedico(medico);
                return CreatedAtAction(nameof(GetMedico), new { id = createMedico.Id }, createMedico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Medico>> UpdateMedico(int Id, Medico medico)
        {
            try
            {
                var updateMedico = await _medicoService.UpdateMedico(Id, medico);
                if (updateMedico == null)
                {
                    return NotFound();
                }
                return Ok(updateMedico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Medico>> DeleteMedico(int Id)
        {
            try
            {
                var deleteMedico = await _medicoService.DeleteMedico(Id);
                if (deleteMedico == null)
                {
                    return NotFound();
                }
                return Ok(deleteMedico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedMedicos()
        {
            try
            {
                var deletedMedicos = await _medicoService.GetDeletedMedico();
                return Ok(deletedMedicos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{medicoId}/pacientes")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientesDeMedico(int medicoId)
        {
            try
            {
                var pacientes = await _medicoService.GetPacientesDeMedico(medicoId);
                return Ok(pacientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}
