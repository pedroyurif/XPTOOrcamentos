﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XPTOOrcamentos;

namespace XPTOOrcamentos.Migrations
{
    [DbContext(typeof(DB))]
    [Migration("20211205221450_AlteracaoNomeTabelaPrestadorServicos")]
    partial class AlteracaoNomeTabelaPrestadorServicos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XPTOOrcamentos.Models.Cliente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("XPTOOrcamentos.Models.OrdemServico", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataExecucao")
                        .HasColumnType("datetime2");

                    b.Property<int>("PrestadorID")
                        .HasColumnType("int");

                    b.Property<string>("TituloServico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("PrestadorID");

                    b.ToTable("OrdensServico");
                });

            modelBuilder.Entity("XPTOOrcamentos.Models.PrestadorServico", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PrestadoresServico");
                });

            modelBuilder.Entity("XPTOOrcamentos.Models.OrdemServico", b =>
                {
                    b.HasOne("XPTOOrcamentos.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XPTOOrcamentos.Models.PrestadorServico", "Prestador")
                        .WithMany()
                        .HasForeignKey("PrestadorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Prestador");
                });
#pragma warning restore 612, 618
        }
    }
}
