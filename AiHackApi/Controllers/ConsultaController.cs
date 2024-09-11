// Importações necessárias para o funcionamento do controlador
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Consulta'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio da 'Consulta'
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para criar controladores de API
using Swashbuckle.AspNetCore.Annotations; // Usado para gerar documentação Swagger para os endpoints
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
        [SwaggerOperation(Summary = "Lista todas as consultas", Description = "Este endpoint retorna todas as consultas cadastradas no sistema.")]
        [SwaggerResponse(200, "Consultas listadas com sucesso", typeof(IEnumerable<Consulta>))]
        [HttpGet] // Define que este método responde a requisições HTTP GET
        public async Task<IEnumerable<Consulta>> GetConsultas()
        {
            // Chama o serviço para obter todas as consultas e retorna a lista
            return await _consultaService.GetAllConsultasAsync();
        }

        /// <summary>
        /// Obtém uma consulta específica.
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
        /// <param name="cpfPaciente">O CPF do paciente.</param>
        /// <param name="idMedico">O ID do médico.</param>
        /// <returns>A consulta correspondente aos parâmetros fornecidos.</returns>
        [SwaggerOperation(Summary = "Obtém uma consulta específica", Description = "Este endpoint retorna os detalhes de uma consulta com base na data, paciente e médico.")]
        [SwaggerResponse(200, "Consulta encontrada com sucesso", typeof(Consulta))]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [HttpGet("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a GET com a chave composta na URL
        public async Task<ActionResult<Consulta>> GetConsultaById(
            [SwaggerParameter(Description = "Data e hora da consulta")][FromRoute] DateTime dataHoraConsulta,
            [SwaggerParameter(Description = "CPF do paciente")][FromRoute] string cpfPaciente,
            [SwaggerParameter(Description = "ID do médico")][FromRoute] int idMedico)
        {
            // Usa o serviço para obter uma consulta específica pela chave composta
            var consulta = await _consultaService.GetConsultaByIdAsync(dataHoraConsulta, cpfPaciente, idMedico);

            // Se a consulta não for encontrada, retorna um erro 404 (Not Found)
            if (consulta == null)
            {
                return NotFound(new { message = "Consulta não encontrada." });
            }

            // Retorna a consulta encontrada com status 200 (OK)
            return Ok(consulta);
        }

        /// <summary>
        /// Cadastra uma nova consulta.
        /// </summary>
        /// <param name="consulta">Objeto com os dados da consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        [SwaggerOperation(Summary = "Cadastra uma nova consulta", Description = "Este endpoint cria uma nova consulta com base nos dados fornecidos.")]
        [SwaggerResponse(201, "Consulta criada com sucesso", typeof(Consulta))]
        [HttpPost] // Define que este método responde a requisições HTTP POST para criar uma nova consulta
        public async Task<ActionResult> CreateConsulta([FromBody] Consulta consulta)
        {
            // Chama o serviço para criar a nova consulta
            await _consultaService.CreateConsultaAsync(consulta);

            // Retorna 201 (Created) e a URL para acessar a consulta recém-criada
            return CreatedAtAction(nameof(GetConsultaById), new
            {
                dataHoraConsulta = consulta.DataHoraConsulta,
                cpfPaciente = consulta.CpfPaciente,
                idMedico = consulta.TbMedicosIdMedico
            }, consulta);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta a ser atualizada.</param>
        /// <param name="cpfPaciente">O CPF do paciente a ser atualizado.</param>
        /// <param name="idMedico">O ID do médico da consulta a ser atualizada.</param>
        /// <param name="consulta">Objeto com os dados atualizados da consulta.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza uma consulta existente", Description = "Este endpoint atualiza as informações de uma consulta já existente.")]
        [SwaggerResponse(204, "Consulta atualizada com sucesso")]
        [SwaggerResponse(400, "Os parâmetros fornecidos não correspondem")]
        [HttpPut("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a HTTP PUT com a chave composta na URL
        public async Task<IActionResult> UpdateConsulta(
            [SwaggerParameter(Description = "Data e hora da consulta a ser atualizada")][FromRoute] DateTime dataHoraConsulta,
            [SwaggerParameter(Description = "CPF do paciente a ser atualizado")][FromRoute] string cpfPaciente,
            [SwaggerParameter(Description = "ID do médico a ser atualizado")][FromRoute] int idMedico,
            [FromBody] Consulta consulta)
        {
            // Verifica se os parâmetros fornecidos na URL correspondem aos dados da consulta fornecida no corpo da requisição
            if (dataHoraConsulta != consulta.DataHoraConsulta || cpfPaciente != consulta.CpfPaciente || idMedico != consulta.TbMedicosIdMedico)
            {
                return BadRequest(new { message = "Os parâmetros fornecidos não correspondem." }); // Retorna 400 (Bad Request) se os parâmetros não corresponderem
            }

            // Chama o serviço para atualizar a consulta
            await _consultaService.UpdateConsultaAsync(consulta);

            // Retorna 204 (No Content) para indicar que a atualização foi bem-sucedida, sem conteúdo no corpo da resposta
            return NoContent();
        }

        /// <summary>
        /// Exclui uma consulta pela chave composta.
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta a ser excluída.</param>
        /// <param name="cpfPaciente">O CPF do paciente da consulta a ser excluída.</param>
        /// <param name="idMedico">O ID do médico da consulta a ser excluída.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui uma consulta", Description = "Este endpoint exclui uma consulta com base na data, paciente e médico.")]
        [SwaggerResponse(204, "Consulta excluída com sucesso")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [HttpDelete("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a HTTP DELETE com a chave composta na URL
        public async Task<IActionResult> DeleteConsulta(
            [SwaggerParameter(Description = "Data e hora da consulta a ser excluída")][FromRoute] DateTime dataHoraConsulta,
            [SwaggerParameter(Description = "CPF do paciente da consulta a ser excluída")][FromRoute] string cpfPaciente,
            [SwaggerParameter(Description = "ID do médico da consulta a ser excluída")][FromRoute] int idMedico)
        {
            // Usa o serviço para verificar se a consulta existe
            var consulta = await _consultaService.GetConsultaByIdAsync(dataHoraConsulta, cpfPaciente, idMedico);

            // Se a consulta não for encontrada, retorna um erro 404 (Not Found)
            if (consulta == null)
            {
                return NotFound(new { message = "Consulta não encontrada." });
            }

            // Chama o serviço para excluir a consulta
            await _consultaService.DeleteConsultaAsync(dataHoraConsulta, cpfPaciente, idMedico);

            // Retorna 204 (No Content) para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }
    }
}

