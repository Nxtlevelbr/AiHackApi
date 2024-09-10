using System.ComponentModel.DataAnnotations;  // Importa as anotações de validação de dados
using System.ComponentModel.DataAnnotations.Schema;  // Importa a capacidade de definir a estrutura da tabela no banco de dados

namespace AiHackApi.Models
{
    // Especifica que esta classe está mapeada para a tabela "tb_pacientes"
    [Table("tb_pacientes")]
    public class Paciente
    {
        // Define a propriedade IdPaciente como chave primária da tabela
        [Key]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        // Campo obrigatório para o nome do paciente, mapeado para a coluna "nome_paciente"
        [Required(ErrorMessage = "O nome do paciente é obrigatório.")]
        [Column("nome_paciente")]
        public string NomePaciente { get; set; }

        // Campo obrigatório para o CPF do paciente, mapeado para a coluna "cpf"
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Column("cpf")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string CPF { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Paciente(string nomePaciente, string cpf)
        {
            // Validação nula para evitar que campos obrigatórios fiquem sem valor
            NomePaciente = nomePaciente ?? throw new ArgumentNullException(nameof(nomePaciente));
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor sem parâmetros, útil para a serialização/deserialização e outros cenários
        public Paciente()
        {
            // Inicializa campos com valores padrão

