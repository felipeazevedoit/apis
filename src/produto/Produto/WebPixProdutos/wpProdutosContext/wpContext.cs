using Microsoft.EntityFrameworkCore;
using Entity;

namespace Repository
{
    public class WebPixContext : DbContext
    {
        public WebPixContext() : base()
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoSku> ProdutoSku { get; set; }
        public DbSet<Propiedades> Propiedades { get; set; }
        public DbSet<Preco> Preco { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=DESKTOP-9B04LJT\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Server = 187.84.229.35; Database = WebPixProdutos; User Id = dev;Password = Lucas-2007");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Propiedades do tipo serviço (nao alimientadas por banco de dados interno do motor)
            modelBuilder.Entity<Propiedades>()
                .Ignore(p => p.ArquivosVinculado)
                .Ignore(p => p.Departamento);
        }
    }
}
