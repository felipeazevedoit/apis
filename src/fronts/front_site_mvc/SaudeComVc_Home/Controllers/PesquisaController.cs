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
    public class PesquisaController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;


        //public async Task<ActionResult> Index(string texto)
        //{
        //    var pesquisa = await PesquisarAsync(texto);


        //    return View(pesquisa);
        //}

        [HttpGet]
        [ActionName("Resultado")]
        public async Task<ActionResult> PesquisarAsync(string texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    var envio = new
                    {
                        idCliente = 12,
                        texto,
                    };

                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                    var helper = new ServiceHelper();
                    var resultMedicos = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl,
                           $"/Seguranca/WpMedicos/Pesquisar/{ 12 }/{ 999 }", envio);

                    var uc = new UsuariosController();
                    var usuarios = await uc.BuscarUsuariosPorIdsAsync(resultMedicos.Select(x => x.IdUsuario));


                    IList<PesquisaViewModel> pesquisaResult = new List<PesquisaViewModel>();

                    //foreach (var item in usuarios)
                    //{
                    //    item.IdEmpresa = resultMedicos.ElementAtOrDefault()
                    //    pesquisaResult.Add(new PesquisaViewModel() { Nome = item.Nome, Descricao = item.Login, Extensao = item.AvatarExtension, ImagemB64 = item.ProfileAvatar, Url = "/Medico/WhiteLabel/" + item.ID + "?nome=" + item.Nome });
                    //}

                    for (int i = 0; i < usuarios.Count(); i++)
                    {
                        usuarios.ElementAtOrDefault(i).IdEmpresa = resultMedicos.ElementAtOrDefault(i).ID;
                        pesquisaResult.Add(new PesquisaViewModel() { Nome = usuarios.ElementAtOrDefault(i).Nome, Descricao = usuarios.ElementAtOrDefault(i).Login, Extensao = usuarios.ElementAtOrDefault(i).AvatarExtension, ImagemB64 = usuarios.ElementAtOrDefault(i).ProfileAvatar, Url = "/Medico/WhiteLabel/" + usuarios.ElementAtOrDefault(i).ID + "?nome=" + usuarios.ElementAtOrDefault(i).Nome });
                    }

                    var resultNoticias = await helper.PostAsync<IEnumerable<NoticiaViewModel>>(keyUrl,
                           $"/Seguranca/WpNoticias/Pesquisar/{ 12 }/{ 999 }", envio);

                    var nc = new MidiasController();


                    foreach (var item in resultNoticias)
                    {
                        var midias = await nc.BuscarMidiaPesquisaAsync(item.ID);
                        pesquisaResult.Add(new PesquisaViewModel()
                        {
                            Nome = item.Nome,
                            Descricao = item.Descricao,
                            Extensao = midias.Extensao,
                            ImagemB64 = Convert.ToBase64String(midias.Arquivo),
                            Url = "/Noticias/Index/" + item.ID
                        });
                    }

                    return View("Index", pesquisaResult);
                }

                return View("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<ActionResult> PesquisarJsonAsync(string texto)
        {
            try
            {
                if (!string.IsNullOrEmpty(texto))
                {
                    var envio = new
                    {
                        idCliente = 12,
                        texto,
                    };

                    var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                    var helper = new ServiceHelper();
                    var resultMedicos = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl,
                           $"/Seguranca/WpMedicos/Pesquisar/{ 12 }/{ 999 }", envio);

                    var uc = new UsuariosController();
                    var usuarios = await uc.BuscarUsuariosPorIdsAsync(resultMedicos.Select(x => x.IdUsuario));

                    IList<PesquisaViewModel> pesquisaResult = new List<PesquisaViewModel>();

                    foreach (var item in usuarios)
                    {
                        pesquisaResult.Add(new PesquisaViewModel() { Nome = item.Nome, Descricao = item.Login, Extensao = item.AvatarExtension, ImagemB64 = item.ProfileAvatar });
                    }

                    var resultNoticias = await helper.PostAsync<IEnumerable<NoticiaViewModel>>(keyUrl,
                           $"/Seguranca/WpNoticias/Pesquisar/{ 12 }/{ 999 }", envio);

                    var nc = new MidiasController();
                    var midias = await nc.BuscarMidiasAsync(resultNoticias.Select(x => x.ID));

                    foreach (var item in resultNoticias)
                    {
                        pesquisaResult.Add(new PesquisaViewModel()
                        {
                            Nome = item.Nome,
                            Descricao = item.Descricao,
                            Extensao = midias.FirstOrDefault(m => m.CodigoExterno.Equals(item.ID))?.Extensao,
                            ImagemB64 = Convert.ToBase64String(midias.FirstOrDefault(m => m.CodigoExterno.Equals(item.ID))?.Arquivo)
                        });
                    }

                    return Json(pesquisaResult.Select(e => e.Nome), JsonRequestBehavior.AllowGet);
                }

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}