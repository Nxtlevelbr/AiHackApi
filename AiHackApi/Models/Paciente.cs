using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_pacientes")]
    public class Paciente
    {
        [Key]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Required]
        [Column("nome_paciente")]
        public string NomePaciente { get; set; }

        [Required]
        [Column("cpf")]
        public string CPF { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Paciente(string nomePaciente, string cpf)
        {
            NomePaciente = nomePaciente ?? throw new ArgumentNullException(nameof(nomePaciente));
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor sem parâmetros
        public Paciente()
        {
            NomePaciente = string.Empty;
            CPF = string.Empty;
        }
    }
}
