using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AiHackApi.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para adicionar uma especialidade
        public async Task<Especialidade> AddAsync(Especialidade especialidade)
        {
            await _context.Especialidades.AddAsync(especialidade);
            await _context.SaveChangesAsync();
            return especialidade;
        }

        // Método para buscar uma especialidade pelo ID, garantindo que não seja nulo
        public async Task<Especialidade> GetByIdAsync(int id)
        {
            var especialidade = await _context.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                throw new Exception("Especialidade não encontrada");
            }
            return especialidade;
        }

        // Método para buscar todas as especialidades
        public async Task<IEnumerable<Especialidade>> GetAllAsync()
        {
            var especialidades = await _context.Especialidades.AsNoTracking().ToListAsync();
            if (!especialidades.Any())
            {
                throw new Exception("Nenhuma especialidade encontrada");
            }
            return especialidades;
        }

        // Método para atualizar uma especialidade
        public async Task<Especialidade> UpdateAsync(Especialidade especialidade)
        {
            var existingEspecialidade = await GetByIdAsync(especialidade.IdEspecialidade);
            _context.Entry(existingEspecialidade).CurrentValues.SetValues(especialidade);
            await _context.SaveChangesAsync();
            return especialidade;
        }

        // Método para deletar uma especialidade
        public async Task<bool> DeleteAsync(int id)
        {
            var especialidade = await GetByIdAsync(id);
            if (especialidade == null)
            {
                throw new Exception("Especialidade não encontrada para exclusão");
            }

            _context.Especialidades.Remove(especialidade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
