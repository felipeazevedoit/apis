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
            //optionsBuilder.UseSqlServer(@"Server=187.84.228.245;Database=WebPixSeguranca;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(@"Data Source=187.84.228.245;Initial Catalog=WebPixSeguranca;Persist Security Info=True;User ID=sa;Password=!Nm-&8;");

        }
    }
}
