using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_especialidades")]
    public class Especialidade
    {
        [Key]
        [Column("id_especialidade")]
        public int IdEspecialidade { get; set; }

        [Required]
        [Column("nome_especialidade")]
        public string NomeEspecialidade { get; set; }

        [Required]
        [Column("ativo")]
        public int Ativo { get; set; } // 1 para true, 0 para false

        // Construtor que assegura que NomeEspecialidade tem um valor válido
        public Especialidade(string nomeEspecialidade, bool ativo)
        {
            NomeEspecialidade = nomeEspecialidade ?? throw new ArgumentNullException(nameof(nomeEspecialidade));
            Ativo = ativo ? 1 : 0; // Converte o bool para int
        }

        // Construtor sem parâmetros, caso necessário
        public Especialidade()
        {
            NomeEspecialidade = string.Empty;
            Ativo = 1; // Definir como ativo por padrão (1 = true)
        }
    }
}
