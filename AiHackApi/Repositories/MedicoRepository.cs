using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;

public class MedicoRepository : IMedicoRepository
{
    private readonly ApplicationDbContext _context;

    public MedicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Medico> CreateMedicoAsync(Medico medico)
    {
        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();
        return medico;
    }

    public async Task<Medico> GetMedicoByIdAsync(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        return medico ?? throw new NotFoundException("Médico não encontrado");
    }

    public async Task<IEnumerable<Medico>> GetAllMedicosAsync()
    {
        return await _context.Medicos
                             .Include(m => m.Especialidade)
                             .Include(m => m.Contato)
                             .Include(m => m.Endereco)
                             .AsNoTracking()
                             .ToListAsync();
    }

    public async Task<Medico> UpdateMedicoAsync(Medico medico)
    {
        _context.Entry(medico).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return medico;
    }

    public async Task<bool> DeleteMedicoAsync(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return false;

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();
        return true;
    }
}

