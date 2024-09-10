// Importações necessárias para o funcionamento do controlador
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Consulta'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio da 'Consulta'
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para criar controladores de API
using System.Collections.Generic; // Usado para trabalhar com listas genéricas (IEnumerable)
using System.Threading.Tasks; // Usado para trabalhar com operações assíncronas (async/await)

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para acessar os endpoints deste controlador (ex: api/Consulta)
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService; // Campo privado para injeção de dependência do serviço de consultas

        // Construtor que injeta o serviço de consultas
        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Lista todas as consultas.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        [HttpGet] // Define que este método responde a requisições HTTP GET
        public async Task<IEnumerable<Consulta>> GetConsultas()
        {
            // Chama o serviço para obter todas as consultas e retorna a lista
            return await _consultaService.GetAllConsultasAsync();
        }

        /// <summary>
        /// Obtém uma consulta específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da consulta.</param>
        /// <returns>A consulta correspondente ao ID informado.</returns>
        [HttpGet("{id}")] // Define que este método responde a requisições GET com um ID na URL (ex: api/Consulta/1)
        public async Task<ActionResult<Consulta>> GetConsultaById(int id)
        {
            // Usa o serviço para obter uma consulta específica pelo ID
            var consulta = await _consultaService.GetConsultaByIdAsync(id);

            // Se a consulta não for encontrada, retorna um erro 404 (Not Found)
            if (consulta == null)
            {
                return NotFound();
            }

            // Retorna a consulta encontrada com status 200 (OK)
            return Ok(consulta);
        }

        /// <summary>
        /// Cadastra uma nova consulta.
        /// </summary>
        /// <param name="consulta">Objeto com os dados da consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        [HttpPost] // Define que este método responde a requisições HTTP POST para criar uma nova consulta
        public async Task<ActionResult> CreateConsulta([FromBody] Consulta consulta)
        {
            // Chama o serviço para criar a nova consulta
            await _consultaService.CreateConsultaAsync(consulta);

            // Retorna 201 (Created) e a URL para acessar a consulta recém-criada
            return CreatedAtAction(nameof(GetConsultaById), new { id = consulta.IdConsulta }, consulta);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="id">O ID da consulta a ser atualizada.</param>
        /// <param name="consulta">Objeto com os dados atualizados da consulta.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")] // Define que este método responde a requisições HTTP PUT com um ID na URL
        public async Task<IActionResult> UpdateConsulta(int id, [FromBody] Consulta consulta)
        {
            // Verifica se o ID informado na URL corresponde ao ID da consulta fornecida no corpo da requisição
            if (id != consulta.IdConsulta)
            {
                return BadRequest(); // Retorna 400 (Bad Request) se os IDs não corresponderem
            }

            // Chama o serviço para atualizar a consulta
            await _consultaService.UpdateConsultaAsync(consulta);

            // Retorna 204 (No Content) para indicar que a atualização foi bem-sucedida, sem conteúdo no corpo da resposta
            return NoContent();
        }

        /// <summary>
        /// Exclui uma consulta pelo ID.
        /// </summary>
        /// <param name="id">O ID da consulta a ser excluída.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")] // Define que este método responde a requisições HTTP DELETE com um ID na URL
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            // Usa o serviço para verificar se a consulta existe
            var consulta = await _consultaService.GetConsultaByIdAsync(id);

            // Se a consulta não for encontrada, retorna um erro 404 (Not Found)
            if (consulta == null)
            {
                return NotFound();
            }

            // Chama o serviço para excluir a consulta
            await _consultaService.DeleteConsultaAsync(id);

            // Retorna 204 (No Content) para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }
    }
}

