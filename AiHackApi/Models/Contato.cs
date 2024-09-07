using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_contatos")]
    public class Contato
    {
        [Key]
        [Column("id_contato")]
        public int IdContato { get; set; }

        [Required]
        [Column("telefone")]
        public string Telefone { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Contato(string telefone, string email)
        {
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        // Construtor sem parâmetros
        public Contato()
        {
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}

