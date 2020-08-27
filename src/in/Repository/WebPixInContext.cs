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
            
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WebPixIn;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
