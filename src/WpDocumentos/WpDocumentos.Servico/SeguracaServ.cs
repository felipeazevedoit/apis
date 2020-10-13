using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpDocumentos.Servico
{
    public class SeguracaServ
    {
        public static async Task<bool> validaTokenAsync(string token)
        {
            RestClient client = new RestClient("http://seguranca.servicepix.com.br:5300/");
            var url = "/api/token/ValidaToken/" + token;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            return Convert.ToBoolean(response.Content);
        }
    }
}
