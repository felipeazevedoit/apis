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
    [AllowAnonymous]
    public class ConviteController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;


        // GET: Convite
        public ActionResult _Convite()
        {
            return PartialView();
        }

        public async Task<ConviteViewModel> ConvidarPacienteAsync(string email, string nome)
        {
            try
            {
                var envio = new
                {
                    convite = new ConviteViewModel()
                    {
                        Nome = Usuario.Nome,
                        DataCriacao = DateTime.UtcNow,
                        DateAlteracao = DateTime.UtcNow,
                        UsuarioCriacao = Usuario.IdUsuario,
                        UsuarioEdicao = Usuario.IdUsuario,
                        Status = 1,
                        IdCliente = 12,
                        Ativo = true,
                        CodigoExterno = Usuario.IdUsuario,
                        EmailConvidado = email,
                        Token = "",
                        IdConvidado = 0,
                        Visualizado = false,
                        NomeConvidado = nome,
                        EmailMedico = Usuario.Login
                    }



                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<ConviteViewModel>(keyUrl, $"/Seguranca/Convite/SalvarConvite/12/{Usuario.IdUsuario}", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível convidar", e);
            }
        }
    }
}