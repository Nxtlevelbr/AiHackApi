using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define a tabela "tb_enderecos" no banco de dados para esta classe
    [Table("tb_enderecos")]
    public class Endereco
    {
        // Define a propriedade IdEndereco como chave primária
        [Key]
        [Column("id_endereco")] // Mapeia a coluna "id_endereco" no banco de dados
        public int IdEndereco { get; set; }

        // Define a propriedade Rua como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("rua")] // Mapeia a coluna "rua" no banco de dados
        public string Rua { get; set; }

        // Define a propriedade Numero como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("numero")] // Mapeia a coluna "numero" no banco de dados
        public string Numero { get; set; }

        // Define a propriedade Bairro como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("bairro")] // Mapeia a coluna "bairro" no banco de dados
        public string Bairro { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Endereco(string rua, string numero, string bairro)
        {
            Rua = rua ?? throw new ArgumentNullException(nameof(rua)); // Garante que Rua não seja nulo
            Numero = numero ?? throw new ArgumentNullException(nameof(numero)); // Garante que Numero não seja nulo
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro)); // Garante que Bairro não seja nulo
        }

        // Construtor sem parâmetros, inicializando com valores padrão
        public Endereco()
        {
            Rua = string.Empty;  // Inicializa Rua como string vazia
            Numero = string.Empty; // Inicializa Numero como string vazia
            Bairro = string.Empty; // Inicializa Bairro como string vazia
        }
    }
}
