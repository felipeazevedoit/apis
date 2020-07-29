using Microsoft.EntityFrameworkCore;
using Entity;

namespace WebPixRepository
{
    public class WebPixInContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<MotorAux> MotorAux { get; set; }
        public DbSet<Acao> Acao { get; set; }
        public DbSet<Parametro> Parametro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=191.252.0.50;Initial Catalog=WebPixPrincipal;Persist Security Info=True;User ID=dev;Password=123456;");
        }
    }
}
