using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WpInfrastructure
{
    /// <summary>
    /// By DarkEls Owner 
    /// Contribute to our project and 
    /// help me type fewer things and
    /// also become a millionaire :)
    /// </summary>
    public class AcessosWP
    {
        #region Propiedades

        public int IDCliente { get; set; }
        public int IdUsuario { get; set; }
        public Dictionary<string, string> Config { get; set; }

        #endregion

        #region Contructors Inferiores (By Vikctor Zaun )
        /// <summary>
        /// By DarkEls Owner 
        /// Constructor inferiores League of legends Main Vicktor 
        /// </summary>
        /// <param name="CIDCliente"></param>
        /// <param name="CidUsuario"></param>
        public AcessosWP(int CIDCliente, int CidUsuario)
        {
            IDCliente = CIDCliente;
            IdUsuario = CidUsuario;
            Config.Add("Segurança", "http://seguranca.mundowebpix.com:5300/api/");
        }


        #endregion

        #region Metodos

        /// <summary>
        /// By DarkEls Owner
        /// WebPix Call Facilitator
        /// </summary>
        /// <param name="motor">Using motor</param>
        /// <param name="acao">Using Action</param>
        /// <returns></returns>
        public async Task<object[]> GetAsync(string motor, string acao)
        {
            RestClient client = new RestClient(Config.GetValueOrDefault("Segurança"));
            var url = "Seguranca/"+ motor + "/"+ acao + "/" + IDCliente + "/" + IdUsuario;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            Object[] retorno = JsonConvert.DeserializeObject<Object[]>(response.Content);

            return retorno;

        }

        #endregion





    }
}
