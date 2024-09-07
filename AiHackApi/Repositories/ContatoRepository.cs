
using AiHackApi.Data;
using AiHackApi.Models;
using AiHackApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
public class ContatoRepository : IContatoRepository
{
    private readonly ApplicationDbContext _context;

    public ContatoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Contato> ObterPorIdAsync(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null)
        {
            throw new KeyNotFoundException("Contato não encontrado");
        }
        return contato;
    }

    public async Task<IEnumerable<Contato>> ObterTodosAsync()
    {
        return await _context.Contatos.AsNoTracking().ToListAsync();
    }

    public async Task<Contato> AdicionarAsync(Contato contato)
    {
        await _context.Contatos.AddAsync(contato);
        await _context.SaveChangesAsync();
        return contato;
    }

    public async Task<Contato> AtualizarAsync(Contato contato)
    {
        _context.Entry(contato).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return contato;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var contato = await ObterPorIdAsync(id);
        if (contato == null) return false;

        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
        return true;
    }
}

