﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpComunic.Infra.Infraestrutura;

namespace WpComunic.Infra.Migrations
{
    [DbContext(typeof(WpContext))]
    [Migration("20201217154600_essa_foi_dificil")]
    partial class essa_foi_dificil
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WpComunicacoes.Entidades.Classe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("classe");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Funcao", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Funcao");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Metodo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int?>("ClasseEntradaID")
                        .HasColumnType("int");

                    b.Property<int?>("ClasseSaidaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endpoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MotorExternoID")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClasseEntradaID");

                    b.HasIndex("ClasseSaidaID");

                    b.HasIndex("MotorExternoID");

                    b.ToTable("metodos");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.MotorExterno", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int?>("funcaoID")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.Property<string>("saida")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("funcaoID");

                    b.ToTable("motorExternos");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Propriedades", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<int?>("ClasseID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeExterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("TradutorID")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("valor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ClasseID");

                    b.HasIndex("TradutorID");

                    b.ToTable("propriedades");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Tradutor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCriacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioEdicao")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.Property<int?>("metodoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("metodoID");

                    b.ToTable("tradutor");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Metodo", b =>
                {
                    b.HasOne("WpComunicacoes.Entidades.Classe", "ClasseEntrada")
                        .WithMany()
                        .HasForeignKey("ClasseEntradaID");

                    b.HasOne("WpComunicacoes.Entidades.Classe", "ClasseSaida")
                        .WithMany()
                        .HasForeignKey("ClasseSaidaID");

                    b.HasOne("WpComunicacoes.Entidades.MotorExterno", null)
                        .WithMany("metodo")
                        .HasForeignKey("MotorExternoID");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.MotorExterno", b =>
                {
                    b.HasOne("WpComunicacoes.Entidades.Funcao", "funcao")
                        .WithMany()
                        .HasForeignKey("funcaoID");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Propriedades", b =>
                {
                    b.HasOne("WpComunicacoes.Entidades.Classe", null)
                        .WithMany("propriedades")
                        .HasForeignKey("ClasseID");

                    b.HasOne("WpComunicacoes.Entidades.Tradutor", null)
                        .WithMany("propriedades")
                        .HasForeignKey("TradutorID");
                });

            modelBuilder.Entity("WpComunicacoes.Entidades.Tradutor", b =>
                {
                    b.HasOne("WpComunicacoes.Entidades.Metodo", "metodo")
                        .WithMany()
                        .HasForeignKey("metodoID");
                });
#pragma warning restore 612, 618
        }
    }
}
