using Newtonsoft.Json;
using RestSharp;
using SaudeComVc_Home.Helpers;
using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    public class ClinicaController : Controller
    {
        // GET: Clinica
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Clinica", "Index", url.AbsoluteUri);

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }

        public async Task<ActionResult> _Clinica()
        {
            var especialidades = await BuscarEspecialidadesAsync();

            TempData["Especialidades"] = especialidades;


            //var usuarios = await GetUsuariosByIdPerfil(13);

            //var controllerU = new UsuariosController();

            //var usuariosAdm = await controllerU.BuscarUsuariosPorIdsAsync(usuarios.Select(p => p.IdUsuario));

            //var lista = usuarios;


            //var ids = lista.Select(x => x.IdUsuario).ToList();




            return PartialView();
        }

        public async Task<IEnumerable<UsuarioXPerfilViewModel>> GetUsuariosByIdPerfil(int idPerfil)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<UsuarioXPerfilViewModel>>(keyUrl, $"/Perfil/GetUsersIdsByPerfil/" + idPerfil);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }
        

        public async Task<ActionResult> ValidarCnpjAsync(string cnpj )
        {
            var usuario = PixCoreValues.UsuarioLogado;
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();


            var helper = new ServiceHelper();
            var empresas = await helper.GetAsync<IEnumerable<EmpresasViewModel>>(keyUrl, "/Seguranca/wpEmpresas/BuscarEmpresas/12/999");

            return Json(empresas.Where(c => c.CNPJ.Equals(cnpj)), JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<EspecialidadeViewModel>> BuscarEspecialidadesAsync()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<IEnumerable<EspecialidadeViewModel>>(keyUrl,
                "/Seguranca/wpEmpresas/BuscarEspecialidades/12/999", envio);

            return result;
        }

        [HttpGet]
        public async Task EnviarEmails()
        {
            var usuarios = await GetUsuariosByIdPerfil(13);

            var controllerU = new UsuariosController();

            var usuariosAdm = await controllerU.BuscarUsuariosPorIdsAsync(usuarios.Select(p => p.IdUsuario));

            for (int i = 0; i < usuariosAdm.Count(); i++)
            {
                try
                {
                    RestClient client = new RestClient("http://webmail.talanservices.com.br/");
                    var url = "api/Enviar";
                    RestRequest request = null;
                    request = new RestRequest(url, Method.POST);

                    var destinatario = new EmailModel(usuariosAdm.ElementAt(i).Nome, usuariosAdm.ElementAt(i).Login);
                    var remetenteEmail = "atendimento@saudecomvc.com.br";
                    var remetente = new EmailModel("Atendimento", remetenteEmail);

                    var directory = Environment.CurrentDirectory;

                    var html = System.IO.File.ReadAllText(@"C:\TempEmails\emailClinica.html");
                    html = html.Replace("NOME_USUARIO", usuariosAdm.ElementAt(i).Nome);

                    var mail = new Mail()
                    {
                        Assunto = $"Cadastro - { usuariosAdm.ElementAt(i).Nome }",
                        Remetente = remetente,
                        Html = true,
                        Mensagem = html,
                        Destinatarios = new List<EmailModel>() { destinatario },
                    };

                    var jsonToSend = JsonConvert.SerializeObject(mail);

                    request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                    var response = await client.ExecuteTaskAsync(request);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }


        }
    }
}