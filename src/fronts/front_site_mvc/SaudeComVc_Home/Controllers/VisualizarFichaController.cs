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
    public class VisualizarFichaController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;

        // GET: VisualizarFicha
        public async Task<ActionResult> Index(int? id)
        {
            _perguntas = await GetPerguntasAsync(id);

            var qtd = SetPages();

            ViewBag.Quantidade = qtd;

            var perguntas = _perguntas.Take(3);

            var usuario = await BuscarUsuarioAsync(id);

            return View(perguntas);
        }

        private async Task<List<PerguntaViewModel>> GetPerguntasAsync(int? id)
        {
            try
            {
                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PerguntaViewModel>>("http://179.188.38.126:82/api/", $"Perguntas/12/" + id);

                var perguntas = new List<PerguntaViewModel>();

                foreach (var item in result)
                {
                    item.CodigoExterno = Convert.ToInt32(id);

                    if (item.PerguntaPaiId == 0)
                    {
                        var perguntasFilho = result.Where(p => p.PerguntaPaiId.Equals(item.ID));
                        item.PerguntasFilho = perguntasFilho;
                        perguntas.Add(item);

                    }
                }

                return perguntas;
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public async Task<UsuarioViewModel> BuscarUsuarioAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    idCliente = 12,
                    idUsuario = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(keyUrl, $"/Seguranca/Principal/BuscarUsuarioPorId/12/999", envio);

                TempData["NomeFc"] = result.Nome;
                TempData["FotoF"] = result.ProfileAvatar;
                TempData["ExtensaoF"] = result.AvatarExtension.Replace(".", "");
                TempData["ID"] = result.ID;

                return result;
            }
            catch (Exception e)
            {
                return new UsuarioViewModel();
            }
        }

        public async Task<ActionResult> Search(int i, int p, int? id, int num)
        {

            var qtd = SetPages();

            ViewBag.Quantidade = qtd;

            if (_perguntas != null && _perguntas.Count() > 0)
            {

                var perguntas = _perguntas.Skip(i * p).Take(p);

                var user = await BuscarUsuarioAsync(id);

                TempData["NumeroPagina"] = num;

                return View("Index", perguntas);
            }

            return RedirectToAction("Index");
        }

        private int SetPages()
        {
            var qtd = 0;

            for (int index = 1; index < _perguntas.Count(); index++)
            {
                if (index % 3 == 0)
                    qtd++;
            }

            return qtd;
        }
    }
}