// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém a definição do modelo de dados, como Consulta.
using AiHackApi.Models;
// System.Collections.Generic: Fornece suporte para coleções genéricas, como IEnumerable.
using System.Collections.Generic;
// System.Threading.Tasks: Fornece suporte para operações assíncronas com Task.
using System.Threading.Tasks;

public interface IConsultaService
{
    /// <summary>
    /// Obtém todas as consultas do repositório.
    /// </summary>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todas as consultas.</returns>
    Task<IEnumerable<Consulta>> GetAllConsultasAsync();

    /// <summary>
    /// Obtém uma consulta específica pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
    /// </summary>
    /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
    /// <param name="cpfPaciente">O CPF do paciente.</param>
    /// <param name="idMedico">O ID do médico.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é a consulta correspondente à chave composta ou null se não for encontrada.</returns>
    Task<Consulta?> GetConsultaByIdAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico);

    /// <summary>
    /// Cria uma nova consulta e a adiciona ao repositório.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> a ser criado.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task CreateConsultaAsync(Consulta consulta);

    /// <summary>
    /// Atualiza uma consulta existente no repositório.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> com as informações atualizadas.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task UpdateConsultaAsync(Consulta consulta);

    /// <summary>
    /// Remove uma consulta do repositório pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
    /// </summary>
    /// <param name="dataHoraConsulta">A data e hora da consulta a ser removida.</param>
    /// <param name="cpfPaciente">O CPF do paciente da consulta a ser removida.</param>
    /// <param name="idMedico">O ID do médico da consulta a ser removida.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task DeleteConsultaAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico);
}
