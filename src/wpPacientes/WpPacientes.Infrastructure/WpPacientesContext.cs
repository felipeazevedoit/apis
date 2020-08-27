using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpPacientes.Entities;

namespace WpPacientes.Infrastructure
{
    public class WpPacientesContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<PacientesXGrupos> PacientesXGrupos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=TSERVICES\SQLEXPRESS;Database=WpPacientes;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpPacientes;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>(pa =>
            {
                pa.Property(p => p.Nome).HasColumnType("varchar(100)");
                pa.Property(p => p.Descricao).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Convenio>(co =>
            {
                co.Property(c => c.Nome).HasColumnType("varchar(100)");
                co.Property(c => c.Descricao).HasColumnType("varchar(250)");
            });
        }
    }
}
