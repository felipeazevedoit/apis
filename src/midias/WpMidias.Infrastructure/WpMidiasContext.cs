using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WpMidias.Entities;

namespace WpMidias.Infrastructure
{
    public class WpMidiasContext : DbContext
    {
        public DbSet<Midia> Midias { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=187.84.228.245;Database=WpMidias;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source=18.229.17.132;Initial Catalog=WpMidias;Persist Security Info=True;User ID=sa;Password=!Nm-&8;");
            //optionsBuilder.UseSqlServer(@"Data Source=187.84.232.164;Initial Catalog=WpMidias;Persist Security Info=True;User ID=sa;Password=!Nm-&8;");
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WpMidias;Trusted_Connection=True;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Midia>(md =>
            {
                md.Property(m => m.Nome).HasColumnType("varchar(100)");
                md.Property(m => m.Descricao).HasColumnType("varchar(250)");
                md.Property(m => m.CaminhoFisico).HasColumnType("varchar(150)");
                md.Property(m => m.CaminhoVirtual).HasColumnType("varchar(150)");
            });

            modelBuilder.Entity<Tipo>(tp =>
            {
                tp.Property(t => t.Nome).HasColumnType("varchar(100)");
                tp.Property(t => t.Descricao).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Configuracao>(cf =>
            {
                cf.Property(c => c.Nome).HasColumnType("varchar(100)");
                cf.Property(c => c.Descricao).HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Categoria>(ct =>
            {
                ct.Property(c => c.Nome).HasColumnType("varchar(100)");
                ct.Property(c => c.Descricao).HasColumnType("varchar(250)");
            });
        }
    }
}
