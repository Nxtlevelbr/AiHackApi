using Microsoft.EntityFrameworkCore;
using AiHackApi.Models;

namespace AiHackApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definir o DbSet para Pacientes
        public DbSet<Paciente> Pacientes { get; set; }

        // Definir outros DbSets para outras entidades (Ex: Medicos, Consultas, etc.)
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
                    .HasPrecision(18, 2); // Define a precisão e escala para SalarioMedico
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
