// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.DTOs: Contém os Data Transfer Objects (DTOs) usados para transferir dados entre camadas.
// AiHackApi.Models: Contém as definições dos modelos de dados, como Paciente.
// AiHackApi.Repositories: Contém as definições do repositório para acesso aos dados, como IPacienteRepository.
using AiHackApi.DTOs;
using AiHackApi.Models;
using AiHackApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public class PacienteService : IPacienteService
    {
        // Repositório utilizado para acessar e manipular dados de pacientes.
        private readonly IPacienteRepository _pacienteRepository;

        // Construtor que recebe uma instância de IPacienteRepository para acesso aos dados.
        // Isso permite a injeção de dependências e facilita o teste da classe.
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        /// <summary>
        /// Obtém um paciente específico pelo seu identificador e o retorna como um DTO.
        /// </summary>
        /// <param name="id">O identificador do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um <see cref="PacienteDto"/> correspondente ao identificador fornecido.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task<PacienteDto> GetPacienteByIdAsync(int id)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            // Converte o modelo de dados Paciente para um PacienteDto
            return new PacienteDto
            {
                IdPaciente = paciente.IdPaciente,
                NomePaciente = paciente.NomePaciente,
                CPF = paciente.CPF
            };
        }

        /// <summary>
        /// Obtém todos os pacientes e os retorna como uma coleção de DTOs.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de <see cref="PacienteDto"/> representando todos os pacientes.</returns>
        public async Task<IEnumerable<PacienteDto>> GetAllPacientesAsync()
        {
            var pacientes = await _pacienteRepository.ObterTodosAsync();
            // Converte a coleção de modelos de dados Paciente para uma coleção de PacienteDto
            return pacientes.Select(p => new PacienteDto
            {
                IdPaciente = p.IdPaciente,
                NomePaciente = p.NomePaciente,
                CPF = p.CPF
            });
        }

        /// <summary>
        /// Cria um novo paciente com base nas informações fornecidas no DTO.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> que contém as informações do paciente a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        public async Task CreatePacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = new Paciente
            {
                IdPaciente = pacienteDto.IdPaciente,
                NomePaciente = pacienteDto.NomePaciente,
                CPF = pacienteDto.CPF
            };
            await _pacienteRepository.AdicionarAsync(paciente);
        }

        /// <summary>
        /// Atualiza as informações de um paciente existente com base no DTO fornecido.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> com as informações atualizadas do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(pacienteDto.IdPaciente);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            // Atualiza as informações do paciente com base no DTO
            paciente.NomePaciente = pacienteDto.NomePaciente;
            paciente.CPF = pacienteDto.CPF;

            await _pacienteRepository.AtualizarAsync(paciente);
        }

        /// <summary>
        /// Remove um paciente do repositório pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador do paciente a ser removido.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task DeletePacienteAsync(int id)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            await _pacienteRepository.DeletarAsync(id);
        }
    }
}