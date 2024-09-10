// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Contato. Esse DTO é responsável por transferir dados de contato 
    // sem expor diretamente o modelo de dados do banco de dados.
    public class ContatoDto
    {
        // Propriedade que representa o ID do contato
        public int IdContato { get; set; }

        // Propriedade que armazena o número de telefone do contato
        public string Telefone { get; set; }

        // Propriedade que armazena o e-mail do contato
        public string Email { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public ContatoDto(int idContato, string telefone, string email)
        {
            // Inicializa o ID do contato
            IdContato = idContato;

            // Verifica se Telefone é nulo e lança uma exceção se for
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));

            // Verifica se Email é nulo e lança uma exceção se for
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public ContatoDto()
        {
            // Inicializa Telefone e Email com strings vazias
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}

