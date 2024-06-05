using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simulacro2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        // GET: api/Medico/GetCitasDeMedico/5
        [HttpGet("GetCitasDeMedico/{medicoId}")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitasDeMedico(int medicoId)
        {
            var citas = await _medicoService.GetCitasDeMedico(medicoId);
            return Ok(citas);
        }

        // Otros métodos de acción para el controlador (Create, Update, Delete, etc.)
    }
}