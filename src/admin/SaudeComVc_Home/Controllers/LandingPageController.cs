using SaudeComVc_Home.Models;
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
    public class LandingPageController : Controller
    {
        // GET: LandingPage
        public async Task<ActionResult> Index(string P1, string P2)
        {
            var token = P1;
            var user = P2;
            var convite = await BuscarConvitePorTokenAsync(token);
            if (convite != null && convite.ID > 0)
            {
                convite.Visualizado = true;
                var altConvite = await AlterarConviteAsync(convite);
            }
            //Response.Cookies["convite"].Value = token;
            //Response.Cookies["convite"].Expires = DateTime.Now.AddMinutes(60);

            TempData["convite"] = token;
            TempData["medico"] = P2;

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }
        
        private async Task<ConviteViewModel> BuscarConvitePorTokenAsync(string token)
        {
            try
            {
                var envio = new
                {
                    idCliente = 12,
                    conviteToken = token

                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<ConviteViewModel>(keyUrl, $"/Seguranca/Convite/BuscarPorToken/12/999/", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível achar o convite com o token passado", e);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetConvitePorTokenAsync(string token)
        {
            try
            {
                var convite = await BuscarConvitePorTokenAsync(token);

                return Json(convite, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível achar o convite com o token passado", e);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveConviteAsync(ConviteViewModel altCconvite)
        {
            try
            {
                var convite = await AlterarConviteAsync(altCconvite);

                if(TempData["convite"] != null)
                {
                    TempData["convite"] = string.Empty;
                    TempData["user"] = string.Empty;
                }
                
                return Json(convite, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível achar o convite com o token passado", e);
            }
        }

        private async Task<ConviteViewModel> AlterarConviteAsync(ConviteViewModel altConvite)
        {
            try
            {
                var envio = new
                {
                    convite = altConvite
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<ConviteViewModel>(keyUrl, $"/Seguranca/Convite/SalvarConvite/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível achar o convite com o token passado", e);
            }
        }

        [HttpPost]
        public async Task<MedicoXPaciente> VincularMedicoXPacienteAsync(string usuario, int idUsuario, int idPaciente, int idMedico)
        {
            try
            {
                var envio = new
                {
                    medicoXpaciente = new MedicoXPaciente()
                    {
                        Nome = usuario,
                        DataCriacao = DateTime.UtcNow,
                        DateAlteracao = DateTime.UtcNow,
                        UsuarioCriacao = idUsuario,
                        UsuarioEdicao = idUsuario,
                        Status = 1,
                        IdCliente = 12,
                        Ativo = true,
                        IdPaciente = idPaciente,
                        MedicoId = idMedico
                    }
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MedicoXPaciente>(keyUrl, $"/Seguranca/WpMedicos/SalvarMedicoXPaciente/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }

        [HttpGet]
        public async Task<JsonResult> SearchMedicoAsync()
        {
            try
            {
                var medico = await BuscarMedicoAsync();

              

                return Json(medico, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível achar o convite com o token passado", e);
            }
        }

        public async Task<MedicoXPaciente> BuscarMedicoAsync()
        {
            try
            {
                var envio = new
                {
                    idCliente = 12,
                    idExterno = TempData["medico"]
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MedicoXPaciente>(keyUrl, $"/Seguranca/WpMedicos/BuscarPorIdExterno/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }

        //[HttpPost]
        //public async Task<JsonResult> SaveVinculoAsync(string usuario, int idUsuario, int idPaciente)
        //{
        //    try
        //    {
        //        var vinculo = await VincularMedicoXPacienteAsync(usuario, idUsuario, idPaciente);

        //        return Json(vinculo, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Nao foi possivel fazer o vinculo", e);
        //    }
        //}
    }
}