// Importações necessárias para o funcionamento do controlador
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para criar controladores de API
using Swashbuckle.AspNetCore.Annotations; // Usado para gerar documentação Swagger para os endpoints
using System.Collections.Generic; // Usado para trabalhar com listas genéricas (IEnumerable)
using System.Threading.Tasks; // Usado para trabalhar com operações assíncronas (async/await)
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Contato'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio de 'Contato'

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para acessar os endpoints deste controlador (ex: api/Contato)
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService; // Campo privado para injeção de dependência do serviço de contatos

        // Construtor que injeta o serviço de contatos
        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        /// <summary>
        /// Lista todos os contatos cadastrados.
        /// </summary>
        /// <returns>Uma lista de contatos.</returns>
        [SwaggerOperation(Summary = "Lista todos os contatos", Description = "Este endpoint retorna todos os contatos cadastrados no sistema.")]
        [SwaggerResponse(200, "Contatos listados com sucesso", typeof(IEnumerable<Contato>))] // Retorna 200 (OK) em caso de sucesso
        [HttpGet] // Define que este método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            // Chama o serviço para obter todos os contatos e retorna a lista
            var contatos = await _contatoService.ObterTodosContatosAsync();
            return Ok(contatos); // Retorna 200 (OK) com a lista de contatos
        }

        /// <summary>
        /// Obtém um contato específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato.</param>
        /// <returns>O contato correspondente ao ID informado.</returns>
        [SwaggerOperation(Summary = "Obtém um contato específico", Description = "Este endpoint retorna os detalhes de um contato com base no ID fornecido.")]
        [SwaggerResponse(200, "Contato encontrado com sucesso", typeof(Contato))] // Retorna 200 se o contato for encontrado
        [SwaggerResponse(404, "Contato não encontrado")] // Retorna 404 se o contato não for encontrado
        [HttpGet("{id}")] // Define que este método responde a requisições GET com um ID na URL (ex: api/Contato/1)
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            // Usa o serviço para obter um contato específico pelo ID
            var contato = await _contatoService.ObterContatoPorIdAsync(id);

            // Se o contato não for encontrado, retorna um erro 404 (Not Found)
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado." });
            }

            // Retorna o contato encontrado com status 200 (OK)
            return Ok(contato);
        }

        /// <summary>
        /// Cadastra um novo contato.
        /// </summary>
        /// <param name="contato">Objeto com os dados do contato a ser criado.</param>
        /// <returns>O contato recém-criado.</returns>
        [SwaggerOperation(Summary = "Cadastra um novo contato", Description = "Este endpoint cria um novo contato no sistema.")]
        [SwaggerResponse(201, "Contato criado com sucesso", typeof(Contato))] // Retorna 201 (Created) em caso de sucesso
        [HttpPost] // Define que este método responde a requisições HTTP POST para criar um novo contato
        public async Task<ActionResult<Contato>> CriarContato([FromBody] Contato contato)
        {
            // Valida o modelo do contato antes de prosseguir
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 (Bad Request) se houver erros de validação
            }

            // Chama o serviço para criar o novo contato
            var novoContato = await _contatoService.CriarContatoAsync(contato);

            // Retorna 201 (Created) e a URL para acessar o contato recém-criado
            return CreatedAtAction(nameof(GetContato), new { id = novoContato.IdContato }, novoContato);
        }

        /// <summary>
        /// Atualiza os dados de um contato existente.
        /// </summary>
        /// <param name="id">O ID do contato a ser atualizado.</param>
        /// <param name="contatoAtualizado">Objeto com os dados atualizados do contato.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza um contato", Description = "Este endpoint atualiza os detalhes de um contato com base no ID fornecido.")]
        [SwaggerResponse(204, "Contato atualizado com sucesso")] // Retorna 204 (No Content) se a atualização for bem-sucedida
        [SwaggerResponse(404, "Contato não encontrado")] // Retorna 404 se o contato não for encontrado
        [HttpPut("{id}")] // Define que este método responde a requisições HTTP PUT com um ID na URL
        public async Task<IActionResult> AtualizarContato(int id, [FromBody] Contato contatoAtualizado)
        {
            // Verifica se o ID informado na URL corresponde ao ID do contato fornecido no corpo da requisição
            if (id != contatoAtualizado.IdContato)
            {
                return BadRequest(new { message = "O ID do contato não corresponde." }); // Retorna 400 (Bad Request) se os IDs não corresponderem
            }

            // Valida o modelo atualizado antes de prosseguir
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna 400 (Bad Request) se houver erros de validação
            }

            try
            {
                // Chama o serviço para atualizar o contato
                await _contatoService.AtualizarContatoAsync(contatoAtualizado);
                return NoContent(); // Retorna 204 (No Content) se a atualização for bem-sucedida
            }
            catch (KeyNotFoundException)
            {
                // Retorna 404 (Not Found) se o contato não for encontrado
                return NotFound(new { message = "Contato não encontrado." });
            }
        }

        /// <summary>
        /// Exclui um contato pelo ID.
        /// </summary>
        /// <param name="id">O ID do contato a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui um contato", Description = "Este endpoint exclui um contato com base no ID fornecido.")]
        [SwaggerResponse(204, "Contato excluído com sucesso")] // Retorna 204 (No Content) se a exclusão for bem-sucedida
        [SwaggerResponse(404, "Contato não encontrado")] // Retorna 404 se o contato não for encontrado
        [HttpDelete("{id}")] // Define que este método responde a requisições HTTP DELETE com um ID na URL
        public async Task<IActionResult> DeletarContato(int id)
        {
            try
            {
                // Chama o serviço para excluir o contato
                await _contatoService.DeletarContatoAsync(id);
                return NoContent(); // Retorna 204 (No Content) se a exclusão for bem-sucedida
            }
            catch (KeyNotFoundException)
            {
                // Retorna 404 (Not Found) se o contato não for encontrado
                return NotFound(new { message = "Contato não encontrado." });
            }
        }
    }
}

