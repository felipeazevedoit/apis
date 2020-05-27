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

namespace SaudeComVc_Home.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel collection)
        {
            try
            {
                //var response = Request["g-recaptcha-response"];
                //var secretKey = "6Lc8RKIUAAAAAF_icWovB6ofjufe7ezfWeUap_DX";

                //var client = new WebClient();
                //var result = client.DownloadString(new Uri($"https://www.google.com/recaptcha/api/siteverify?secret={ secretKey }&response={ response }"));

                //var obj = JObject.Parse(result);

                //var status = Convert.ToBoolean(obj);

                //if (status)
                //{
                    if (await PixCoreValues.LoginAsync(collection))
                    {
                        TempData["loginResult"] = string.Empty;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["loginResult"] = "Usuario ou senha invalido!";
                        return RedirectToAction("TelaLogin", "Login");
                    }
                //}
                //else
                //{
                //    TempData["loginResult"] = "Validação de captcha falhou!";
                //    return RedirectToAction("TelaLogin", "Login");
                //}
            }
            catch (Exception e)
            {
                TempData["loginResult"] = "Houve um erro ao tentar efetuar o login.";
                return RedirectToAction("TelaLogin", "Login");
            }
        }

        public ActionResult Sair()
        {
            PixCoreValues.Sair();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public string SetUsuarioLogado(UsuarioViewModel model)
        {
            try
            {
                PixCoreValues.AtualizarUsuario(model);
                return "ok";
            }
            catch(Exception e)
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

                if(result.ID > 0)
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
    }
}