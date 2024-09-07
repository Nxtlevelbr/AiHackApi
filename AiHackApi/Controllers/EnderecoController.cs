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
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [SwaggerOperation(Summary = "Lista todos os endereços", Description = "Este endpoint retorna todos os endereços cadastrados no sistema.")]
        [SwaggerResponse(200, "Endereços listados com sucesso", typeof(IEnumerable<Endereco>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            var enderecos = await _enderecoService.GetAllEnderecosAsync();
            return Ok(enderecos);
        }

        [SwaggerOperation(Summary = "Obtém um endereço específico", Description = "Este endpoint retorna os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(200, "Endereço encontrado com sucesso", typeof(Endereco))]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            var endereco = await _enderecoService.GetEnderecoByIdAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }
            return Ok(endereco);
        }

        [SwaggerOperation(Summary = "Cadastra um novo endereço", Description = "Este endpoint cria um novo endereço no sistema.")]
        [SwaggerResponse(201, "Endereço criado com sucesso", typeof(Endereco))]
        [HttpPost]
        public async Task<ActionResult<Endereco>> AddEndereco([FromBody] Endereco endereco)
        {
            var enderecoCriado = await _enderecoService.CreateEnderecoAsync(endereco);
            return CreatedAtAction(nameof(GetEndereco), new { id = enderecoCriado.IdEndereco }, enderecoCriado);
        }

        [SwaggerOperation(Summary = "Atualiza um endereço", Description = "Este endpoint atualiza os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço atualizado com sucesso")]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] Endereco enderecoAtualizado)
        {
            if (id != enderecoAtualizado.IdEndereco)
            {
                return BadRequest(new { message = "O ID do endereço não corresponde." });
            }

            var endereco = await _enderecoService.UpdateEnderecoAsync(enderecoAtualizado);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Exclui um endereço", Description = "Este endpoint exclui um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço excluído com sucesso")]
        [SwaggerResponse(404, "Endereço não encontrado")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var sucesso = await _enderecoService.DeleteEnderecoAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }
            return NoContent();
        }
    }
}
