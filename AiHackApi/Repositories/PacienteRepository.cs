using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementação de AdicionarAsync com retorno Task<Paciente>
        public async Task<Paciente> AdicionarAsync(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        // Implementação de ObterPorIdAsync com retorno Task<Paciente>
        public async Task<Paciente> ObterPorIdAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {id} não encontrado.");
            }
            return paciente;
        }

        // Implementação de ObterTodosAsync com retorno Task<IEnumerable<Paciente>>
        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            var pacientes = await _context.Pacientes.AsNoTracking().ToListAsync();
            if (pacientes == null || !pacientes.Any())
            {
                throw new NotFoundException("Nenhum paciente encontrado.");
            }

            return pacientes;
        }

        // Implementação de AtualizarAsync com retorno Task<Paciente>
        public async Task<Paciente> AtualizarAsync(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            var existingPaciente = await ObterPorIdAsync(paciente.IdPaciente);
            if (existingPaciente == null)
            {
                throw new NotFoundException($"Paciente com ID {paciente.IdPaciente} não encontrado.");
            }

            _context.Entry(paciente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return paciente;
        }

        // Implementação de DeletarAsync com retorno Task<bool>
        public async Task<bool> DeletarAsync(int id)
        {
            var paciente = await ObterPorIdAsync(id);
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {id} não encontrado.");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

