﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetWebApi.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NetWebApi.Migrations
{
    [DbContext(typeof(NetdbContext))]
    partial class NetdbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("NetWebApi.Modelos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Opcao")
                        .HasColumnType("integer");

                    b.Property<double>("Quantidade")
                        .HasColumnType("double precision");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Net visualsutido",
                            Opcao = 1,
                            Quantidade = 1.0,
                            Valor = 2.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}