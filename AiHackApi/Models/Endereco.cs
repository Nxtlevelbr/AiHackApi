using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_enderecos")]
    public class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public int IdEndereco { get; set; }

        [Required]
        [Column("rua")]
        public string Rua { get; set; }

        [Required]
        [Column("numero")]
        public string Numero { get; set; }

        [Required]
        [Column("bairro")]
        public string Bairro { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Endereco(string rua, string numero, string bairro)
        {
            Rua = rua ?? throw new ArgumentNullException(nameof(rua));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
        }

        // Construtor sem parâmetros
        public Endereco()
        {
            Rua = string.Empty;
            Numero = string.Empty;
            Bairro = string.Empty;
        }
    }
}
