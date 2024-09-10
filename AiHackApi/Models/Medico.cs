using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela "tb_medicos" no banco de dados
    [Table("tb_medicos")]
    public class Medico
    {
        // Define a propriedade IdMedico como chave primária
        [Key]
        [Column("id_medico")] // Mapeia a coluna "id_medico" no banco de dados
        public int IdMedico { get; set; }

        // Define que o nome do médico é obrigatório e tem no máximo 100 caracteres
        [Required(ErrorMessage = "O nome do médico é obrigatório.")] // Campo obrigatório com mensagem de erro
        [StringLength(100, ErrorMessage = "O nome do médico pode ter no máximo 100 caracteres.")] // Limite de 100 caracteres
        [Column("nm_medico")] // Mapeia a coluna "nm_medico"
        public required string NmMedico { get; set; }

        // Define que o CRM do médico é obrigatório
        [Required] // Campo obrigatório
        [Column("crm_medico")] // Mapeia a coluna "crm_medico"
        public int CrmMedico { get; set; }

        // Define que a especialidade do médico é obrigatória
        [Required] // Campo obrigatório
        [Column("tb_especialidades_id_especialidade")] // Mapeia a coluna de chave estrangeira
        public int TbEspecialidadesIdEspecialidade { get; set; }

        // Define que o contato do médico é obrigatório
        [Required] // Campo obrigatório
        [Column("tb_contatos_id_contato")] // Mapeia a coluna de chave estrangeira
        public int TbContatosIdContato { get; set; }

        // Define que o endereço do médico é obrigatório
        [Required] // Campo obrigatório
        [Column("tb_enderecos_id_endereco")] // Mapeia a coluna de chave estrangeira
        public int TbEnderecosIdEndereco { get; set; }

        // Define a relação de chave estrangeira com a entidade Contato
        [ForeignKey("TbContatosIdContato")] // Relaciona com a tabela de contatos
        public required Contato Contato { get; set; }

        // Define a relação de chave estrangeira com a entidade Endereco
        [ForeignKey("TbEnderecosIdEndereco")] // Relaciona com a tabela de endereços
        public required Endereco Endereco { get; set; }

        // Define a relação de chave estrangeira com a entidade Especialidade
        [ForeignKey("TbEspecialidadesIdEspecialidade")] // Relaciona com a tabela de especialidades
        public required Especialidade Especialidade { get; set; }

        // Define o salário do médico, com precisão numérica para Oracle (NUMBER(18, 2))
        [Required] // Campo obrigatório
        [Column("salario_medico", TypeName = "NUMBER(18, 2)")] // Define o tipo para o banco de dados Oracle
        public decimal SalarioMedico { get; set; }

        // Define se o médico está ativo (1 = ativo, 0 = inativo)
        [Required] // Campo obrigatório
        [Column("ativo")] // Mapeia a coluna "ativo"
        public int Ativo { get; set; } = 1; // Por padrão, o médico está ativo (1)
    }
}
