using AiHackApi.DTOs;
using AiHackApi.Models;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // Obter todos os pacientes
        [HttpGet]
        public async Task<IEnumerable<PacienteDto>> GetPacientes()
        {
            return await _pacienteService.GetAllPacientesAsync();
        }

        // Obter paciente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDto>> GetPacienteById(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        // Criar novo paciente
        [HttpPost]
        public async Task<ActionResult> CreatePaciente([FromBody] PacienteDto pacienteDto)
        {
            await _pacienteService.CreatePacienteAsync(pacienteDto);
            return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteDto.IdPaciente }, pacienteDto);
        }

        // Atualizar paciente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.IdPaciente)
            {
                return BadRequest();
            }

            await _pacienteService.UpdatePacienteAsync(pacienteDto);
            return NoContent();
        }

        // Deletar paciente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            await _pacienteService.DeletePacienteAsync(id);
            return NoContent();
        }
    }
}

