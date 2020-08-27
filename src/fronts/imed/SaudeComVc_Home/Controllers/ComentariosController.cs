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
    public class ComentariosController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;

        // GET: Comentarios
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IEnumerable<NoticiaViewModel>> BuscarNoticiasPorCodExternoAsync(int codExt)
        {
            try
            {
                var envio = new
                {
                    idCliente = 12,
                    codigoExterno = codExt
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<NoticiaViewModel>>(keyUrl, $"/Seguranca/WpNoticias/BuscarPorMedico/12/{Usuario.IdUsuario}", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível convidar", e);
            }
        }

        public async Task<IEnumerable<ComentarioViewModel>> BuscarComentarioPorIdNoticiaAsync(int idNoticia)
        {
            try
            {
                var envio = new
                {
                    idCliente = 12,
                    noticiaId = idNoticia
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<ComentarioViewModel>>(keyUrl, $"/Seguranca/WpNoticias/BuscarComentariosPorNoticia/12/{Usuario.IdUsuario}", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível convidar", e);
            }
        }

        public async Task<ActionResult> ListarComentarios(int codExt)
        {
            var noticias = await BuscarNoticiasPorCodExternoAsync(codExt);

            var comentarios = new List<IEnumerable<ComentarioViewModel>>();

            if (noticias != null)
            {
                for (int i = 0; i < noticias.Count(); i++)
                {
                    var coment = await BuscarComentarioPorIdNoticiaAsync(noticias.ElementAtOrDefault(i).ID);
                    comentarios.Add(coment);
                }

                TempData["Comentarios"] = comentarios.OrderBy(n => n.Select(e => e.DataCriacao));

            }        
            return null;
        }
    }
}