using IntegradorDeVidas.Dominio.Einstein;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace IntegradorDeVidas.Integracoes
{
    public class EinsteinIntegracao
    {
        private RestClient Client { get; set; }
        private string LoginApi { get; set; }
        private string SenhaApi { get; set; }



        public EinsteinIntegracao(TipoAcaoIntegracao tipoRequest)
        {
            Client = new RestClient(GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.UrlPadrao) + GetUrlClinetePorTipoIntegracao(tipoRequest));
            LoginApi = GetLoginApi();
            SenhaApi = GetSenhaApi();
        }

        public EinsteinIntegracao()
        {
            Client = new RestClient(GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.UrlPadrao));
            LoginApi = GetLoginApi();
            SenhaApi = GetSenhaApi();
        }



        private RestRequest GetRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"login\": \"" + LoginApi + "\",\r\n    \"senha\": \"" + SenhaApi + "\"\r\n}\r\n", ParameterType.RequestBody);
            return request;
        }

        public IRestResponse GetResponse()
        {
            return Client.Execute(GetRequest());
        }

        public IRestResponse GetResponse(RestRequest request)
        {
            return Client.Execute(request);
        }


        private string GetSenhaApi()
        {
            //aqui é para por a chavinha
            return "l14v81465aaotlmrprgnv";
        }


        public RestRequest GetRequestComToken()
        {
            var request =  new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + GetToken() + " ");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        public RestRequest RequestnAddParametro(object obj, RestRequest request)
        {
            var jsonObject = JsonConvert.SerializeObject(obj);
            request.AddParameter("application/json", jsonObject);
            return request;
        }
        public void CadastrarVidas(ImportadorVidas vidas)
        {
            try
            {
                SetClientRestCadastrarVida();
                GetResponse(RequestnAddParametro(vidas, GetRequestComToken()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
  
        }

        private void SetClientRestCadastrarVida()
        {
            this.Client = new RestClient(GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.UrlPadrao)    
                + GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.CadastroVidas));
        }
        private string GetLoginApi()
        {
            //aqui é para por a chavinha também!
            return "api.infinito";
        }

        private string GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao tipo)
        {
            return tipo switch
            {
                TipoAcaoIntegracao.UrlPadrao => "https://portalempresas-backend-qas.telemedicinaeinstein.com.br",
                TipoAcaoIntegracao.Login => "/Sessao",
                TipoAcaoIntegracao.CadastroVidas => "/vidasElegiveis/cadastro",
                _ => "",
            };
        }

        public string GetToken()
        {
           return Login().token;
        }

        public Root Login()
        {
            try
            {
                return JsonConvert.DeserializeObject<Root>(new EinsteinIntegracao(TipoAcaoIntegracao.Login).GetResponse().Content);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}
