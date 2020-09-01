using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpPagamentos.Entidade;

namespace wpPagamento.Infra
{
    public class WpContext : DbContext
    {
        public DbSet<Loja> loja { get; set; }
        public DbSet<MeioPagamento> meioPagamentos { get; set; }
        public DbSet<Propriedades> Propiedade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-9B04LJT\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpPagamentos;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
