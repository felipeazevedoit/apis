using RestSharp;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebPixProdutos.Models;

namespace WebPixProdutos
{
    internal class AuxNotStatic
    {
        public static async Task<MotorAuxViewModel> GetInfoMotorAux(string aux, int idcliente)
        {
            try
            {
                RestClient client = new RestClient("http://localhost:5400/");
                var url = "api/motoraux/acessarmotor/" + aux;
                RestRequest request = null;
                request = new RestRequest(url, Method.GET);
                var response = await client.ExecuteTaskAsync(request);
                var lstData = JsonConvert.DeserializeObject<MotorAuxViewModel>(response.Content);
                return lstData;
            }
            catch (Exception ex)
            {
                return new MotorAuxViewModel();
            }
        }
    }
}