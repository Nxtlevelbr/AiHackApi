using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public interface IBairroRepository
    {
        Task<Bairro> AddAsync(Bairro bairro);
        Task<Bairro> GetByIdAsync(int id);
        Task<IEnumerable<Bairro>> GetAllAsync();
        Task<Bairro> UpdateAsync(Bairro bairro);
        Task<bool> DeleteAsync(int id);
    }
}
