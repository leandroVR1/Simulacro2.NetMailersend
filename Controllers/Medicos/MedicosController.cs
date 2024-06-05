using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Methods;
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
            return await GetMedicosMethod.GetAllMedicos(_medicoService);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMedico(int Id)
        {
            return await GetMedicoByIdMethod.GetMedicoById(Id, _medicoService);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedico(Medico medico)
        {
            return await CreateMedicoMethod.CreateMedico(medico, _medicoService);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMedico(int Id, Medico medico)
        {
            return await UpdateMedicoMethod.UpdateMedico(Id, medico, _medicoService);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMedico(int Id)
        {
            return await DeleteMedicoMethod.DeleteMedico(Id, _medicoService);
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedMedicos()
        {
            return await GetDeletedMedicosMethod.GetDeletedMedicos(_medicoService);
        }

        [HttpGet("{medicoId}/pacientes")]
        public async Task<IActionResult> GetPacientesDeMedico(int medicoId)
        {
            return await GetPacientesDeMedicoMethod.GetPacientesDeMedico(medicoId, _medicoService);
        }

        
    }
}
