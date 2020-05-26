using Newtonsoft.Json;
using RestSharp;
using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class TelDoctorController : Controller
    {
        private readonly LoginViewModel _usuario = PixCoreValues.UsuarioLogado;

        // GET: TelDoctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _TelDoctor()
        {
            return PartialView();
        }

        public string VerificarResultado()
        {
            try
            {
                var result = RealizarLogin();

                if (result.Status == 400 && result.Title == "invalid_client")
                {
                    var usuarioTelDoctor = CadastroTelDoctor();
                    result = RealizarLogin();
                    return BuscarUrlAutenticacao(result);
                }
                else
                {
                    return BuscarUrlAutenticacao(result);
                }

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private string BuscarUrlAutenticacao(TelDoctorResultViewModel result)
        {
            var client = new RestClient("https://teldoctor:sucesso@qc.teldoctor.com.br/auth/api/99999");
            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            var envio = new
            {
                client_id = _usuario.Login,
                bearer = result.AccessToken,
            };

            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(envio), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return "https://teldoctor.andrologia.com.br";
            }

            return string.Empty;
        }

        private TelDoctorResultViewModel RealizarLogin()
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"];

            var TelDoctorViewModel = new TelDoctorViewModel("password", _usuario.Login, _usuario.Senha, _usuario.Login, _usuario.Senha);

            var client = new RestClient("https://apidev.mdtelemedicina.com.br/");
            var request = new RestRequest("oauth", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(TelDoctorViewModel), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            //var helper = new ServiceHelper();
            //var result = await helper.PostAsync<TelDoctorResultViewModel>("https://apidev.mdtelemedicina.com.br/", "oauth", TelDoctorViewModel);

            var result = JsonConvert.DeserializeObject<TelDoctorResultViewModel>(response.Content);
            return result;
        }

        public UsuarioTDViewModel CadastroTelDoctor()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var cadastro = new UsuarioTDViewModel( _usuario.Login, "saudecomvc", DateTime.UtcNow, _usuario.Nome, "saude", "1", _usuario.Senha);

                var client = new RestClient("https://apidev.mdtelemedicina.com.br/");
                var request = new RestRequest("autenticacao", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(cadastro), ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                //var helper = new ServiceHelper();
                //var result = await helper.PostAsync<UsuarioTDViewModel>("https://apidev.mdtelemedicina.com.br/", "autenticacao", cadastro);

                var result = JsonConvert.DeserializeObject<UsuarioTDViewModel>(response.Content);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }
    }
}