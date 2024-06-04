// Methods/CreateMedicoMethod.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    public static class CreateMedicoMethod
    {
        public static async Task<IActionResult> CreateMedico(Medico medico, IMedicoService medicoService)
        {
            try
            {
                var createMedico = await medicoService.CreateMedico(medico);
                return new CreatedAtActionResult(nameof(GetMedicoByIdMethod.GetMedicoById), "Medicos", new { id = createMedico.Id }, createMedico);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
