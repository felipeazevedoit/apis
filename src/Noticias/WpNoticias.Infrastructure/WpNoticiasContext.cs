using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WpNoticias.Entities;

namespace WpNoticias.Infrastructure
{
    public class WpNoticiasContext : DbContext
    {
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<NoticiaXPaciente> NoticiaXPacientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WpNoticias;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source=187.84.232.164;Initial Catalog=WpNoticias;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }
    }
}
