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
            var medico = await _medicoService.GetMedicoById(Id);
            if (medico == null)
            {
                return NotFound();
            }
            return Ok(medico);
        }


        [HttpPost]
        public async Task<ActionResult<Medico>> AddMedico(Medico medico)
        {
            var createMedico = await _medicoService.CreateMedico(medico);
            return CreatedAtAction("AddMedico",new {id=createMedico.Id},createMedico);

        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Medico>> UpdateMedico(int Id , Medico medico)

        {
            var updateMedico = await _medicoService.UpdateMedico(Id, medico);
            if(updateMedico == null)
            {
                return NotFound();
            }
            return Ok(updateMedico);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Medico>> DeleteMedico(int Id)
        {
            var deleteMedico = await _medicoService.DeleteMedico(Id);
            if(deleteMedico == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedMedicos()
        {
            return Ok(await _medicoService.GetDeletedMedico());
        }
    }
}