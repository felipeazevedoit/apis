using Mensagens.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensagens.Infrastructure
{
    public class MensagensContext :  DbContext
    {
        public DbSet<Mensagem> Mensagens { get; set; }
        public MensagensContext()
            : base("Data Source=187.84.232.164;Initial Catalog=Mensagens;Persist Security Info=True;User ID=sa;Password=!Nm-&8;")
        //public MensagensContext()
        //   : base(@"data source=187.84.228.245;database=Mensagens;Integrated Security=SSPI;persist security info=True;")
        {
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mensagem>().ToTable("Mensagens");

            base.OnModelCreating(modelBuilder);
        }
    }
}
