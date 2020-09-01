using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpInfrastructure;
using wpService.Models.PrincipalBase;
using wpService.Models.ProdutoBase;

namespace wpService
{
    public class SeguracaServ
    {
        public static async Task<bool> validaTokenAsync(string token)
        {

            RestClient client = new RestClient("http://localhost:5300/");
            var url = "/api/token/ValidaToken/" + token;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            return Convert.ToBoolean(response.Content);
        }
        public static List<ConfiguracaoWp> GetConfig(int idCliente, int idUsuario)
        {
            AcessosWP wP = new AcessosWP(idCliente, idUsuario);
            var get = wP.GetAsync("Principal", "buscarconfiguracoes");
            List<ConfiguracaoWp> retorno = get.Result.Cast<ConfiguracaoWp>().ToList();
            return retorno;
        }
        public static ProdutoWp GetProduto(int idCliente, int idUsuario, int idProduto)
        {
            AcessosWP wP = new AcessosWP(idCliente, idUsuario);
            var get = wP.GetAsync("Principal", "buscarproduto");
            List<ProdutoWp> retorno = get.Result.Cast<ProdutoWp>().ToList();

            return retorno.Where(x=> x.ID == idProduto).FirstOrDefault();

        }
    }
}
