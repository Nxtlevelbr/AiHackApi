// Importações necessárias para o funcionamento do controlador
using AiHackApi.DTOs; // DTOs (Data Transfer Objects) usados para transferência de dados entre o cliente e a API
using AiHackApi.Models; // Modelos de entidades da aplicação
using AiHackApi.Services; // Serviços que contêm a lógica de negócio
using Microsoft.AspNetCore.Mvc; // Bibliotecas essenciais para criar APIs em ASP.NET Core
using System.Collections.Generic; // Para trabalhar com listas genéricas
using System.Threading.Tasks; // Para operações assíncronas

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para os endpoints (ex: api/Paciente)
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService; // Campo privado para o serviço de pacientes

        // Construtor que injeta o serviço de paciente
        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Lista todos os pacientes cadastrados.
        /// </summary>
        /// <returns>Uma lista de objetos PacienteDto.</returns>
        [HttpGet] // Método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientes()
        {
            // Chama o serviço para obter todos os pacientes
            var pacientes = await _pacienteService.GetAllPacientesAsync();

            // Verifica se há pacientes e retorna 204 No Content se a lista estiver vazia
            if (pacientes == null || !pacientes.Any())
            {
                return NoContent(); // Retorna 204 No Content se não houver pacientes
            }

            return Ok(pacientes); // Retorna 200 OK com a lista de pacientes
        }

        /// <summary>
        /// Obtém um paciente específico pelo CPF.
        /// </summary>
        /// <param name="cpf">O CPF do paciente.</param>
        /// <returns>O objeto PacienteDto correspondente ao CPF fornecido.</returns>
        [HttpGet("{cpf}")] // Método responde a GET com o CPF na URL (ex: api/Paciente/12345678900)
        public async Task<ActionResult<PacienteDto>> GetPacienteByCpf(string cpf)
        {
            // Chama o serviço para obter o paciente pelo CPF
            var paciente = await _pacienteService.GetPacienteByCpfAsync(cpf);

            // Se o paciente não for encontrado, retorna 404
            if (paciente == null)
            {
                return NotFound(new { message = "Paciente não encontrado." }); // Retorna 404 Not Found com mensagem
            }

            return Ok(paciente); // Retorna 200 OK com os dados do paciente
        }

        /// <summary>
        /// Cadastra um novo paciente.
        /// </summary>
        /// <param name="pacienteDto">Objeto com os dados do paciente a ser criado.</param>
        /// <returns>O paciente recém-criado.</returns>
        [HttpPost] // Método responde a requisições HTTP POST para criação de recursos
        public async Task<ActionResult> CreatePaciente([FromBody] PacienteDto pacienteDto)
        {
            // Chama o serviço para criar o novo paciente
            await _pacienteService.CreatePacienteAsync(pacienteDto);

            // Retorna 201 Created com a URL para acessar o paciente recém-criado
            return CreatedAtAction(nameof(GetPacienteByCpf), new { cpf = pacienteDto.CPF }, pacienteDto);
        }

        /// <summary>
        /// Atualiza os dados de um paciente existente.
        /// </summary>
        /// <param name="cpf">O CPF do paciente a ser atualizado.</param>
        /// <param name="pacienteDto">Objeto com os dados atualizados do paciente.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [HttpPut("{cpf}")] // Método responde a HTTP PUT com um CPF na URL
        public async Task<IActionResult> UpdatePaciente(string cpf, [FromBody] PacienteDto pacienteDto)
        {
            // Verifica se o CPF da URL corresponde ao CPF do objeto paciente
            if (cpf != pacienteDto.CPF)
            {
                return BadRequest(new { message = "O CPF informado não corresponde ao paciente." }); // Retorna 400 Bad Request se os CPFs não coincidirem
            }

            // Chama o serviço para atualizar o paciente
            await _pacienteService.UpdatePacienteAsync(pacienteDto);

            return NoContent(); // Retorna 204 No Content se a atualização for bem-sucedida
        }

        /// <summary>
        /// Exclui um paciente pelo CPF.
        /// </summary>
        /// <param name="cpf">O CPF do paciente a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{cpf}")] // Método responde a HTTP DELETE com um CPF na URL
        public async Task<IActionResult> DeletePaciente(string cpf)
        {
            // Chama o serviço para verificar se o paciente existe
            var paciente = await _pacienteService.GetPacienteByCpfAsync(cpf);

            // Se o paciente não for encontrado, retorna 404
            if (paciente == null)
            {
                return NotFound(new { message = "Paciente não encontrado." }); // Retorna 404 Not Found se o paciente não existir
            }

            // Chama o serviço para excluir o paciente
            await _pacienteService.DeletePacienteAsync(cpf);

            return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
        }
    }
}
