using System;
using WpPagamento.Dominio;
using WpPagamentos.Entidade;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            PagamentoBO pagamento = new PagamentoBO();
            Loja loja = new Loja
            {
                Ativo = true,
                dataCriacao = DateTime.Now,
                dataEdicao = DateTime.Now,
                Descricao = "",
                idCliente = 16,
                idPedido = 1323232,
                meioPagamento = new MeioPagamento {
                    Nome = "Erede",
                    Ativo = true,
                    Configuracao = new
                    {
                        cartao = "5448280000000007",
                        codSeg = "123",
                        MesVenc = "01",
                        AnoVen = "2021",
                        Nome = "Lucas Fernando"

                    },
                    idCliente = 16
                },
                Nome = "eRede",
                propiedades = new Propriedades
                {
                    idCliente = 16,
                    Descricao = "",
                    Ativo = true,
                    Parcela = 1,
                    Nome = "Propriedades E-Rede",
                    dataCriacao = DateTime.Now,
                    dataEdicao = DateTime.Now,
                    Meio = false,
                    Moeda = 1,
                    Recalculo = false,
                    Status = 1,
                    UsuarioCriacao = 1,
                    UsuarioEdicao = 1,
                    Valor = 100
                }
            };

            pagamento.GerarPagamentoSimplesErede(loja);
        }
    }
}
