// Importações necessárias para o funcionamento do controlador
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para controladores de API
using Swashbuckle.AspNetCore.Annotations; // Usado para gerar documentação Swagger
using System.Collections.Generic; // Para trabalhar com listas genéricas (IEnumerable)
using System.Threading.Tasks; // Para operações assíncronas (async/await)
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Endereco'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio de 'Endereco'

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que a classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para os endpoints (ex: api/Endereco)
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService; // Campo para o serviço de endereços

        // Construtor que injeta o serviço de endereços
        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        /// <summary>
        /// Lista todos os endereços cadastrados.
        /// </summary>
        /// <returns>Uma lista de endereços.</returns>
        [SwaggerOperation(Summary = "Lista todos os endereços", Description = "Este endpoint retorna todos os endereços cadastrados no sistema.")]
        [SwaggerResponse(200, "Endereços listados com sucesso", typeof(IEnumerable<Endereco>))] // 200 OK com a lista de endereços
        [HttpGet] // Método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            // Chama o serviço para obter todos os endereços e retorna a lista
            var enderecos = await _enderecoService.GetAllEnderecosAsync();
            return Ok(enderecos); // Retorna 200 OK com a lista de endereços
        }

        /// <summary>
        /// Obtém um endereço específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço.</param>
        /// <returns>O endereço correspondente ao ID informado.</returns>
        [SwaggerOperation(Summary = "Obtém um endereço específico", Description = "Este endpoint retorna os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(200, "Endereço encontrado com sucesso", typeof(Endereco))] // 200 OK se encontrado
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se não encontrado
        [HttpGet("{id}")] // Método responde a GET com um ID na URL (ex: api/Endereco/1)
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            // Usa o serviço para obter o endereço pelo ID
            var endereco = await _enderecoService.GetEnderecoByIdAsync(id);

            // Se o endereço não for encontrado, retorna 404
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }

            // Retorna 200 OK com o endereço encontrado
            return Ok(endereco);
        }

        /// <summary>
        /// Cadastra um novo endereço.
        /// </summary>
        /// <param name="endereco">Objeto com os dados do endereço a ser criado.</param>
        /// <returns>O endereço recém-criado.</returns>
        [SwaggerOperation(Summary = "Cadastra um novo endereço", Description = "Este endpoint cria um novo endereço no sistema.")]
        [SwaggerResponse(201, "Endereço criado com sucesso", typeof(Endereco))] // 201 Created se o endereço for criado
        [HttpPost] // Método responde a HTTP POST para criação
        public async Task<ActionResult<Endereco>> AddEndereco([FromBody] Endereco endereco)
        {
            // Chama o serviço para criar o novo endereço
            var enderecoCriado = await _enderecoService.CreateEnderecoAsync(endereco);

            // Retorna 201 Created com a URL para acessar o novo endereço
            return CreatedAtAction(nameof(GetEndereco), new { id = enderecoCriado.IdEndereco }, enderecoCriado);
        }

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="id">O ID do endereço a ser atualizado.</param>
        /// <param name="enderecoAtualizado">Objeto com os dados atualizados do endereço.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza um endereço", Description = "Este endpoint atualiza os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço atualizado com sucesso")] // 204 No Content se a atualização for bem-sucedida
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se o endereço não for encontrado
        [HttpPut("{id}")] // Método responde a HTTP PUT com um ID na URL
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] Endereco enderecoAtualizado)
        {
            // Verifica se o ID na URL corresponde ao ID do objeto endereço
            if (id != enderecoAtualizado.IdEndereco)
            {
                return BadRequest(new { message = "O ID do endereço não corresponde." }); // Retorna 400 Bad Request se os IDs não coincidirem
            }

            // Chama o serviço para atualizar o endereço
            var endereco = await _enderecoService.UpdateEnderecoAsync(enderecoAtualizado);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." }); // Retorna 404 se o endereço não for encontrado
            }

            // Retorna 204 No Content se a atualização for bem-sucedida
            return NoContent();
        }

        /// <summary>
        /// Exclui um endereço pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui um endereço", Description = "Este endpoint exclui um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço excluído com sucesso")] // 204 No Content se a exclusão for bem-sucedida
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se o endereço não for encontrado
        [HttpDelete("{id}")] // Método responde a HTTP DELETE com um ID na URL
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            // Chama o serviço para excluir o endereço
            var sucesso = await _enderecoService.DeleteEnderecoAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Endereço não encontrado." }); // Retorna 404 se o endereço não for encontrado
            }

            // Retorna 204 No Content se a exclusão for bem-sucedida
            return NoContent();
        }
    }
}
