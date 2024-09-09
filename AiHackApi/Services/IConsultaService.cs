using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IConsultaService
{
    Task<IEnumerable<Consulta>> GetAllConsultasAsync();
    Task<Consulta?> GetConsultaByIdAsync(int id);
    Task CreateConsultaAsync(Consulta consulta);
    Task UpdateConsultaAsync(Consulta consulta);
    Task DeleteConsultaAsync(int id);
}
