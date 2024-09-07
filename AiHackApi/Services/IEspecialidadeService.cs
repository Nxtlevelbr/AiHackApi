using AiHackApi.Models;
using AiHackApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public interface IEspecialidadeService
    {
        Task<Especialidade> CreateEspecialidadeAsync(Especialidade especialidade);
        Task<Especialidade> GetEspecialidadeByIdAsync(int id);
        Task<IEnumerable<Especialidade>> GetAllEspecialidadesAsync();
        Task<Especialidade> UpdateEspecialidadeAsync(Especialidade especialidade);
        Task<bool> DeleteEspecialidadeAsync(int id);
    }
}

