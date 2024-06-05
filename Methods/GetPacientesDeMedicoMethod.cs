// Methods/GetPacientesDeMedicoMethod.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Methods
{
    // Esta clase contiene un método estático para recuperar una lista de pacientes asociados a un médico específico.
    public static class GetPacientesDeMedicoMethod
    {
        // Este método se encarga de recuperar los pacientes asociados a un médico dado.
        // Acepta un entero que representa el ID del médico y una instancia de la interfaz IMedicoService.
        // Devuelve un IActionResult, que puede ser un OkObjectResult con los datos recuperados o un StatusCodeResult en caso de error.
        public static async Task<IActionResult> GetPacientesDeMedico(int medicoId, IMedicoService medicoService)
        {
            try
            {
                // Llama al método GetPacientesDeMedico del medicoService para recuperar los pacientes asociados al médico dado.
                var pacientes = await medicoService.GetPacientesDeMedico(medicoId);

                // Devuelve un OkObjectResult con los pacientes recuperados.
                return new OkObjectResult(pacientes);
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve un StatusCodeResult con un código de estado de 500 (Error Interno del Servidor).
                return new StatusCodeResult(500);
            }
        }
    }
}