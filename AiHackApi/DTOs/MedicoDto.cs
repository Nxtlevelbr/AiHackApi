// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Médico. Esse DTO é usado para transferir informações sobre médicos
    // entre diferentes camadas da aplicação.
    public class MedicoDto
    {
        // Propriedade que representa o ID do médico
        public int IdMedico { get; set; }

        // Propriedade que armazena o nome do médico
        public string NomeMedico { get; set; }

        // Propriedade que contém o CRM do médico (registro profissional)
        public string CRM { get; set; }

        // Propriedade que indica a especialidade médica do médico
        public string Especialidade { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public MedicoDto(int idMedico, string nomeMedico, string crm, string especialidade)
        {
            // Inicializa o ID do médico
            IdMedico = idMedico;

            // Verifica se o nome do médico é nulo e lança uma exceção se for
            NomeMedico = nomeMedico ?? throw new ArgumentNullException(nameof(nomeMedico));

            // Verifica se o CRM é nulo e lança uma exceção se for
            CRM = crm ?? throw new ArgumentNullException(nameof(crm));

            // Verifica se a especialidade é nula e lança uma exceção se for
            Especialidade = especialidade ?? throw new ArgumentNullException(nameof(especialidade));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public MedicoDto()
        {
            // Inicializa o nome do médico como uma string vazia
            NomeMedico = string.Empty;

            // Inicializa o CRM como uma string vazia
            CRM = string.Empty;

            // Inicializa a especialidade como uma string vazia
            Especialidade = string.Empty;
        }
    }
}

