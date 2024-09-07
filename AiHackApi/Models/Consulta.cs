using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_consultas")] // Define o nome da tabela
    public class Consulta
    {
        [Key]
        [Column("id_consulta")]
        public int IdConsulta { get; set; }

        [Required]
        [Column("data_hora_consulta")]
        public DateTime DataHoraConsulta { get; set; }

        [Required(ErrorMessage = "O status da consulta é obrigatório.")]
        [Column("status_consulta")]
        public required string StatusConsulta { get; set; }

        [Required]
        [Column("tb_medicos_id_medico")]
        public int TbMedicosIdMedico { get; set; }

        [Required]
        [Column("tb_pacientes_id_paciente")]
        public int TbPacientesIdPaciente { get; set; }

        [ForeignKey("TbMedicosIdMedico")]
        public required Medico Medico { get; set; }

        [ForeignKey("TbPacientesIdPaciente")]
        public required Paciente Paciente { get; set; }
    }
}
