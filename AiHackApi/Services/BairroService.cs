using AiHackApi.Models;
using AiHackApi.Repositories;


namespace AiHackApi.Services
{
    public class BairroService : IBairroService
    {
        private readonly IBairroRepository _bairroRepository;

        public BairroService(IBairroRepository bairroRepository)
        {
            _bairroRepository = bairroRepository;
        }

        public async Task<Bairro> CreateBairroAsync(Bairro bairro)
        {
            return await _bairroRepository.AddAsync(bairro);
        }

        public async Task<Bairro> GetBairroByIdAsync(int id)
        {
            return await _bairroRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Bairro>> GetAllBairrosAsync()
        {
            return await _bairroRepository.GetAllAsync();
        }

        public async Task<Bairro> UpdateBairroAsync(Bairro bairro)
        {
            return await _bairroRepository.UpdateAsync(bairro);
        }

        public async Task<bool> DeleteBairroAsync(int id)
        {
            return await _bairroRepository.DeleteAsync(id);
        }
    }
}

