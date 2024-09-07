using AiHackApi.Models;

public interface IEnderecoService
{
    Task<Endereco> CreateEnderecoAsync(Endereco endereco);
    Task<Endereco> GetEnderecoByIdAsync(int id);
    Task<IEnumerable<Endereco>> GetAllEnderecosAsync();
    Task<Endereco> UpdateEnderecoAsync(Endereco endereco);
    Task<bool> DeleteEnderecoAsync(int id);
}

