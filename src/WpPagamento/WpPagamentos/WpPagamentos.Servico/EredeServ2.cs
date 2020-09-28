using eRede;
using eRede.Service.Error;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpPagamentos.Entidade;

namespace WpPagamentos.Servico
{
    public class EredeServ2
    {
        public string pv { get; set; }
        public string token { get; set; }
        public eRede.Environment environment { get; set; }

        /// <summary>
        /// Mostra uma integração com cartão de débito e autenticação.
        /// </summary>
        /// 
        /// <param name="pv"></param>
        /// <param name="token"></param>
        /// <param name="environment"></param>
        public async Task DebitAsync(string pv, string token, eRede.Environment environment)
        {
            var config = await PrincipalServ.BuscarConfiguracoesAsync(16, 999);


            pv = config.Where(x => x.Chave == "PVERede").FirstOrDefault().Valor;
            token = config.Where(x => x.Chave == "TokenErede").FirstOrDefault().Valor;
            environment = eRede.Environment.Sandbox();

            var store = new Store(pv, token, environment);
            var transaction = new Transaction
            {
                amount = 20,
                reference = "pedido" + new Random().Next(200, 10000)
            }.DebitCard(
                "2223000148400010",
                "123",
                "12",
                "2020",
                "Fulano de tal"
            );

            transaction.AddUrl("http://example.org/success", Url.THREE_D_SECURE_SUCCESS);
            transaction.AddUrl("http://example.org/failure", Url.THREE_D_SECURE_FAILURE);

            try
            {
                var response = new eRede.eRede(store).create(transaction);

                if (response.returnCode == "220")
                {
                    Console.WriteLine("Tudo certo. Redirecione o cliente para autenticação\n{0}", response.threeDSecure.url);
                }
            }
            catch (RedeException e)
            {
                Console.WriteLine("Opz[{0}]: {1}", e.error.returnCode, e.error.returnMessage);
            }
        }

        /// <summary>
        /// Mostra uma integração com cartão de crédito
        /// </summary>
        /// 
        /// <param name="pv"></param>
        /// <param name="token"></param>
        /// <param name="environment"></param>
        public async Task<string> CreditAsync(Loja loja)
        {
            var config = await PrincipalServ.BuscarConfiguracoesAsync(16, 999);

            pv = config.Where(x => x.Chave == "PVERede").FirstOrDefault().Valor;
            token = config.Where(x => x.Chave == "TokenErede").FirstOrDefault().Valor;
            environment = eRede.Environment.Sandbox();
            
            //cartao
           
            String cartao = GetPropValue(loja.meioPagamento.Configuracao, "cartao").ToString();

            //codSeg
            String codSeg = GetPropValue(loja.meioPagamento.Configuracao, "codSeg").ToString();

            //MesVenc
            String MesVenc = GetPropValue(loja.meioPagamento.Configuracao, "MesVenc").ToString();

            //AnoVen
            String AnoVen = GetPropValue(loja.meioPagamento.Configuracao, "AnoVen").ToString();

            //Nome
            String Nome = GetPropValue(loja.meioPagamento.Configuracao, "Nome").ToString();

            var store = new Store(pv, token, environment);
            int valor = Convert.ToInt32(loja.propiedades.Valor * 100);
            var transaction = new Transaction
            {
                amount = valor,
                reference = loja.idPedido,
            }.CreditCard(
                cartao,
                codSeg,
                MesVenc,
                AnoVen,
                Nome
            );

            try
            {
                var response = new eRede.eRede(store).create(transaction);

                if (response.returnCode == "00")
                {
                    return response.tid;
                }
            }
            catch (RedeException e)
            {
                Console.WriteLine("Opz[{0}]: {1}", e.error.returnCode, e.error.returnMessage);
                return "";
            }

            return "";
        }

        public object GetPropValue(object src, string propName)
        {
            JObject obj = JObject.Parse(src.ToString());
            object VA = obj[propName];

            return VA;
        }
    }
}
