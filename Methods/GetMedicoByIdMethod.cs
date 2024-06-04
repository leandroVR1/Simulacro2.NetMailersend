// Methods/GetMedicoByIdMethod.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    public static class GetMedicoByIdMethod
    {
        public static async Task<IActionResult> GetMedicoById(int Id, IMedicoService medicoService)
        {
            try
            {
                var medico = await medicoService.GetMedicoById(Id);
                if (medico == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(medico);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
