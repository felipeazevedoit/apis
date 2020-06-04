using DomainBusiness;
using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1Async()
        {
            Estrutura estrutura = new Estrutura { ID = 2 };
            Arquivo arquivo = new Arquivo {  ID = 7 };

            var list = new List<Estrutura>();
            var list2 = new List<Arquivo>();

            list.Add(estrutura);
            list2.Add(arquivo);
            list2.Add(arquivo);

            Propiedades propiedades = new Propiedades
            {
                ArquivosVinculado = list2,
                Ativo = true,
                dataCriacao = DateTime.Now,
                dataEdicao = DateTime.Now,
                Departamento = list,
                Descricao = "TEste",
                idCliente = 1,
                Nome = "Teste",
                Status = 1,
                UsuarioCriacao = 1,
                UsuarioEdicao = 1
            };

            await PropiedadesBO.SaveAsync(propiedades, "aaaaaa");

        }
    }
}
