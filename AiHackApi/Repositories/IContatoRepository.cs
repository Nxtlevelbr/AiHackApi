
using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public interface IContatoRepository
    {
        Task<Contato> AdicionarAsync(Contato contato);
        Task<Contato> ObterPorIdAsync(int id);
        Task<IEnumerable<Contato>> ObterTodosAsync();
        Task<Contato> AtualizarAsync(Contato contato);
        Task<bool> DeletarAsync(int id);
    }
}
