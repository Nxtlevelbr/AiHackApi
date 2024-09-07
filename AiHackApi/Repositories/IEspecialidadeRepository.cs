using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public interface IEspecialidadeRepository
    {
        Task<Especialidade> AddAsync(Especialidade especialidade);
        Task<Especialidade> GetByIdAsync(int id);
        Task<IEnumerable<Especialidade>> GetAllAsync();
        Task<Especialidade> UpdateAsync(Especialidade especialidade);
        Task<bool> DeleteAsync(int id);
    }
}
