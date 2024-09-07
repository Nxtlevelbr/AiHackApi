using AiHackApi.Models;
using AiHackApi.Repositories;


namespace AiHackApi.Services
{
    public class EspecialidadeService : IEspecialidadeService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeService(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<Especialidade> CreateEspecialidadeAsync(Especialidade especialidade)
        {
            return await _especialidadeRepository.AddAsync(especialidade);
        }

        public async Task<Especialidade> GetEspecialidadeByIdAsync(int id)
        {
            return await _especialidadeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Especialidade>> GetAllEspecialidadesAsync()
        {
            return await _especialidadeRepository.GetAllAsync();
        }

        public async Task<Especialidade> UpdateEspecialidadeAsync(Especialidade especialidade)
        {
            return await _especialidadeRepository.UpdateAsync(especialidade);
        }

        public async Task<bool> DeleteEspecialidadeAsync(int id)
        {
            return await _especialidadeRepository.DeleteAsync(id);
        }
    }
}
