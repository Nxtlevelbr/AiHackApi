using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public interface IConsultaRepository
    {
        Task<Consulta> CriarConsultaAsync(Consulta consulta);
        Task<Consulta> ObterPorIdAsync(int id);
        Task<IEnumerable<Consulta>> ObterTodosAsync();
        Task<Consulta> AtualizarConsultaAsync(Consulta consulta);
        Task<bool> DeletarConsultaAsync(int id);
    }
}
