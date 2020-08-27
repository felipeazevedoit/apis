using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class WebPixContext : DbContext
    {
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<UsuarioPermissao> UsuarioPermissao { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<UsuarioXPerfil> UsuarioXPerfil { get; set; }
        public DbSet<Entity.Log> Log { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=179.188.38.126,9000;Initial Catalog=TServices.Seguranca;Persist Security Info=True;User ID=sa;Password=WebPix@2020;");
        }
    }
}
