﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Repository;
using System;

namespace Repository.Migrations
{
    [DbContext(typeof(wpContext))]
    [Migration("20180102231223_WebPixCart")]
    partial class WebPixCart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity.Carrinho", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.Property<DateTime>("dataCriacao");

                    b.Property<DateTime>("dataEdicao");

                    b.Property<int>("idCliente");

                    b.HasKey("ID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Entity.Vendedor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao");

                    b.Property<string>("Expressao");

                    b.Property<string>("Moeda");

                    b.Property<string>("Nome");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.Property<DateTime>("dataCriacao");

                    b.Property<DateTime>("dataEdicao");

                    b.Property<int>("idCliente");

                    b.HasKey("ID");

                    b.ToTable("Vendedor");
                });
#pragma warning restore 612, 618
        }
    }
}
