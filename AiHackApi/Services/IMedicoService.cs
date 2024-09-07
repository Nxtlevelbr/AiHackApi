using AiHackApi.Models;
using AiHackApi.Data;
public interface IMedicoService
{
    Task<Medico> CreateMedicoAsync(Medico medico);
    Task<Medico> GetMedicoByIdAsync(int id);
    Task<IEnumerable<Medico>> GetAllMedicosAsync();
    Task<Medico> UpdateMedicoAsync(Medico medico);
    Task<bool> DeleteMedicoAsync(int id);
}

