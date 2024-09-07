using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public interface IBairroService
    {
        Task<Bairro> CreateBairroAsync(Bairro bairro);
        Task<Bairro> GetBairroByIdAsync(int id);
        Task<IEnumerable<Bairro>> GetAllBairrosAsync();
        Task<Bairro> UpdateBairroAsync(Bairro bairro);
        Task<bool> DeleteBairroAsync(int id);
    }
}
