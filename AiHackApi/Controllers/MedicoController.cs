
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
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [SwaggerOperation(Summary = "Lista todos os médicos", Description = "Este endpoint retorna todos os médicos cadastrados no sistema.")]
        [SwaggerResponse(200, "Médicos listados com sucesso", typeof(IEnumerable<Medico>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            var medicos = await _medicoService.GetAllMedicosAsync();
            return Ok(medicos);
        }

        [SwaggerOperation(Summary = "Obtém um médico específico", Description = "Este endpoint retorna os detalhes de um médico com base no ID fornecido.")]
        [SwaggerResponse(200, "Médico encontrado com sucesso", typeof(Medico))]
        [SwaggerResponse(404, "Médico não encontrado")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _medicoService.GetMedicoByIdAsync(id);
            if (medico == null)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }
            return Ok(medico);
        }

        [SwaggerOperation(Summary = "Cadastra um novo médico", Description = "Este endpoint cria um novo médico no sistema.")]
        [SwaggerResponse(201, "Médico criado com sucesso", typeof(Medico))]
        [HttpPost]
        public async Task<ActionResult<Medico>> AddMedico([FromBody] Medico medico)
        {
            var medicoCriado = await _medicoService.CreateMedicoAsync(medico);
            return CreatedAtAction(nameof(GetMedico), new { id = medicoCriado.IdMedico }, medicoCriado);
        }

        [SwaggerOperation(Summary = "Atualiza um médico", Description = "Este endpoint atualiza os detalhes de um médico com base no ID fornecido.")]
        [SwaggerResponse(204, "Médico atualizado com sucesso")]
        [SwaggerResponse(404, "Médico não encontrado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedico(int id, [FromBody] Medico medicoAtualizado)
        {
            if (id != medicoAtualizado.IdMedico)
            {
                return BadRequest(new { message = "O ID do médico não corresponde." });
            }

            var medico = await _medicoService.UpdateMedicoAsync(medicoAtualizado);
            if (medico == null)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }

            return NoContent();
        }

        [SwaggerOperation(Summary = "Exclui um médico", Description = "Este endpoint exclui um médico com base no ID fornecido.")]
        [SwaggerResponse(204, "Médico excluído com sucesso")]
        [SwaggerResponse(404, "Médico não encontrado")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var sucesso = await _medicoService.DeleteMedicoAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }
            return NoContent();
        }
    }
}
