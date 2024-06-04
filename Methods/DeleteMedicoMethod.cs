// Methods/DeleteMedicoMethod.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    public static class DeleteMedicoMethod
    {
        public static async Task<IActionResult> DeleteMedico(int Id, IMedicoService medicoService)
        {
            try
            {
                var deleteMedico = await medicoService.DeleteMedico(Id);
                if (deleteMedico == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(deleteMedico);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
