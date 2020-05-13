using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpFinanceiro.Entities;

namespace WpFinanceiro.Infrastructure
{
    public class WpFinanceiroContext : DbContext
    {
        public DbSet<Extrato> Extratos { get; set; }
        public DbSet<Natureza> Naturezas { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=TSERVICES\SQLEXPRESS;Database=StaffProFinanceiro;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source=34.226.175.244;Initial Catalog=StaffProFinanceiro;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }
    }
}
