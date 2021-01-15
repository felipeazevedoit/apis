

using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class wpContext : DbContext
    {
        public wpContext()
        {
          //  IConfiguration.LazyLoadingEnabled = false;
          //  Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<Carrinho> Cart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=WebPixCart;Trusted_Connection=True;Integrated Security = True;");
            //optionsBuilder.UseSqlServer(@"Server = 187.84.229.35; Database = WebPixCart; User Id = dev;Password = Lucas-2007");
        }

    }
}
