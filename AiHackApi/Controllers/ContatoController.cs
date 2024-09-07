using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using AiHackApi.Models;
using AiHackApi.Services;

namespace AiHackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [SwaggerOperation(Summary = "Lista todos os contatos", Description = "Este endpoint retorna todos os contatos cadastrados no sistema.")]
        [SwaggerResponse(200, "Contatos listados com sucesso", typeof(IEnumerable<Contato>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            var contatos = await _contatoService.ObterTodosContatosAsync();
            return Ok(contatos);
        }

        [SwaggerOperation(Summary = "Obtém um contato específico", Description = "Este endpoint retorna os detalhes de um contato com base no ID fornecido.")]
        [SwaggerResponse(200, "Contato encontrado com sucesso", typeof(Contato))]
        [SwaggerResponse(404, "Contato não encontrado")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _contatoService.ObterContatoPorIdAsync(id);
            if (contato == null)
            {
                return NotFound(new { message = "Contato não encontrado." });
            }
            return Ok(contato);
        }

        [SwaggerOperation(Summary = "Cadastra um novo contato", Description = "Este endpoint cria um novo contato no sistema.")]
        [SwaggerResponse(201, "Contato criado com sucesso", typeof(Contato))]
        [HttpPost]
        public async Task<ActionResult<Contato>> CriarContato([FromBody] Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoContato = await _contatoService.CriarContatoAsync(contato);
            return CreatedAtAction(nameof(GetContato), new { id = novoContato.IdContato }, novoContato);
        }

        [SwaggerOperation(Summary = "Atualiza um contato", Description = "Este endpoint atualiza os detalhes de um contato com base no ID fornecido.")]
        [SwaggerResponse(204, "Contato atualizado com sucesso")]
        [SwaggerResponse(404, "Contato não encontrado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarContato(int id, [FromBody] Contato contatoAtualizado)
        {
            if (id != contatoAtualizado.IdContato)
            {
                return BadRequest(new { message = "O ID do contato não corresponde." });
            }

            if (!ModelState.IsValid) // Adicionando validação aqui também
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _contatoService.AtualizarContatoAsync(contatoAtualizado);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Contato não encontrado." });
            }
        }

        [SwaggerOperation(Summary = "Exclui um contato", Description = "Este endpoint exclui um contato com base no ID fornecido.")]
        [SwaggerResponse(204, "Contato excluído com sucesso")]
        [SwaggerResponse(404, "Contato não encontrado")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarContato(int id)
        {
            try
            {
                await _contatoService.DeletarContatoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Contato não encontrado." });
            }
        }
    }
}


