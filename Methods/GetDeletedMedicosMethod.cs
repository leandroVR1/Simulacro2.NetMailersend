// Methods/GetDeletedMedicosMethod.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;

namespace Simulacro2.Methods
{
    public static class GetDeletedMedicosMethod
    {
        public static async Task<IActionResult> GetDeletedMedicos(IMedicoService medicoService)
        {
            try
            {
                var deletedMedicos = await medicoService.GetDeletedMedico();
                return new OkObjectResult(deletedMedicos);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
