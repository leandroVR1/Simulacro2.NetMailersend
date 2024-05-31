using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simulacro2.Data;
using Simulacro2.Interfaces;
using Simulacro2.Models;

namespace Simulacro2.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class EspecialidadesController : ControllerBase

        {
            private readonly IEspecialidadService _especialidadService;
            public EspecialidadesController(IEspecialidadService especialidadService)
            {
                _especialidadService = especialidadService;
            }

            [HttpGet]
            public async Task<IActionResult> GetEspecialidades()
            {
                try
                {
                    var especialidades = await _especialidadService.GetAllEspecialidades();
                    return Ok(especialidades);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            [HttpGet("{Id}")]
            public async Task<ActionResult<Especialidad>> GetEspecialidad(int Id)
            {
                try
                {
                    var especialidad = await _especialidadService.GetEspecialidadById(Id);
                    if (especialidad == null)
                    {
                        return NotFound();
                    }
                    return Ok(especialidad);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPost]
            public async Task<ActionResult<Especialidad>> CreateEspecialidad(Especialidad especialidad)
            {
                try
                {
                    var createEspecialidad = await _especialidadService.CreateEspecialidad(especialidad);
                    return CreatedAtAction(nameof(GetEspecialidad), new { id = createEspecialidad.Id }, createEspecialidad);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            [HttpPut("{Id}")]
            public async Task<ActionResult<Especialidad>> UpdateEspecialidad(int Id, Especialidad especialidad)
            {
                try
                {
                    var updateEspecialidad = await _especialidadService.UpdateEspecialidad(Id, especialidad);
                    if (updateEspecialidad == null)
                    {
                        return NotFound();
                    }
                    return Ok(updateEspecialidad);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            [HttpDelete("{Id}")]
            public async Task<ActionResult<Especialidad>> DeleteEspecialidad(int Id)
            {
                try
                {
                    var deleteEspecialidad = await _especialidadService.DeleteEspecialidad(Id);
                    if (deleteEspecialidad == null)
                    {
                        return NotFound();
                    }
                    return Ok(deleteEspecialidad);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            [HttpGet("deleted")]
            public async Task<IActionResult> GetDeletedEspecialidades()
            {
                try
                {
                    var deletedEspecialidades = await _especialidadService.GetDeletedEspecialidad();
                    return Ok(deletedEspecialidades);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }


            
           
        }
        
    }
