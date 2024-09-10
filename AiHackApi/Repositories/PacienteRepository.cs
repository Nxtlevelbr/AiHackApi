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
            // Verifica se o paciente passado é nulo, caso contrário lança exceção
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            // Adiciona o paciente ao contexto
            await _context.Pacientes.AddAsync(paciente);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return paciente; // Retorna o paciente adicionado
        }

        // Método para buscar um paciente pelo ID de forma assíncrona
        public async Task<Paciente> ObterPorIdAsync(int id)
        {
            // Busca o paciente no banco de dados
            var paciente = await _context.Pacientes.FindAsync(id);
            // Se o paciente não for encontrado, lança exceção
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {id} não encontrado.");
            }
            return paciente; // Retorna o paciente encontrado
        }

        // Método para obter todos os pacientes de forma assíncrona
        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            // Busca todos os pacientes sem rastreamento de mudanças (AsNoTracking)
            var pacientes = await _context.Pacientes.AsNoTracking().ToListAsync();
            // Verifica se a lista de pacientes está vazia ou nula
            if (pacientes == null || !pacientes.Any())
            {
                throw new NotFoundException("Nenhum paciente encontrado.");
            }

            return pacientes; // Retorna a lista de pacientes
        }

        // Método para atualizar um paciente existente de forma assíncrona
        public async Task<Paciente> AtualizarAsync(Paciente paciente)
        {
            // Verifica se o paciente passado é nulo, caso contrário lança exceção
            if (paciente == null)
            {
                throw new ArgumentNullException(nameof(paciente));
            }

            // Busca o paciente existente pelo ID
            var existingPaciente = await ObterPorIdAsync(paciente.IdPaciente);
            // Se o paciente não for encontrado, lança exceção
            if (existingPaciente == null)
            {
                throw new NotFoundException($"Paciente com ID {paciente.IdPaciente} não encontrado.");
            }

            // Marca a entidade como modificada no contexto
            _context.Entry(paciente).State = EntityState.Modified;
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return paciente; // Retorna o paciente atualizado
        }

        // Método para excluir um paciente de forma assíncrona
        public async Task<bool> DeletarAsync(int id)
        {
            // Busca o paciente pelo ID
            var paciente = await ObterPorIdAsync(id);
            // Se o paciente não for encontrado, lança exceção
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente com ID {id} não encontrado.");
            }

            // Remove o paciente do contexto
            _context.Pacientes.Remove(paciente);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return true; // Retorna true indicando que a exclusão foi bem-sucedida
        }
    }
}
