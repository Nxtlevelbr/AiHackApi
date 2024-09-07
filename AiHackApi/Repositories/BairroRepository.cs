using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly ApplicationDbContext _context;

        public BairroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementação de AdicionarAsync
        public async Task<Bairro> AddAsync(Bairro bairro)
        {
            if (bairro == null)
            {
                throw new ArgumentNullException(nameof(bairro));
            }

            await _context.Bairros.AddAsync(bairro);
            await _context.SaveChangesAsync();
            return bairro;
        }

        // Implementação de ObterPorIdAsync
        public async Task<Bairro> GetByIdAsync(int id)
        {
            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro == null)
            {
                throw new NotFoundException($"Bairro com ID {id} não encontrado.");
            }
            return bairro;
        }

        // Implementação de ObterTodosAsync
        public async Task<IEnumerable<Bairro>> GetAllAsync()
        {
            var bairros = await _context.Bairros.AsNoTracking().ToListAsync();
            if (bairros == null || !bairros.Any())
            {
                throw new NotFoundException("Nenhum bairro encontrado.");
            }

            return bairros;
        }

        // Implementação de AtualizarAsync
        public async Task<Bairro> UpdateAsync(Bairro bairro)
        {
            if (bairro == null)
            {
                throw new ArgumentNullException(nameof(bairro));
            }

            var existingBairro = await GetByIdAsync(bairro.IdBairro);
            if (existingBairro == null)
            {
                throw new NotFoundException($"Bairro com ID {bairro.IdBairro} não encontrado.");
            }

            _context.Entry(bairro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return bairro;
        }

        // Implementação de DeletarAsync
        public async Task<bool> DeleteAsync(int id)
        {
            var bairro = await GetByIdAsync(id);
            if (bairro == null)
            {
                throw new NotFoundException($"Bairro com ID {id} não encontrado.");
            }

            _context.Bairros.Remove(bairro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

