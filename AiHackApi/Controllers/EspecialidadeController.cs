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
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeService _especialidadeService;

        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        [SwaggerOperation(Summary = "Lista todas as especialidades", Description = "Este endpoint retorna todas as especialidades cadastradas no sistema.")]
        [SwaggerResponse(200, "Especialidades listadas com sucesso", typeof(IEnumerable<Especialidade>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidade>>> GetEspecialidades()
        {
            var especialidades = await _especialidadeService.GetAllEspecialidadesAsync();
            return Ok(especialidades);
        }

        [SwaggerOperation(Summary = "Obtém uma especialidade específica", Description = "Este endpoint retorna os detalhes de uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(200, "Especialidade encontrada com sucesso", typeof(Especialidade))]
        [SwaggerResponse(404, "Especialidade não encontrada")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidade>> GetEspecialidade(int id)
        {
            var especialidade = await _especialidadeService.GetEspecialidadeByIdAsync(id);
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada." });
            }
            return Ok(especialidade);
        }

        [SwaggerOperation(Summary = "Cadastra uma nova especialidade", Description = "Este endpoint cria uma nova especialidade no sistema.")]
        [SwaggerResponse(201, "Especialidade criada com sucesso", typeof(Especialidade))]
        [HttpPost]
        public async Task<ActionResult<Especialidade>> AddEspecialidade([FromBody] Especialidade especialidade)
        {
            var especialidadeCriada = await _especialidadeService.CreateEspecialidadeAsync(especialidade);
            return CreatedAtAction(nameof(GetEspecialidade), new { id = especialidadeCriada.IdEspecialidade }, especialidadeCriada);
        }

        [SwaggerOperation(Summary = "Atualiza uma especialidade", Description = "Este endpoint atualiza os detalhes de uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(204, "Especialidade atualizada com sucesso")]
        [SwaggerResponse(404, "Especialidade não encontrada")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspecialidade(int id, [FromBody] Especialidade especialidadeAtualizada)
        {
            if (id != especialidadeAtualizada.IdEspecialidade)
            {
                return BadRequest(new { message = "O ID da especialidade não corresponde." });
            }

            var especialidade = await _especialidadeService.UpdateEspecialidadeAsync(especialidadeAtualizada);
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada." });
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Exclui uma especialidade", Description = "Este endpoint exclui uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(204, "Especialidade excluída com sucesso")]
        [SwaggerResponse(404, "Especialidade não encontrada")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidade(int id)
        {
            var sucesso = await _especialidadeService.DeleteEspecialidadeAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Especialidade não encontrada." });
            }
            return NoContent();
        }
    }
}
