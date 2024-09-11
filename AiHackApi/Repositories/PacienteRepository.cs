using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa as classes de modelo
using Microsoft.EntityFrameworkCore; // Importa funcionalidades do Entity Framework Core
using System.Collections.Generic; // Importa as funcionalidades relacionadas a coleções genéricas
using System.Threading.Tasks; // Importa o suporte para operações assíncronas

namespace AiHackApi.Repositories
{
    // Implementação do repositório de Pacientes que implementa a interface IPacienteRepository
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context; // Instância do contexto do banco de dados

        // Construtor que recebe o contexto e o atribui ao campo _context
        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para adicionar um novo paciente ao banco de dados de forma assíncrona
        public async Task<Paciente> AdicionarAsync(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente)); // Verifica se o paciente é nulo
            }

            await _context.Pacientes.AddAsync(paciente); // Adiciona o paciente ao contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return paciente; // Retorna o paciente adicionado
        }

        // Método para buscar um paciente pelo ID de forma assíncrona
        public async Task<Paciente> ObterPorIdAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id); // Busca o paciente pelo ID
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {id} não encontrado."); // Lança exceção se o paciente não for encontrado
            }
            return paciente; // Retorna o paciente encontrado
        }

        // Método para obter todos os pacientes de forma assíncrona
        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            var pacientes = await _context.Pacientes.AsNoTracking().ToListAsync(); // Busca todos os pacientes sem rastreamento

            // Retorna uma lista vazia, ao invés de lançar exceção, se nenhum paciente for encontrado
            return pacientes ?? new List<Paciente>();
        }

        // Método para atualizar um paciente existente de forma assíncrona
        public async Task<Paciente> AtualizarAsync(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente)); // Verifica se o paciente é nulo
            }

            var existingPaciente = await ObterPorIdAsync(paciente.IdPaciente); // Verifica se o paciente existe
            if (existingPaciente == null)
            {
                throw new NotFoundException($"Paciente com ID {paciente.IdPaciente} não encontrado.");
            }

            _context.Entry(paciente).State = EntityState.Modified; // Marca a entidade como modificada
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return paciente; // Retorna o paciente atualizado
        }

        // Método para excluir um paciente de forma assíncrona
        public async Task<bool> DeletarAsync(int id)
        {
            var paciente = await ObterPorIdAsync(id); // Busca o paciente pelo ID
            if (paciente == null)
            {
                return false; // Retorna false se o paciente não for encontrado
            }

            _context.Pacientes.Remove(paciente); // Remove o paciente do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return true; // Retorna true indicando que a exclusão foi bem-sucedida
        }
    }
}

