using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpCotacao.Entidades;

namespace WpCotacao.Infra
{
    public class WpContext : DbContext
    {
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Cobertura> Coberturas { get; set; }
        public DbSet<Juridico> Juridicos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proposta> Propostas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-9B04LJT\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpProposta;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
