using AiHackApi.Models; // Importa o modelo Paciente
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

/// <summary>
/// Interface que define os métodos para o repositório de pacientes.
/// </summary>
public interface IPacienteRepository
{
    /// <summary>
    /// Adiciona um novo paciente no banco de dados.
    /// </summary>
    /// <param name="paciente">O objeto paciente a ser adicionado.</param>
    /// <returns>O paciente recém-criado.</returns>
    Task<Paciente> AdicionarAsync(Paciente paciente);

    /// <summary>
    /// Obtém os detalhes de um paciente específico pelo ID.
    /// </summary>
    /// <param name="id">O ID do paciente.</param>
    /// <returns>O paciente correspondente ao ID informado.</returns>
    Task<Paciente> ObterPorIdAsync(int id);

    /// <summary>
    /// Retorna todos os pacientes cadastrados no banco de dados.
    /// </summary>
    /// <returns>Uma lista de pacientes.</returns>
    Task<IEnumerable<Paciente>> ObterTodosAsync();

    /// <summary>
    /// Atualiza os dados de um paciente existente.
    /// </summary>
    /// <param name="paciente">O objeto paciente com os dados atualizados.</param>
    /// <returns>O paciente atualizado.</returns>
    Task<Paciente> AtualizarAsync(Paciente paciente);

    /// <summary>
    /// Remove um paciente do banco de dados usando o ID.
    /// </summary>
    /// <param name="id">O ID do paciente a ser deletado.</param>
    /// <returns>True se o paciente foi deletado com sucesso, caso contrário False.</returns>
    Task<bool> DeletarAsync(int id);
}
}

