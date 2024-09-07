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
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        // Obter paciente por ID
        public async Task<PacienteDto> GetPacienteByIdAsync(int id)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            return new PacienteDto
            {
                IdPaciente = paciente.IdPaciente,
                NomePaciente = paciente.NomePaciente,
                CPF = paciente.CPF
            };
        }

        // Obter todos os pacientes
        public async Task<IEnumerable<PacienteDto>> GetAllPacientesAsync()
        {
            var pacientes = await _pacienteRepository.ObterTodosAsync();
            return pacientes.Select(p => new PacienteDto
            {
                IdPaciente = p.IdPaciente,
                NomePaciente = p.NomePaciente,
                CPF = p.CPF
            });
        }

        // Criar novo paciente
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

        // Atualizar paciente
        public async Task UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = await _pacienteRepository.ObterPorIdAsync(pacienteDto.IdPaciente);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            paciente.NomePaciente = pacienteDto.NomePaciente;
            paciente.CPF = pacienteDto.CPF;

            await _pacienteRepository.AtualizarAsync(paciente);
        }

        // Deletar paciente
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

