﻿// <auto-generated />
using System;
using Ellen_Falpus_CadCategoria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ellen_Falpus_CadCategoria.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20221115012509_atualizacao de projeto")]
    partial class atualizacaodeprojeto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CentroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Comprimento")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<decimal>("Largura")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SubcategoriaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroId");

                    b.HasIndex("SubcategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Subcategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Subcategorias");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Models.CentroDeDistribuicao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("CEP")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UF")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("CentrosDeDistribuicao");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Produto", b =>
                {
                    b.HasOne("Ellen_Falpus_CadCategoria.Modelos.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ellen_Falpus_CadCategoria.Models.CentroDeDistribuicao", "CentroDeDistribuicao")
                        .WithMany("Produtos")
                        .HasForeignKey("CentroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ellen_Falpus_CadCategoria.Modelos.Subcategoria", "Subcategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubcategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("CentroDeDistribuicao");

                    b.Navigation("Subcategoria");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Subcategoria", b =>
                {
                    b.HasOne("Ellen_Falpus_CadCategoria.Modelos.Categoria", "Categoria")
                        .WithMany("Subcategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Categoria", b =>
                {
                    b.Navigation("Produtos");

                    b.Navigation("Subcategorias");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Modelos.Subcategoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("Ellen_Falpus_CadCategoria.Models.CentroDeDistribuicao", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}