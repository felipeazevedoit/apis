using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaudeComVc_Home.Helpers;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SaudeComVc_Home.Helpers;
using System.Web;
using System.Security.Claims;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        public ActionResult TelaLogin()
        {
            return View();
        }

        public ActionResult EsqueceuSenha()
        {
            return View();
        }

        public async Task<ActionResult> _Login()
        {
            var url = System.Web.HttpContext.Current.Request.Url;
            var log = await Log.GerarLogAsync("Login", "Index", url.AbsoluteUri);

            return PartialView();
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                var response = Request["g-recaptcha-response"];
                var secretKey = "6LfGeLUUAAAAAKKbQJ-Z-s1o0JgVzROwtRU5itor";

                var client = new WebClient();
                var result = client.DownloadString(new Uri($"https://www.google.com/recaptcha/api/siteverify?secret={ secretKey }&response={ response }"));

                var obj = JObject.Parse(result);
                var jResult = obj.GetValue("success");

                var status = Convert.ToBoolean(jResult);

                if (status)
                {
                    if (await PixCoreValues.LoginAsync(this, model))
                    {
                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, model.IdUsuario.ToString()),
                        }, "ApplicationCookie");

                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;

                        authManager.SignIn(identity);
                        

                        if (model.idPerfil == 14)
                        {
                            TempData["loginResult"] = string.Empty;
                            return RedirectToAction("Feed", "Paciente");
                        }
                        else
                        {
                            TempData["loginResult"] = string.Empty;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["loginResult"] = "Usuario ou senha invalido!";
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    TempData["loginResult"] = "Validação de captcha falhou!";
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception e)
            {
                TempData["loginResult"] = "Houve um erro ao tentar efetuar o login.";
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Sair()
        {
            PixCoreValues.Sair();

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<string> SetUsuarioLogado(UsuarioViewModel model)
        //{
        //    try
        //    {
        //        var uc = new UsuariosController();

        //        var user = await uc.GetUsuarioAsync(model.ID);
        //        user.UsuarioXPerfil = await uc.BuscarPerfilUsuarioAsync(user.ID);

        //        PixCoreValues.AtualizarUsuario(user);
        //        return "ok";
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}

        [HttpPost]
        public async Task<string> SetUsuarioLogado(UsuarioViewModel model)
        {
            try
            {
                var uc = new UsuariosController();

                var user = await uc.GetUsuarioCadAsync(model.ID);
                user.UsuarioXPerfil = await uc.BuscarPerfilUsuarioAsync(user.ID);


                PixCoreValues.AtualizarUsuario(user);
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<ActionResult> NewPassword()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Troca de Senha", "NewPassword", url.AbsoluteUri);

            return View();
        }

        public ActionResult _EsqueciSenha()
        {
            return PartialView();
        }

        public async Task<ActionResult> ChangePassword(UsuarioViewModel model)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    usuario = model,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(keyUrl,
                    "/Seguranca/Principal/GerarNovaSenha/" + 12 + "/" + 999, envio);

                if (result.ID > 0)
                {
                    TempData["Message"] = $"{ result.Nome }, sua nova senha foi enviada com sucesso para o seu email.";
                    return RedirectToAction("EsqueceuSenha");
                }

                TempData["Message"] = $"{ result.Nome }, não foi possível alterar a sua senha.";
                return RedirectToAction("EsqueceuSenha");
            }
            catch (Exception e)
            {
                TempData["Message"] = $"Não foi possível alterar a sua senha.";
                return RedirectToAction("EsqueceuSenha");
            }
        }

        public string GetDataApi(string url, RestSharp.Method method, dynamic obj)
        {
            var restCliente = new RestSharp.RestClient(url);

            try
            {
                var restRequest = new RestSharp.RestRequest(url, method);
                restRequest.RequestFormat = RestSharp.DataFormat.Json;

                if (method == RestSharp.Method.POST)
                    restRequest.AddBody(obj);

                var result = restCliente.Execute<dynamic>(restRequest);
                if (result.IsSuccessful)
                    return result.Content;

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}