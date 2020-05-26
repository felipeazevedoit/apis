﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpLogs.Infrastructure;

namespace WpLogs.Infrastructure.Migrations
{
    [DbContext(typeof(WpLogsContext))]
    partial class WpLogsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WpLogs.Entities.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao");

                    b.Property<int>("IdCliente");

                    b.Property<int>("Status");

                    b.Property<string>("UrlAcessada");

                    b.Property<int>("UsuarioCriacao");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
