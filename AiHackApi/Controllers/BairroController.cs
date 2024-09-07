using AiHackApi.Models;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class BairroController : ControllerBase
{
    private readonly IBairroService _bairroService;

    public BairroController(IBairroService bairroService)
    {
        _bairroService = bairroService;
    }

    [SwaggerOperation(Summary = "Lista todos os bairros", Description = "Este endpoint retorna uma lista completa de todos os bairros cadastrados.")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bairro>>> GetBairros()
    {
        var bairros = await _bairroService.GetAllBairrosAsync();
        return Ok(bairros);
    }

    [SwaggerOperation(Summary = "Obtém um bairro específico", Description = "Este endpoint retorna os detalhes de um bairro específico com base no ID fornecido.")]
    [SwaggerResponse(200, "Bairro encontrado com sucesso", typeof(Bairro))]
    [SwaggerResponse(404, "Bairro não encontrado")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Bairro>> GetBairro(int id)
    {
        var bairro = await _bairroService.GetBairroByIdAsync(id);
        if (bairro == null)
        {
            return NotFound(new { message = "Bairro não encontrado." });
        }
        return Ok(bairro);
    }

    [SwaggerOperation(Summary = "Cadastra um novo bairro", Description = "Este endpoint cria um novo bairro com base nas informações fornecidas.")]
    [SwaggerResponse(201, "Bairro criado com sucesso")]
    [HttpPost]
    public async Task<ActionResult> AddBairro([FromBody] Bairro bairro)
    {
        var bairroCriado = await _bairroService.CreateBairroAsync(bairro);
        return CreatedAtAction(nameof(GetBairro), new { id = bairroCriado.IdBairro }, bairroCriado);
    }

    [SwaggerOperation(Summary = "Atualiza um bairro existente", Description = "Este endpoint atualiza as informações de um bairro com base no ID fornecido.")]
    [SwaggerResponse(204, "Bairro atualizado com sucesso")]
    [SwaggerResponse(404, "Bairro não encontrado")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBairro(int id, [FromBody] Bairro bairro)
    {
        if (id != bairro.IdBairro)
        {
            return BadRequest(new { message = "O ID do bairro não corresponde." });
        }

        var bairroAtualizado = await _bairroService.UpdateBairroAsync(bairro);
        if (bairroAtualizado == null)
        {
            return NotFound(new { message = "Bairro não encontrado." });
        }

        return NoContent();
    }

    [SwaggerOperation(Summary = "Exclui um bairro", Description = "Este endpoint exclui um bairro com base no ID fornecido.")]
    [SwaggerResponse(204, "Bairro excluído com sucesso")]
    [SwaggerResponse(404, "Bairro não encontrado")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBairro(int id)
    {
        var success = await _bairroService.DeleteBairroAsync(id);
        if (!success)
        {
            return NotFound(new { message = "Bairro não encontrado." });
        }
        return NoContent();
    }
}

