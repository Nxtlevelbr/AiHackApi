
using AiHackApi.Models;
using AiHackApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task<Contato> CriarContatoAsync(Contato contato)
        {
            return await _contatoRepository.AdicionarAsync(contato);
        }

        public async Task<Contato> ObterContatoPorIdAsync(int id)
        {
            return await _contatoRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Contato>> ObterTodosContatosAsync()
        {
            var contatos = await _contatoRepository.ObterTodosAsync();
            return contatos.ToList(); // Conversão explícita de IEnumerable para List, se necessário
        }

        public async Task<Contato> AtualizarContatoAsync(Contato contato)
        {
            return await _contatoRepository.AtualizarAsync(contato);
        }

        public async Task<bool> DeletarContatoAsync(int id)
        {
            return await _contatoRepository.DeletarAsync(id);
        }
    }
}
