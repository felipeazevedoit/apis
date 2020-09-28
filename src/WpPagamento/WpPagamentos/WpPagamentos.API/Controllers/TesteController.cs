using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WpPagamento.Dominio;
using WpPagamentos.Entidade;

namespace WpPagamentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public async Task<string> TesteAsync()
        {
            PagamentoBO pagamento = new PagamentoBO();
            Loja loja = new Loja
            {
                Ativo = true,
                dataCriacao = DateTime.Now,
                dataEdicao = DateTime.Now,
                Descricao = "",
                idCliente = 16,
                idPedido = "1323232",
                meioPagamento = new MeioPagamento
                {
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

            await pagamento.GerarPagamentoSimplesErede(loja);

            return "";
        }
    }
}
