// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Endereço. Esse DTO é responsável por transferir os dados de endereço
    // sem expor diretamente o modelo de dados do banco de dados.
    public class EnderecoDto
    {
        // Propriedade que representa o ID do endereço
        public int IdEndereco { get; set; }

        // Propriedade que armazena o nome da rua do endereço
        public string Rua { get; set; }

        // Propriedade que armazena a cidade do endereço
        public string Cidade { get; set; }

        // Propriedade que armazena o estado do endereço
        public string Estado { get; set; }

        // Propriedade que armazena o CEP (Código de Endereçamento Postal)
        public string CEP { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public EnderecoDto(int idEndereco, string rua, string cidade, string estado, string cep)
        {
            // Inicializa o ID do endereço
            IdEndereco = idEndereco;

            // Verifica se Rua é nula e lança uma exceção se for
            Rua = rua ?? throw new ArgumentNullException(nameof(rua));

            // Verifica se Cidade é nula e lança uma exceção se for
            Cidade = cidade ?? throw new ArgumentNullException(nameof(cidade));

            // Verifica se Estado é nulo e lança uma exceção se for
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));

            // Verifica se CEP é nulo e lança uma exceção se for
            CEP = cep ?? throw new ArgumentNullException(nameof(cep));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public EnderecoDto()
        {
            // Inicializa todas as propriedades com strings vazias
            Rua = string.Empty;
            Cidade = string.Empty;
            Estado = string.Empty;
            CEP = string.Empty;
        }
    }
}
