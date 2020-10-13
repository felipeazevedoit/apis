using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpDocumentos.Servico.Model;

namespace WpDocumentos.Servico
{
    public class PrincipalServ
    {
        public static async Task<List<Configuracao>> BuscarConfiguracoesAsync(int IDCliente, int IdUsuario)
        {
            try
            {
                RestClient client = new RestClient("http://seguranca.servicepix.com.br:5300/api/");
                var url = "Seguranca/Principal/buscarconfiguracoes/" + IDCliente + "/" + IdUsuario;
                RestRequest request = null;
                request = new RestRequest(url, Method.GET);
                var response = await client.ExecuteTaskAsync(request);
                var lstData = JsonConvert.DeserializeObject<List<Configuracao>>(response.Content.ToString());
                return lstData;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
