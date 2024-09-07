using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationDbContext _context;

        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Endereco?> ObterPorIdAsync(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                throw new KeyNotFoundException("Endereço não encontrado");
            }
            return endereco;
        }

        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            var enderecos = await _context.Enderecos.ToListAsync();
            return enderecos ?? new List<Endereco>();
        }

        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        // Aqui está a correção
        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            _context.Entry(endereco).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return endereco; // Retorna o próprio objeto atualizado
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var endereco = await ObterPorIdAsync(id);
            if (endereco == null) return false;

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

