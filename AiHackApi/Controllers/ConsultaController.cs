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
        /// Obtém uma consulta específica com base na chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
        /// <param name="cpfPaciente">O CPF do paciente.</param>
        /// <param name="idMedico">O ID do médico.</param>
        /// <returns>A consulta correspondente aos parâmetros fornecidos.</returns>
        [HttpGet("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a GET com a chave composta na URL
        public async Task<ActionResult<Consulta>> GetConsultaById(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
        {
            // Usa o serviço para obter uma consulta específica pela chave composta
            var consulta = await _consultaService.GetConsultaByIdAsync(dataHoraConsulta, cpfPaciente, idMedico);

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
        [HttpPut("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a HTTP PUT com a chave composta na URL
        public async Task<IActionResult> UpdateConsulta(DateTime dataHoraConsulta, string cpfPaciente, int idMedico, [FromBody] Consulta consulta)
        {
            // Verifica se os parâmetros fornecidos na URL correspondem aos dados da consulta fornecida no corpo da requisição
            if (dataHoraConsulta != consulta.DataHoraConsulta || cpfPaciente != consulta.CpfPaciente || idMedico != consulta.TbMedicosIdMedico)
            {
                return BadRequest(); // Retorna 400 (Bad Request) se os parâmetros não corresponderem
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
        [HttpDelete("{dataHoraConsulta}/{cpfPaciente}/{idMedico}")] // Define que este método responde a HTTP DELETE com a chave composta na URL
        public async Task<IActionResult> DeleteConsulta(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
        {
            // Usa o serviço para verificar se a consulta existe
            var consulta = await _consultaService.GetConsultaByIdAsync(dataHoraConsulta, cpfPaciente, idMedico);

            // Se a consulta não for encontrada, retorna um erro 404 (Not Found)
            if (consulta == null)
            {
                return NotFound();
            }

            // Chama o serviço para excluir a consulta
            await _consultaService.DeleteConsultaAsync(dataHoraConsulta, cpfPaciente, idMedico);

            // Retorna 204 (No Content) para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }
    }
}
