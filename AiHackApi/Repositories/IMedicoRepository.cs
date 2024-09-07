using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedicoRepository
{
    Task<Medico> CreateMedicoAsync(Medico medico);
    Task<Medico> GetMedicoByIdAsync(int id);
    Task<IEnumerable<Medico>> GetAllMedicosAsync();
    Task<Medico> UpdateMedicoAsync(Medico medico);
    Task<bool> DeleteMedicoAsync(int id);
}
