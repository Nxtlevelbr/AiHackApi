using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_bairros")]
    public class Bairro
    {
        [Key]
        [Column("id_bairro")]
        public int IdBairro { get; set; }

        [Required]
        [Column("nome_bairro")]
        public string NomeBairro { get; set; }

        // Construtor que assegura que NomeBairro tem um valor válido
        public Bairro(string nomeBairro)
        {
            NomeBairro = nomeBairro ?? throw new ArgumentNullException(nameof(nomeBairro));
        }

        // Construtor sem parâmetros, caso necessário
        public Bairro()
        {
            // Inicializando NomeBairro com um valor padrão
            NomeBairro = string.Empty;
        }
    }
}


