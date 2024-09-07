using Microsoft.EntityFrameworkCore;
using AiHackApi.Models;

namespace AiHackApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Defina o DbSet para Pacientes
        public DbSet<Paciente> Pacientes { get; set; }

        // Defina outros DbSets para outras entidades (Ex: Medicos, Consultas, etc.)
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Contato> Contatos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>(entity =>
            {
                entity.Property(e => e.SalarioMedico)
                    .HasPrecision(18, 2); // Define a precisão (18 dígitos no total) e escala (2 casas decimais)
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
