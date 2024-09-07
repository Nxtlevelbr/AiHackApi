using AiHackApi.Models;
using AiHackApi.Data;


public class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _medicoRepository;

    public MedicoService(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    public async Task<Medico> CreateMedicoAsync(Medico medico)
    {
        return await _medicoRepository.CreateMedicoAsync(medico);
    }

    public async Task<Medico> GetMedicoByIdAsync(int id)
    {
        return await _medicoRepository.GetMedicoByIdAsync(id);
    }

    public async Task<IEnumerable<Medico>> GetAllMedicosAsync()
    {
        return await _medicoRepository.GetAllMedicosAsync();
    }

    public async Task<Medico> UpdateMedicoAsync(Medico medico)
    {
        return await _medicoRepository.UpdateMedicoAsync(medico);
    }

    public async Task<bool> DeleteMedicoAsync(int id)
    {
        return await _medicoRepository.DeleteMedicoAsync(id);
    }
}
