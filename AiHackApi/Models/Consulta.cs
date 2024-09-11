using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela no banco de dados como "tb_consultas"
    [Table("tb_consultas")]
    public class Consulta
    {
        // Define a data e hora da consulta, campo obrigatório
        [Required] // O campo é obrigatório
        [Column("data_hora_consulta")] // Mapeia a coluna "data_hora_consulta"
        public DateTime DataHoraConsulta { get; set; }

        // Define o status da consulta, campo obrigatório com uma mensagem personalizada
        [Required(ErrorMessage = "O status da consulta é obrigatório.")] // Validação personalizada
        [Column("status_consulta")] // Mapeia a coluna "status_consulta"
        public string StatusConsulta { get; set; }

        // Define o campo que armazena o ID do médico, chave estrangeira
        [Required] // O campo é obrigatório
        [Column("tb_medicos_id_medico")] // Mapeia a coluna "tb_medicos_id_medico"
        public int TbMedicosIdMedico { get; set; }

        // Define o campo que armazena o CPF do paciente, chave estrangeira
        [Required] // O campo é obrigatório
        [Column("cpf_paciente")] // Mapeia a coluna "cpf_paciente"
        public string CpfPaciente { get; set; }

        // Relacionamento de chave estrangeira com a entidade Medico
        [ForeignKey("TbMedicosIdMedico")] // Indica a chave estrangeira para Medico
        public Medico Medico { get; set; }

        // Relacionamento de chave estrangeira com a entidade Paciente
        [ForeignKey("CpfPaciente")] // Indica a chave estrangeira para Paciente
        public Paciente Paciente { get; set; }
    }
}
