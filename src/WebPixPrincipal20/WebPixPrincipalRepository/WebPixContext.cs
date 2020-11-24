using Microsoft.EntityFrameworkCore;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class WebPixContext : DbContext
    {
        public DbSet<Page> Page { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Estilo> Estilo { get; set; }
        public DbSet<Configuracao> Configuracao { get; set; }
        public DbSet<Estrutura> Estrutura { get; set; }
        public DbSet<Arquivo> Arquivo { get; set; }
        public DbSet<Tema> Tema { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=TSERVICES\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=WebPixPrincipal;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
