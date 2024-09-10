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
    /// Obtém uma consulta específica pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador da consulta.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é a consulta correspondente ao identificador, ou null se não for encontrada.</returns>
    Task<Consulta?> GetConsultaByIdAsync(int id);

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
    /// Remove uma consulta do repositório pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador da consulta a ser removida.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task DeleteConsultaAsync(int id);
}
