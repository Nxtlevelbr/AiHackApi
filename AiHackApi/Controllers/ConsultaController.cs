
using AiHackApi.Models;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarConsultaAsync(int id, Consulta consulta)
        {
            if (id != consulta.IdConsulta)
                return BadRequest("O ID da consulta não corresponde.");

            var consultaAtualizada = await _consultaService.AtualizarConsultaAsync(consulta);
            if (consultaAtualizada == null)
                return NotFound();

            return Ok(consultaAtualizada);
        }
    }
}
