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
            optionsBuilder.UseSqlServer(@"Data Source=191.252.0.50;Initial Catalog=WebPixSeguranca;Persist Security Info=True;User ID=dev;Password=123456;");
        }
    }
}
