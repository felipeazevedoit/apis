using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PrincipalServ
    {
        public static async Task<bool> SalvaArquivoAsync(int IDCliente, int IdUsuario,List<Arquivo> arquivo)
        {

            RestClient client = new RestClient("http://seguranca.mundowebpix.com:5300/api/");
            var url = "Seguranca/Principal/SalvarListaArquivo/" + IDCliente + "/" + IdUsuario;
            RestRequest request = null;

            request = new RestRequest(url, Method.POST);

            JObject obj = JObject.FromObject(arquivo);

            request.AddParameter("application/json", obj, ParameterType.RequestBody);

            var response = await client.ExecuteTaskAsync(request);

            return Convert.ToBoolean(response.Content);
        }
        public static async Task<List<Estrutura>> GetAllEstruturaAsync(int IDCliente, int IdUsuario)
        {

            RestClient client = new RestClient("http://seguranca.mundowebpix.com:5300/api/");
            var url = "Seguranca/Principal/buscarEstruturas/" + IDCliente + "/" + IdUsuario;
            RestRequest request = null;

            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            
            Estrutura[] estruturas = JsonConvert.DeserializeObject<Estrutura[]>(response.Content);
            return estruturas.ToList();
        }
    }
}
