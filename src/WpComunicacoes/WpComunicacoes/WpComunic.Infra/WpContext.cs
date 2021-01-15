using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpComunicacoes.Entidades;

namespace WpComunic.Infra.Infraestrutura
{
    public class WpContext : DbContext
    {
        public DbSet<Metodo> metodos
        {
            get; set;
        }
        public DbSet<MotorExterno> motorExternos
        {
            get; set;
        }
        public DbSet<Propriedades> propriedades
        {
            get; set;
        }
        public DbSet<Classe> classe
        {
            get; set;
        }
        public DbSet<DataBase> database
        {
            get; set;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sql-data-services.database.windows.net;Database=WpComunicacao;User ID=dev;Password=U}4ENk|g9ryRl<BWx;");
            //optionsBuilder.UseSqlServer(@"Server =localhost\SQLEXPRESS;Database=WpComunicacao;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WpComunicacao;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}