using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela como "tb_contatos" no banco de dados
    [Table("tb_contatos")]
    public class Contato
    {
        // Define a propriedade IdContato como chave primária da tabela
        [Key]
        [Column("id_contato")] // Mapeia a coluna "id_contato" no banco de dados
        public int IdContato { get; set; }

        // Define a propriedade Telefone como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("telefone")] // Mapeia a coluna "telefone" no banco de dados
        public string Telefone { get; set; }

        // Define a propriedade Email como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("email")] // Mapeia a coluna "email" no banco de dados
        public string Email { get; set; }

        // Construtor que inicializa os campos obrigatórios com valores válidos
        public Contato(string telefone, string email)
        {
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone)); // Verifica se telefone é nulo
            Email = email ?? throw new ArgumentNullException(nameof(email)); // Verifica se email é nulo
        }

        // Construtor sem parâmetros, inicializando os campos com valores padrão
        public Contato()
        {
            // Inicializa as propriedades com valores padrões para evitar nulos
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}


