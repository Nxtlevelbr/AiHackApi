using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementação de CriarConsultaAsync
        public async Task<Consulta> CriarConsultaAsync(Consulta consulta)
        {
            if (consulta == null)
            {
                throw new ArgumentNullException(nameof(consulta));
            }

            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        // Implementação de ObterPorIdAsync
        public async Task<Consulta> ObterPorIdAsync(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                throw new NotFoundException($"Consulta com ID {id} não encontrada.");
            }
            return consulta;
        }

        // Implementação de ObterTodosAsync
        public async Task<IEnumerable<Consulta>> ObterTodosAsync()
        {
            var consultas = await _context.Consultas.AsNoTracking().ToListAsync();
            if (consultas == null || !consultas.Any())
            {
                throw new NotFoundException("Nenhuma consulta encontrada.");
            }

            return consultas;
        }

        // Implementação de AtualizarConsultaAsync
        public async Task<Consulta> AtualizarConsultaAsync(Consulta consulta)
        {
            if (consulta == null)
            {
                throw new ArgumentNullException(nameof(consulta));
            }

            var existingConsulta = await ObterPorIdAsync(consulta.IdConsulta);
            if (existingConsulta == null)
            {
                throw new NotFoundException($"Consulta com ID {consulta.IdConsulta} não encontrada.");
            }

            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return consulta;
        }

        // Implementação de DeletarConsultaAsync
        public async Task<bool> DeletarConsultaAsync(int id)
        {
            var consulta = await ObterPorIdAsync(id);
            if (consulta == null)
            {
                throw new NotFoundException($"Consulta com ID {id} não encontrada.");
            }

            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
