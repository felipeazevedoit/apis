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
            optionsBuilder.UseSqlServer(@"Data Source=34.67.175.232\SQLEXPRESS;Initial Catalog=WebPixPrincipal2;Persist Security Info=True;User ID=dev3;Password=WebPix@321;");
        }
    }
}
