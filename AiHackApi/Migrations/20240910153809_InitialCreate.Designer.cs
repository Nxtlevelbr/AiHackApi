﻿// <auto-generated />
using System;
using AiHackApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace AiHackApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240910153809_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AiHackApi.Models.Bairro", b =>
                {
                    b.Property<int>("IdBairro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_bairro");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBairro"));

                    b.Property<string>("NomeBairro")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("nome_bairro");

                    b.HasKey("IdBairro");

                    b.ToTable("tb_bairros");
                });

            modelBuilder.Entity("AiHackApi.Models.Consulta", b =>
                {
                    b.Property<int>("IdConsulta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_consulta");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdConsulta"));

                    b.Property<DateTime>("DataHoraConsulta")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("data_hora_consulta");

                    b.Property<string>("StatusConsulta")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("status_consulta");

                    b.Property<int>("TbMedicosIdMedico")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("tb_medicos_id_medico");

                    b.Property<int>("TbPacientesIdPaciente")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("tb_pacientes_id_paciente");

                    b.HasKey("IdConsulta");

                    b.HasIndex("TbMedicosIdMedico");

                    b.HasIndex("TbPacientesIdPaciente");

                    b.ToTable("tb_consultas");
                });

            modelBuilder.Entity("AiHackApi.Models.Contato", b =>
                {
                    b.Property<int>("IdContato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_contato");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContato"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("email");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("telefone");

                    b.HasKey("IdContato");

                    b.ToTable("tb_contatos");
                });

            modelBuilder.Entity("AiHackApi.Models.Endereco", b =>
                {
                    b.Property<int>("IdEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_endereco");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEndereco"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("bairro");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("numero");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("rua");

                    b.HasKey("IdEndereco");

                    b.ToTable("tb_enderecos");
                });

            modelBuilder.Entity("AiHackApi.Models.Especialidade", b =>
                {
                    b.Property<int>("IdEspecialidade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_especialidade");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEspecialidade"));

                    b.Property<int>("Ativo")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ativo");

                    b.Property<string>("NomeEspecialidade")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("nome_especialidade");

                    b.HasKey("IdEspecialidade");

                    b.ToTable("tb_especialidades");
                });

            modelBuilder.Entity("AiHackApi.Models.Medico", b =>
                {
                    b.Property<int>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_medico");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMedico"));

                    b.Property<int>("Ativo")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ativo");

                    b.Property<int>("CrmMedico")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("crm_medico");

                    b.Property<string>("NmMedico")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("nm_medico");

                    b.Property<decimal>("SalarioMedico")
                        .HasPrecision(18, 2)
                        .HasColumnType("NUMBER(18, 2)")
                        .HasColumnName("salario_medico");

                    b.Property<int>("TbContatosIdContato")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("tb_contatos_id_contato");

                    b.Property<int>("TbEnderecosIdEndereco")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("tb_enderecos_id_endereco");

                    b.Property<int>("TbEspecialidadesIdEspecialidade")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("tb_especialidades_id_especialidade");

                    b.HasKey("IdMedico");

                    b.HasIndex("TbContatosIdContato");

                    b.HasIndex("TbEnderecosIdEndereco");

                    b.HasIndex("TbEspecialidadesIdEspecialidade");

                    b.ToTable("tb_medicos");
                });

            modelBuilder.Entity("AiHackApi.Models.Paciente", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("id_paciente");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPaciente"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("cpf");

                    b.Property<string>("NomePaciente")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("nome_paciente");

                    b.HasKey("IdPaciente");

                    b.ToTable("tb_pacientes");
                });

            modelBuilder.Entity("AiHackApi.Models.Consulta", b =>
                {
                    b.HasOne("AiHackApi.Models.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("TbMedicosIdMedico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AiHackApi.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("TbPacientesIdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("AiHackApi.Models.Medico", b =>
                {
                    b.HasOne("AiHackApi.Models.Contato", "Contato")
                        .WithMany()
                        .HasForeignKey("TbContatosIdContato")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AiHackApi.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("TbEnderecosIdEndereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AiHackApi.Models.Especialidade", "Especialidade")
                        .WithMany()
                        .HasForeignKey("TbEspecialidadesIdEspecialidade")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contato");

                    b.Navigation("Endereco");

                    b.Navigation("Especialidade");
                });
#pragma warning restore 612, 618
        }
    }
}
