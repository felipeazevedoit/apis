using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpMedicos.Entities;

namespace WpMedicos.Infrastructure
{
    public class WpMedicosContext : DbContext
    {
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<MedicoXEspecialidade> MedicosXEspecialidades { get; set; }
        public DbSet<MedicoXPaciente> MedicoXPacientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<MedicoXClinicas> MedicoXClinicas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WpMedicos;Trusted_Connection=True;Integrated Security = True;");
           // optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpMedicos;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Medico>(op =>
            {
                op.Property(o => o.Nome).HasColumnType("varchar(100)");
                op.Property(o => o.Descricao).HasColumnType("varchar(250)");
            });
            modelBuilder.Entity<Especialidade>(ep =>
            {
                ep.Property(e => e.Nome).HasColumnType("varchar(100)");
                ep.Property(e => e.Descricao).HasColumnType("varchar(250)");
            });
        }
    }
}
