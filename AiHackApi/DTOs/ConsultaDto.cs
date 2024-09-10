// Namespace que contém os DTOs (Data Transfer Objects) da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Consulta. Esse DTO transfere dados sobre consultas 
    // sem expor diretamente o modelo de dados do banco.
    public class ConsultaDto
    {
        // Propriedade que representa o ID da consulta
        public int IdConsulta { get; set; }

        // Propriedade que representa a data da consulta
        public DateTime DataConsulta { get; set; }

        // Propriedade que armazena o diagnóstico da consulta
        public string Diagnostico { get; set; }

        // Propriedade que representa o ID do paciente relacionado à consulta
        public int IdPaciente { get; set; }

        // Propriedade que representa o ID do médico relacionado à consulta
        public int IdMedico { get; set; }

        // Construtor que inicializa o DTO com valores específicos para todas as propriedades
        public ConsultaDto(int idConsulta, DateTime dataConsulta, string diagnostico, int idPaciente, int idMedico)
        {
            // Inicializa as propriedades com os valores fornecidos
            IdConsulta = idConsulta;
            DataConsulta = dataConsulta;

            // Verifica se Diagnostico é nulo e lança uma exceção se for
            Diagnostico = diagnostico ?? throw new ArgumentNullException(nameof(diagnostico));

            // Inicializa os IDs do paciente e do médico
            IdPaciente = idPaciente;
            IdMedico = idMedico;
        }

        // Construtor padrão para inicializar o DTO com valores padrão
        public ConsultaDto()
        {
            // Inicializa DataConsulta com a menor data possível e Diagnostico com uma string vazia
            DataConsulta = DateTime.MinValue;
            Diagnostico = string.Empty;
        }
    }
}

