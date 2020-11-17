﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Paginas.Api.Infrastructure;

namespace Paginas.Api.Infrastructure.Migrations
{
    [DbContext(typeof(PaginasContext))]
    [Migration("20190222163953_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Paginas.Api.Entities.Pagina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apresentacao");

                    b.Property<bool>("Ativo");

                    b.Property<byte[]>("Banner");

                    b.Property<int>("CodigoExterno");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DateAlteracao");

                    b.Property<string>("Descricao");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Nome");

                    b.Property<int>("Status");

                    b.Property<int>("UsuarioCriacao");

                    b.Property<int>("UsuarioEdicao");

                    b.HasKey("ID");

                    b.ToTable("Paginas");
                });
#pragma warning restore 612, 618
        }
    }
}
