using Entity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serivce
{
    public class ProdutoServ
    {
        public static async Task<Produto> LoadProduto(int idCliente, int idUsuario, int idProduto)
        {

            RestClient client = new RestClient("http://localhost:5300/");
            var url = "api/seguranca/Produto/BuscarProdutosRetStore/" + idCliente + "/" + idUsuario;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            Produto[] Produtos = JsonConvert.DeserializeObject<Produto[]>(response.Content);

            var retorno = Produtos.Where(x => x.ID == idProduto).FirstOrDefault();
            return retorno;
        }


        public static async Task<List<Produto>> LoadProdutos(int idCliente, int idUsuario)
        {

            RestClient client = new RestClient("http://localhost:5300/");
            var url = "api/seguranca/Produto/BuscarProdutosRetStore/" + idCliente + "/" + idUsuario;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            Produto[] Produtos = JsonConvert.DeserializeObject<Produto[]>(response.Content);
            var retorno = Produtos;
            return retorno.ToList();
        }
    }
}
