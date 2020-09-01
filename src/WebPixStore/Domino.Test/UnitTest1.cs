using Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebPixStore.Dominio;

namespace Domino.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async System.Threading.Tasks.Task TestMethod1Async()
        {

            //Cria um carrinho
            CarrinhoBO carrinhoBO = new CarrinhoBO();
            Carrinho carrinho = new Carrinho()
            {
                Ativo = true,
                Descricao = "teste",
                ID = 0,
                idCliente = 4,
                Menssagens = new List<string>(),
                Nome = "teste",
                Produtos = new List<Produto>(),
                Propiedades = new List<Propriedades>(),
                Status = 1,
                SubTotal = 0,
                Total = 0,
                UsuarioCriacao = 999
            };

            var a = carrinhoBO.IniciarCarrinho(carrinho);

            //Adiciona um produto
            var b = await carrinhoBO.AddProdutoAsync(4,a);

        }
    }
}
