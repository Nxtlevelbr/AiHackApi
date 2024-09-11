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
                throw new ArgumentNullException(nameof(consulta)); // Lança exceção se o objeto Consulta for nulo
            }

            await _context.Consultas.AddAsync(consulta); // Adiciona a nova consulta ao contexto do banco de dados
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return consulta; // Retorna a consulta criada
        }

        /// <summary>
        /// Obtém uma consulta pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
        /// </summary>
        /// <param name="dataHoraConsulta">Data e hora da consulta.</param>
        /// <param name="cpfPaciente">CPF do paciente.</param>
        /// <param name="idMedico">ID do médico.</param>
        /// <returns>A consulta correspondente à chave composta fornecida.</returns>
        public async Task<Consulta> ObterPorChaveAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
        {
            // Busca a consulta pela chave composta no banco de dados
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.DataHoraConsulta == dataHoraConsulta
                                          && c.CpfPaciente == cpfPaciente
                                          && c.TbMedicosIdMedico == idMedico);

            if (consulta == null)
            {
                throw new NotFoundException($"Consulta não encontrada para DataHoraConsulta: {dataHoraConsulta}, CPF: {cpfPaciente}, ID do médico: {idMedico}.");
            }

            return consulta; // Retorna a consulta encontrada
        }

        /// <summary>
        /// Obtém todas as consultas do banco de dados.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        public async Task<IEnumerable<Consulta>> ObterTodosAsync()
        {
            var consultas = await _context.Consultas.AsNoTracking().ToListAsync(); // Busca todas as consultas no banco de dados

            if (consultas == null || !consultas.Any())
            {
                throw new NotFoundException("Nenhuma consulta encontrada."); // Lança exceção se nenhuma consulta for encontrada
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
                throw new ArgumentNullException(nameof(consulta)); // Lança exceção se o objeto Consulta for nulo
            }

            // Verifica se a consulta existe pela chave composta
            var existingConsulta = await ObterPorChaveAsync(consulta.DataHoraConsulta, consulta.CpfPaciente, consulta.TbMedicosIdMedico);
            if (existingConsulta == null)
            {
                throw new NotFoundException($"Consulta não encontrada para DataHoraConsulta: {consulta.DataHoraConsulta}, CPF: {consulta.CpfPaciente}, ID do médico: {consulta.TbMedicosIdMedico}.");
            }

            _context.Entry(consulta).State = EntityState.Modified; // Marca a consulta como modificada e salva as alterações
            await _context.SaveChangesAsync();
            return consulta; // Retorna a consulta atualizada
        }

        /// <summary>
        /// Deleta uma consulta existente pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
        /// </summary>
        /// <param name="dataHoraConsulta">Data e hora da consulta.</param>
        /// <param name="cpfPaciente">CPF do paciente.</param>
        /// <param name="idMedico">ID do médico.</param>
        /// <returns>Booleano indicando sucesso ou falha da exclusão.</returns>
        public async Task<bool> DeletarConsultaAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
        {
            var consulta = await ObterPorChaveAsync(dataHoraConsulta, cpfPaciente, idMedico); // Busca a consulta pela chave composta
            if (consulta == null)
            {
                return false; // Retorna false se a consulta não for encontrada
            }

            _context.Consultas.Remove(consulta); // Remove a consulta do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return true; // Indica que a exclusão foi bem-sucedida
        }
    }
}
