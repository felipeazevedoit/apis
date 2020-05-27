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
            //optionsBuilder.UseSqlServer(@"Server=TSERVICES\SQLEXPRESS;Database=Paginas;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=187.84.232.164;Initial Catalog=Paginas;Persist Security Info=True;User ID=sa;Password=StaffPro@123;");
        }
    }
}
