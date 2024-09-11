using AiHackApi.Models; // Importa o modelo Consulta
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de consultas.
    /// </summary>
    public interface IConsultaRepository
    {
        /// <summary>
        /// Cria uma nova consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">A consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        Task<Consulta> CriarConsultaAsync(Consulta consulta);

        /// <summary>
        /// Busca uma consulta pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
        /// <param name="cpfPaciente">O CPF do paciente.</param>
        /// <param name="idMedico">O ID do médico.</param>
        /// <returns>A consulta correspondente à chave composta fornecida.</returns>
        Task<Consulta> ObterPorChaveAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico);

        /// <summary>
        /// Retorna todas as consultas cadastradas.
        /// </summary>
        /// <returns>Uma lista de todas as consultas.</returns>
        Task<IEnumerable<Consulta>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="consulta">A consulta com os dados atualizados.</param>
        /// <returns>A consulta atualizada.</returns>
        Task<Consulta> AtualizarConsultaAsync(Consulta consulta);

        /// <summary>
        /// Deleta uma consulta pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
        /// </summary>
        /// <param name="dataHoraConsulta">A data e hora da consulta a ser deletada.</param>
        /// <param name="cpfPaciente">O CPF do paciente da consulta a ser deletada.</param>
        /// <param name="idMedico">O ID do médico da consulta a ser deletada.</param>
        /// <returns>True se a consulta foi deletada com sucesso, caso contrário False.</returns>
        Task<bool> DeletarConsultaAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico);
    }
}
