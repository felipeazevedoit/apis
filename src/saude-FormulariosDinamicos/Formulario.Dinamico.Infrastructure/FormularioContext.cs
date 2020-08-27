using Formulario.Dinamico.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Dinamico.Infrastructure
{
    public class FormularioContext : DbContext
    {
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<TipoResposta> TiposResposta { get; set; }
        public DbSet<Criptografia> Criptografias { get; set; }

        //public FormularioContext()
        //    : base("Data Source=187.84.232.164;Initial Catalog=FormularioDinamico;Persist Security Info=True;User ID=sa;Password=StaffPro@123;")
        public FormularioContext()
            : base(@"Server=179.188.38.126,9000;Database=FormularioDinamico;User ID=sa;Password=WebPix@2020;")
        {
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pergunta>().ToTable("Perguntas");
            modelBuilder.Entity<Resposta>().ToTable("Respostas");
            modelBuilder.Entity<TipoResposta>().ToTable("TiposResposta");
            modelBuilder.Entity<Criptografia>().ToTable("Criptografias");

            modelBuilder.Entity<Resposta>()
            .HasRequired(r => r.Pergunta)
            .WithMany(p => p.Respostas)
            .HasForeignKey(r => r.PerguntaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
