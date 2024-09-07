using AiHackApi.Models;
using AiHackApi.Repositories;

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _enderecoRepository;

    public EnderecoService(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    public async Task<Endereco> CreateEnderecoAsync(Endereco endereco)
    {
        return await _enderecoRepository.AdicionarAsync(endereco);
    }

    public async Task<Endereco> GetEnderecoByIdAsync(int id)
    {
        return await _enderecoRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Endereco>> GetAllEnderecosAsync()
    {
        return await _enderecoRepository.ObterTodosAsync();
    }

    public async Task<Endereco> UpdateEnderecoAsync(Endereco endereco)
    {
        await _enderecoRepository.AtualizarAsync(endereco);
        return endereco;
    }

    public async Task<bool> DeleteEnderecoAsync(int id)
    {
        return await _enderecoRepository.DeletarAsync(id);
    }
}
