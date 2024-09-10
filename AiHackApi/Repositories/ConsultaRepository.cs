using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo Consulta
using Microsoft.EntityFrameworkCore; // Usado para interagir com o banco de dados
using System.Collections.Generic; // Para o uso de coleções como IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

namespace AiHackApi.Repositories
{
    // Implementa a interface IConsultaRepository
    public class ConsultaRepository : IConsultaRepository
    {
        // O campo _context permite a interação com o banco de dados via Entity Framework
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto do banco de dados
        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria uma nova consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">Objeto Consulta a ser criado.</param>
        /// <returns>A consulta recém-criada.</returns>
        public async Task<Consulta> CriarConsultaAsync(Consulta consulta)
        {
            if (consulta == null)
            {
                // Lança exceção se o objeto Consulta for nulo
                throw new ArgumentNullException(nameof(consulta));
            }

            // Adiciona a nova consulta ao contexto do banco de dados
            await _context.Consultas.AddAsync(consulta);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return consulta; // Retorna a consulta criada
        }

        /// <summary>
        /// Obtém uma consulta pelo seu ID.
        /// </summary>
        /// <param name="id">ID da consulta a ser obtida.</param>
        /// <returns>A consulta correspondente ao ID fornecido.</returns>
        public async Task<Consulta> ObterPorIdAsync(int id)
        {
            // Busca a consulta pelo ID no banco de dados
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                // Lança exceção personalizada se a consulta não for encontrada
                throw new NotFoundException($"Consulta com ID {id} não encontrada.");
            }
            return consulta; // Retorna a consulta encontrada
        }

        /// <summary>
        /// Obtém todas as consultas do banco de dados.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        public async Task<IEnumerable<Consulta>> ObterTodosAsync()
        {
            // Busca todas as consultas no banco de dados, sem rastreamento (AsNoTracking)
            var consultas = await _context.Consultas.AsNoTracking().ToListAsync();
            if (consultas == null || !consultas.Any())
            {
                // Lança exceção se nenhuma consulta for encontrada
                throw new NotFoundException("Nenhuma consulta encontrada.");
            }

            return consultas; // Retorna a lista de consultas
        }

        /// <summary>
        /// Atualiza uma consulta existente.
        /// </summary>
        /// <param name="consulta">A consulta com os dados atualizados.</param>
        /// <returns>A consulta atualizada.</returns>
        public async Task<Consulta> AtualizarConsultaAsync(Consulta consulta)
        {
            if (consulta == null)
            {
                // Lança exceção se o objeto Consulta for nulo
                throw new ArgumentNullException(nameof(consulta));
            }

            // Verifica se a consulta existe pelo ID
            var existingConsulta = await ObterPorIdAsync(consulta.IdConsulta);
            if (existingConsulta == null)
            {
                // Lança exceção se a consulta não for encontrada
                throw new NotFoundException($"Consulta com ID {consulta.IdConsulta} não encontrada.");
            }

            // Marca a consulta como modificada e salva as alterações
            _context.Entry(consulta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return consulta; // Retorna a consulta atualizada
        }

        /// <summary>
        /// Deleta uma consulta existente pelo ID.
        /// </summary>
        /// <param name="id">ID da consulta a ser deletada.</param>
        /// <returns>Booleano indicando sucesso ou falha da exclusão.</returns>
        public async Task<bool> DeletarConsultaAsync(int id)
        {
            // Busca a consulta pelo ID
            var consulta = await ObterPorIdAsync(id);
            if (consulta == null)
            {
                // Lança exceção se a consulta não for encontrada
                throw new NotFoundException($"Consulta com ID {id} não encontrada.");
            }

            // Remove a consulta do contexto e salva as alterações
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return true; // Indica que a exclusão foi bem-sucedida
        }
    }
}

