using Microsoft.EntityFrameworkCore;// Importação do Entity Framework Core para uso de DbContext e manipulação de dados
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
            // Configuração da chave primária do Paciente
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.CPF); // Define CPF como chave primária
                entity.Property(e => e.CPF).IsRequired(); // Certifica que CPF é obrigatório
                entity.Property(e => e.NomePaciente).IsRequired(); // Certifica que o NomePaciente é obrigatório
            });

            // Chamando o método base para garantir que outras configurações padrão sejam aplicadas.
            base.OnModelCreating(modelBuilder);
        }
    }
}
