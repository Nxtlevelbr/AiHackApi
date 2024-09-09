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

        /// <summary>
        /// Lista todos os pacientes cadastrados.
        /// </summary>
        /// <returns>Uma lista de pacientes.</returns>
        [HttpGet]
        public async Task<IEnumerable<PacienteDto>> GetPacientes()
        {
            return await _pacienteService.GetAllPacientesAsync();
        }

        /// <summary>
        /// Obtém um paciente específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do paciente.</param>
        /// <returns>O paciente correspondente ao ID informado.</returns>
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

        /// <summary>
        /// Cadastra um novo paciente.
        /// </summary>
        /// <param name="pacienteDto">Objeto com os dados do paciente a ser criado.</param>
        /// <returns>O paciente recém-criado.</returns>
        [HttpPost]
        public async Task<ActionResult> CreatePaciente([FromBody] PacienteDto pacienteDto)
        {
            await _pacienteService.CreatePacienteAsync(pacienteDto);
            return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteDto.IdPaciente }, pacienteDto);
        }

        /// <summary>
        /// Atualiza os dados de um paciente existente.
        /// </summary>
        /// <param name="id">O ID do paciente a ser atualizado.</param>
        /// <param name="pacienteDto">Objeto com os dados atualizados do paciente.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
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

        /// <summary>
        /// Exclui um paciente pelo ID.
        /// </summary>
        /// <param name="id">O ID do paciente a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
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
