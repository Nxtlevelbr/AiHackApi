using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public interface IConsultaService
    {
        Task<IEnumerable<Consulta>> ObterTodasConsultasAsync();
        Task<Consulta?> ObterConsultaPorIdAsync(int id);
        Task<Consulta> CriarConsultaAsync(Consulta consulta);
        Task<Consulta> AtualizarConsultaAsync(Consulta consulta); // Este método é o correto
        Task<bool> DeletarConsultaAsync(int id);
    }
}
