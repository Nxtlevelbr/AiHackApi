using AiHackApi.Models;
using AiHackApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;

        public ConsultaService(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<IEnumerable<Consulta>> ObterTodasConsultasAsync()
        {
            return await _consultaRepository.ObterTodosAsync();
        }

        public async Task<Consulta?> ObterConsultaPorIdAsync(int id)
        {
            return await _consultaRepository.ObterPorIdAsync(id);
        }

        public async Task<Consulta> CriarConsultaAsync(Consulta consulta)
        {
            return await _consultaRepository.CriarConsultaAsync(consulta);
        }

        public async Task<Consulta> AtualizarConsultaAsync(Consulta consulta)
        {
            return await _consultaRepository.AtualizarConsultaAsync(consulta);
        }

        public async Task<bool> DeletarConsultaAsync(int id)
        {
            return await _consultaRepository.DeletarConsultaAsync(id);
        }
    }
}

