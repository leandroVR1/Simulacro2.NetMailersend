// Methods/GetMedicosMethod.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;

namespace Simulacro2.Methods
{
    public static class GetMedicosMethod
    {
        public static async Task<IActionResult> GetAllMedicos(IMedicoService medicoService)
        {
            return new OkObjectResult(await medicoService.GetAllMedicos());
        }
    }
}
