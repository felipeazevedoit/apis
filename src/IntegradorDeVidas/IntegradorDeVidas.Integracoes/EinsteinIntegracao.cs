using IntegradorDeVidas.Dominio.Einstein;
using RestSharp;
using System;
using System.Linq;
using System.Text.Json;

namespace IntegradorDeVidas.Integracoes
{
    public class EinsteinIntegracao
    {
        private RestClient Client { get; set; }
        private string LoginApi { get; set; }
        private string SenhaApi { get; set; }
        private string Url { get; set; }

        public string Token { get; set; }

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
            Url = GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.UrlPadrao);
            Token = GetToken();
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
            var jsonObject = JsonSerializer.Serialize(obj);
            request.AddParameter("application/json", jsonObject);
            return request;
        }
        public void CadastrarVidas(ImportadorVidas vidas)
        {
            try
            {
                Console.WriteLine("Iniciando processo de importação de vidas");
                if(vidas.ValidarListaDeVidas().Count() == 0)
                {
                    Url += GetUrlClinetePorTipoIntegracao(TipoAcaoIntegracao.CadastroVidas);
                    
                    Console.WriteLine("URL: "+Url);
                    Console.WriteLine("");
                    Console.WriteLine("USER: " + GetLoginApi());
                    Console.WriteLine("");
                    Console.WriteLine("Token: " + Token);
                    Console.WriteLine("");

                    var data = ConverterVidasToJson(vidas);
                    Console.WriteLine("DATA: " + data);

                    var client = new RestClient(Url)
                    {
                        Timeout = -1
                    };
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", Token);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", data, ParameterType.RequestBody);
                    Console.WriteLine("");

                    Console.WriteLine("===========================================");

                    Console.WriteLine(JsonSerializer.Serialize(request));

                    IRestResponse response = client.Execute(request);
                    Console.WriteLine("RESPONSE: " + response.Content);
                    var result = JsonSerializer.Deserialize<ResultRequest>(response.Content);

                    Console.WriteLine("RESULT: "+ result);
                    if (result != null)
                    {
                        //gravar futuramente no banco
                        Console.WriteLine(result);
                    }
                }
                else
                {
                    foreach (var item in vidas.ValidarListaDeVidas())
                    {
                        Console.WriteLine(item);
                    }
                }
      


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
  
        }


        private string ConverterVidasToJson(ImportadorVidas vidas)
        {

            return JsonSerializer.Serialize(vidas);
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
           var token = Login();
            return token.token;
        }

        public Root Login()
        {
            try
            {
                return JsonSerializer.Deserialize<Root>(new EinsteinIntegracao(TipoAcaoIntegracao.Login).GetResponse().Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

    }
}
