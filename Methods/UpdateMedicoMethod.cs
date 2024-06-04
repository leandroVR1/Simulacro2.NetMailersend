// Methods/UpdateMedicoMethod.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    public static class UpdateMedicoMethod
    {
        public static async Task<IActionResult> UpdateMedico(int Id, Medico medico, IMedicoService medicoService)
        {
            try
            {
                var updateMedico = await medicoService.UpdateMedico(Id, medico);
                if (updateMedico == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(updateMedico);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
