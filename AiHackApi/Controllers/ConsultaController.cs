using AiHackApi.Models;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Lista todas as consultas.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        [HttpGet]
        public async Task<IEnumerable<Consulta>> GetConsultas()
        {
            return await _consultaService.GetAllConsultasAsync();
        }

        /// <summary>
        /// Obtém uma consulta específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da consulta.</param>
        /// <returns>A consulta correspondente ao ID informado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetConsultaById(int id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            return Ok(consulta);
        }

        /// <summary>
        /// Cadastra uma nova consulta.
        /// </summary>
        /// <param name="consulta">Objeto com os dados da consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateConsulta([FromBody] Consulta consulta)
        {
            await _consultaService.CreateConsultaAsync(consulta);
            return CreatedAtAction(nameof(GetConsultaById), new { id = consulta.IdConsulta }, consulta);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="id">O ID da consulta a ser atualizada.</param>
        /// <param name="consulta">Objeto com os dados atualizados da consulta.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsulta(int id, [FromBody] Consulta consulta)
        {
            if (id != consulta.IdConsulta)
            {
                return BadRequest();
            }

            await _consultaService.UpdateConsultaAsync(consulta);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma consulta pelo ID.
        /// </summary>
        /// <param name="id">O ID da consulta a ser excluída.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            await _consultaService.DeleteConsultaAsync(id);
            return NoContent();
        }
    }
}

