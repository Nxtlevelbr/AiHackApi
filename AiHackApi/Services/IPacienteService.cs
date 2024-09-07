using AiHackApi.DTOs;
using AiHackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public interface IPacienteService
    {
        Task<PacienteDto> GetPacienteByIdAsync(int id);
        Task<IEnumerable<PacienteDto>> GetAllPacientesAsync();
        Task CreatePacienteAsync(PacienteDto pacienteDto);
        Task UpdatePacienteAsync(PacienteDto pacienteDto);
        Task DeletePacienteAsync(int id);
    }
}

