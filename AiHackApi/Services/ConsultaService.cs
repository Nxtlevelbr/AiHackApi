using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ConsultaService : IConsultaService
{
    private readonly ApplicationDbContext _context;

    public ConsultaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Consulta>> GetAllConsultasAsync()
    {
        return await _context.Consultas.Include(c => c.Paciente).Include(c => c.Medico).ToListAsync();
    }

    public async Task<Consulta?> GetConsultaByIdAsync(int id)
    {
        return await _context.Consultas.Include(c => c.Paciente).Include(c => c.Medico).FirstOrDefaultAsync(c => c.IdConsulta == id);
    }

    public async Task CreateConsultaAsync(Consulta consulta)
    {
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateConsultaAsync(Consulta consulta)
    {
        _context.Entry(consulta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteConsultaAsync(int id)
    {
        var consulta = await _context.Consultas.FindAsync(id);
        if (consulta != null)
        {
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
        }
    }
}
