using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela "tb_medicos" no banco de dados
    [Table("tb_medicos")]
    public class Medico
    {
        // Define a propriedade CrmMedico como chave primária
        [Key]
        [Column("crm_medico")] // Mapeia a coluna "crm_medico"
        public int CrmMedico { get; set; }

        // Define que o nome do médico é obrigatório e tem no máximo 100 caracteres
        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do médico pode ter no máximo 100 caracteres.")]
        [Column("nm_medico")] // Mapeia a coluna "nm_medico"
        public required string NmMedico { get; set; }

        // Define que a especialidade do médico é obrigatória
        [Required]
        [Column("tb_especialidades_id_especialidade")] // Mapeia a coluna de chave estrangeira
        public int TbEspecialidadesIdEspecialidade { get; set; }

        // Define que o contato do médico é obrigatório, agora usando o campo Email
        [Required]
        [Column("email_contato")] // Mapeia a coluna "email_contato"
        public string EmailContato { get; set; }

        // Define que o endereço do médico é obrigatório
        [Required]
        [Column("tb_enderecos_id_endereco")] // Mapeia a coluna de chave estrangeira
        public int TbEnderecosIdEndereco { get; set; }

        // Define a relação de chave estrangeira com a entidade Contato, usando o Email
        [ForeignKey("EmailContato")]
        public required Contato Contato { get; set; }

        // Define a relação de chave estrangeira com a entidade Endereco
        [ForeignKey("TbEnderecosIdEndereco")]
        public required Endereco Endereco { get; set; }

        // Define a relação de chave estrangeira com a entidade Especialidade
        [ForeignKey("TbEspecialidadesIdEspecialidade")]
        public required Especialidade Especialidade { get; set; }
    }
}
