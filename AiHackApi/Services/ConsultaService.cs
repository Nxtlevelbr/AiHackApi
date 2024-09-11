// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Data: Contém a definição do contexto de dados da aplicação.
using AiHackApi.Data;
// AiHackApi.Models: Contém as definições dos modelos de dados, como Consulta.
using AiHackApi.Models;
// Microsoft.EntityFrameworkCore: Fornece classes e métodos para trabalhar com Entity Framework Core.
using Microsoft.EntityFrameworkCore;
// System.Collections.Generic: Fornece interfaces e classes para coleções genéricas, como IEnumerable.
using System.Collections.Generic;
// System.Threading.Tasks: Fornece tipos para operações assíncronas, como Task.
using System.Threading.Tasks;

public class ConsultaService : IConsultaService
{
    // Contexto de dados usado para acessar a base de dados.
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Construtor do serviço de consultas.
    /// </summary>
    /// <param name="context">Instância do contexto de dados da aplicação.</param>
    public ConsultaService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todas as consultas, incluindo os dados relacionados de pacientes e médicos.
    /// </summary>
    /// <returns>Uma lista de todas as consultas.</returns>
    public async Task<IEnumerable<Consulta>> GetAllConsultasAsync()
    {
        // Recupera todas as consultas, incluindo informações de pacientes e médicos.
        return await _context.Consultas.Include(c => c.Paciente).Include(c => c.Medico).ToListAsync();
    }

    /// <summary>
    /// Obtém uma consulta específica pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
    /// </summary>
    /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
    /// <param name="cpfPaciente">O CPF do paciente.</param>
    /// <param name="idMedico">O ID do médico.</param>
    /// <returns>A consulta correspondente à chave composta ou null se não for encontrada.</returns>
    public async Task<Consulta?> GetConsultaByIdAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
    {
        // Recupera uma consulta pela chave composta, incluindo informações de pacientes e médicos.
        return await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .FirstOrDefaultAsync(c => c.DataHoraConsulta == dataHoraConsulta
                                    && c.CpfPaciente == cpfPaciente
                                    && c.TbMedicosIdMedico == idMedico);
    }

    /// <summary>
    /// Cria uma nova consulta e a adiciona ao banco de dados.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> a ser criado.</param>
    public async Task CreateConsultaAsync(Consulta consulta)
    {
        // Adiciona a nova consulta ao contexto e salva as mudanças no banco de dados.
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Atualiza uma consulta existente no banco de dados.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> com as informações atualizadas.</param>
    public async Task UpdateConsultaAsync(Consulta consulta)
    {
        // Marca o objeto consulta como modificado e salva as mudanças no banco de dados.
        _context.Entry(consulta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove uma consulta do banco de dados pela chave composta (DataHoraConsulta, CpfPaciente, TbMedicosIdMedico).
    /// </summary>
    /// <param name="dataHoraConsulta">A data e hora da consulta.</param>
    /// <param name="cpfPaciente">O CPF do paciente.</param>
    /// <param name="idMedico">O ID do médico da consulta.</param>
    public async Task DeleteConsultaAsync(DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
    {
        // Encontra a consulta pela chave composta e a remove do banco de dados, se existir.
        var consulta = await GetConsultaByIdAsync(dataHoraConsulta, cpfPaciente, idMedico);
        if (consulta != null)
        {
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
        }
    }
}
