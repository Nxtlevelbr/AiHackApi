using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    [Table("tb_medicos")] // Define o nome da tabela
    public class Medico
    {
        [Key]
        [Column("id_medico")]
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do médico pode ter no máximo 100 caracteres.")]
        [Column("nm_medico")]
        public required string NmMedico { get; set; }

        [Required]
        [Column("crm_medico")]
        public int CrmMedico { get; set; }

        [Required]
        [Column("tb_especialidades_id_especialidade")]
        public int TbEspecialidadesIdEspecialidade { get; set; }

        [Required]
        [Column("tb_contatos_id_contato")]
        public int TbContatosIdContato { get; set; }

        [Required]
        [Column("tb_enderecos_id_endereco")]
        public int TbEnderecosIdEndereco { get; set; }

        // Definindo as chaves estrangeiras
        [ForeignKey("TbContatosIdContato")]
        public required Contato Contato { get; set; }

        [ForeignKey("TbEnderecosIdEndereco")]
        public required Endereco Endereco { get; set; }

        [ForeignKey("TbEspecialidadesIdEspecialidade")]
        public required Especialidade Especialidade { get; set; }

        // Correção no campo salario_medico
        [Required]
        [Column("salario_medico", TypeName = "NUMBER(18, 2)")] // Define o tipo para Oracle
        public decimal SalarioMedico { get; set; }

        // Correção no campo ativo, usando int para simular booleano
        [Required]
        [Column("ativo")]
        public int Ativo { get; set; } = 1; // 1 = ativo (true), 0 = inativo (false)
    }
}
