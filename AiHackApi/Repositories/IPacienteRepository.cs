using AiHackApi.Models;

public interface IPacienteRepository
{
    Task<Paciente> AdicionarAsync(Paciente paciente);
    Task<Paciente> ObterPorIdAsync(int id);
    Task<IEnumerable<Paciente>> ObterTodosAsync();
    Task<Paciente> AtualizarAsync(Paciente paciente);
    Task<bool> DeletarAsync(int id);
}

