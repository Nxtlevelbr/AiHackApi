// Importação do Entity Framework Core para uso de DbContext e manipulação de dados
using Microsoft.EntityFrameworkCore;
using AiHackApi.Models; // Importa os modelos de entidades do projeto

namespace AiHackApi.Data
{
    // A classe ApplicationDbContext é responsável por representar a sessão com o banco de dados
    // e por mapear as entidades do modelo para as tabelas no banco de dados.
    public class ApplicationDbContext : DbContext
    {
        // O construtor injeta opções de configuração do DbContext, como a ConnectionString e o provedor de banco de dados.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet representa uma coleção de entidades do tipo Paciente no banco de dados.
        // O Entity Framework vai criar a tabela Pacientes e mapear as propriedades de Paciente para as colunas dessa tabela.
        public DbSet<Paciente> Pacientes { get; set; }

        // Definindo outros DbSets para mapear as entidades relacionadas a outras tabelas do banco de dados.
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        // Método OnModelCreating é usado para customizar o comportamento do Entity Framework ao criar o modelo de dados.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remover a configuração para SalarioMedico, pois não faz mais parte da entidade Medico
            modelBuilder.Entity<Medico>(entity =>
            {
                // Se necessário, adicione qualquer configuração adicional para CrmMedico aqui.
                // entity.HasKey(e => e.CrmMedico); // Isso é automático, mas pode ser configurado explicitamente se necessário.
            });

            // Chamando o método base para garantir que outras configurações padrão sejam aplicadas.
            base.OnModelCreating(modelBuilder);
        }
    }
}
