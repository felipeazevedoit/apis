using RestSharp;
using RestSharpEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WpPagamentos.API.Helper
{
    public class Seguranca
    {
        public static async Task<bool> validaTokenAsync(string token)
        {
            var list = "http://seguranca.servicepix.com.br:5300";
            RestClient client = new RestClient(list);
            var url = "/api/token/ValidaToken/" + token;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync2(request);

            return Convert.ToBoolean(response.Content);
        }

       
    }
}
