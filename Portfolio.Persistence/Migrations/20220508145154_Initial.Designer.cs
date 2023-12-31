﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Persistence.Context;

namespace Portfolio.Persistence.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    [Migration("20220508145154_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Portfolio.Domain.Conhecimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnosDeExperiencia")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cor")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAtualizacaoConhecimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Informacao")
                        .HasColumnType("TEXT");

                    b.Property<int>("MesesDeExperiencia")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conhecimento");
                });

            modelBuilder.Entity("Portfolio.Domain.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cargo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAtualizacaoPerfil")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlInstagram")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlLinkedin")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlTelegram")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("Portfolio.Domain.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataAtualizacaoProjeto")
                        .HasColumnType("TEXT");

                    b.Property<string>("Detalhes")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlGitHub")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlWebSite")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("Portfolio.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProjetoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Portfolio.Domain.Tag", b =>
                {
                    b.HasOne("Portfolio.Domain.Projeto", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProjetoId");
                });

            modelBuilder.Entity("Portfolio.Domain.Projeto", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
