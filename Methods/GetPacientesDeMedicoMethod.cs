// Methods/GetPacientesDeMedicoMethod.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    public static class GetPacientesDeMedicoMethod
    {
        public static async Task<IActionResult> GetPacientesDeMedico(int medicoId, IMedicoService medicoService)
        {
            try
            {
                var pacientes = await medicoService.GetPacientesDeMedico(medicoId);
                return new OkObjectResult(pacientes);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
