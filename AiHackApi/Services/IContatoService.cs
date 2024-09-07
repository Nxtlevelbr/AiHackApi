using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public interface IContatoService
    {
        Task<Contato> CriarContatoAsync(Contato contato);
        Task<Contato> ObterContatoPorIdAsync(int id);
        Task<IEnumerable<Contato>> ObterTodosContatosAsync();
        Task<Contato> AtualizarContatoAsync(Contato contato);
        Task<bool> DeletarContatoAsync(int id);
    }
}
