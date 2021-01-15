using Microsoft.EntityFrameworkCore;
using Paginas.Api.Entities;

namespace Paginas.Api.Infrastructure
{
    public class PaginasContext : DbContext
    {
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<PaginaXPaciente> PaginaXPaciente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Paginas;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000s;Initial Catalog=Paginas;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
