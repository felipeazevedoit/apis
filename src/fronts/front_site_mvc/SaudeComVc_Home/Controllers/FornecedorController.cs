using SaudeComVc_Home.Helpers;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Fornecedor", "Index", url.AbsoluteUri);

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }

        public async Task<ActionResult> _Fornecedor()
        {
            var area = await BuscarAreaAtuacaoAsync();

            return PartialView(area);
        }

        public async Task<UsuarioXPerfilViewModel> VincularPerfilAsync(int usuarioId, int perfilId, int vinculoId = 0)
        {
            try
            {
                var envio = new UsuarioXPerfilViewModel()
                {
                    Id = vinculoId,
                    DataCriacao = DateTime.UtcNow,
                    DataEdicao = DateTime.UtcNow,
                    IdPerfil = perfilId,
                    IdUsuario = usuarioId,
                    UsuarioCriacao = usuarioId,
                    UsuarioEdicao = usuarioId,
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioXPerfilViewModel>(keyUrl, $"/Perfil/SaveUsuarioXPerfil/", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }

        public async Task<IEnumerable<ServicoViewModel>> BuscarAreaAtuacaoAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<ServicoViewModel>>(url,
                    $"/Seguranca/wpProfissionais/BuscarServico/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }
    }
}