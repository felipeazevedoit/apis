using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpDocumentos.Entidades;

namespace WpDocumentos.Infraestrutura
{
    public class WpContext : DbContext
    {
        public DbSet<Layout> layout { get; set; }
        public DbSet<Documento> documento { get; set; }
        public DbSet<Propriedades> propiedade { get; set; }
        public DbSet<Tradutor> tradutor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-9B04LJT\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpDocumentos;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }   
    }
}
