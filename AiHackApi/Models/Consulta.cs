using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela no banco de dados como "tb_consultas"
    [Table("tb_consultas")]
    public class Consulta
    {
        // Define a propriedade IdConsulta como chave primária da tabela
        [Key]
        [Column("id_consulta")] // Mapeia a coluna "id_consulta" no banco de dados
        public int IdConsulta { get; set; }

        // Define a data e hora da consulta, campo obrigatório
        [Required] // O campo é obrigatório
        [Column("data_hora_consulta")] // Mapeia a coluna "data_hora_consulta"
        public DateTime DataHoraConsulta { get; set; }

        // Define o status da consulta, campo obrigatório com uma mensagem personalizada
        [Required(ErrorMessage = "O status da consulta é obrigatório.")] // Validação personalizada
        [Column("status_consulta")] // Mapeia a coluna "status_consulta"
        public required string StatusConsulta { get; set; } // O uso de "required" reforça que o valor é obrigatório no .NET 6+

        // Define o campo que armazena o ID do médico, chave estrangeira
        [Required] // O campo é obrigatório
        [Column("tb_medicos_id_medico")] // Mapeia a coluna "tb_medicos_id_medico"
        public int TbMedicosIdMedico { get; set; }

        // Define o campo que armazena o ID do paciente, chave estrangeira
        [Required] // O campo é obrigatório
        [Column("tb_pacientes_id_paciente")] // Mapeia a coluna "tb_pacientes_id_paciente"
        public int TbPacientesIdPaciente { get; set; }

        // Relacionamento de chave estrangeira com a entidade Medico
        [ForeignKey("TbMedicosIdMedico")] // Indica a chave estrangeira para Medico
        public required Medico Medico { get; set; }

        // Relacionamento de chave estrangeira com a entidade Paciente
        [ForeignKey("TbPacientesIdPaciente")] // Indica a chave estrangeira para Paciente
        public required Paciente Paciente { get; set; }
    }
}
