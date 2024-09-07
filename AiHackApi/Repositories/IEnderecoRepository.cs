using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public interface IEnderecoRepository
    {
        Task<Endereco?> ObterPorIdAsync(int id);
        Task<IEnumerable<Endereco>> ObterTodosAsync();
        Task<Endereco> AdicionarAsync(Endereco endereco);
        Task<Endereco> AtualizarAsync(Endereco endereco);
        Task<bool> DeletarAsync(int id);
    }
}
