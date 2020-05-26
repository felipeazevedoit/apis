using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class WhiteLabelController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;

        // GET: WhiteLabel
        // GET: WhiteLabel
        public async Task<ActionResult> Index()
        {
            var curc = new CurriculoController();

            var result = await curc.BuscarCurriculoAsync(Usuario.IdUsuario);

            if (result == null || result.Count() == 0)
            {
                TempData["curriculo"] = null;
            }
            else
            {
                TempData["curriculo"] = "a";
            }

            var model = await BuscarPaginaAsync(Usuario.IdUsuario);
            if (model != null)
            {
                var md = new MedicoController();
                var home = new HomeController();
                var codigos = await home.GetCodigosAsync();
                var medicos = await home.GetMedicosAsync(codigos);
                TempData["Medicos"] = medicos;

                var medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(Usuario.IdUsuario) || m.Nome.Equals(Usuario.Nome));

                if (medico != null)
                {
                    var nc = new NoticiasController();

                    int codE = Convert.ToInt32(medico.CodigoExterno);

                    var pagina = md.BuscarPagina(codE);
                    var galeria = await nc.BuscarMidiasAsync(codE);
                    var noticias = await nc.BuscarNoticiasAsync(codE);

                    return View(new WhiteLabelAViewModel((int)Usuario.IdUsuario, galeria, noticias, medico));
                }
                return View();
            }
            else
                return View(GerarPaginaDefault());
        }

        public async Task<ActionResult> Portifolio()
        {
            var model = await BuscarPaginaAsync(Usuario.IdUsuario);
            if (model != null)
            {
                var mC = new MidiasController();
                var codigoExterno = Usuario.IdUsuario;
                var result = await mC.BuscarGaleriaAsync(codigoExterno);
                return View(result);
            }               
            else
            {
                return View(GerarPaginaDefault());
            }
        }
        public async Task<ActionResult> Pessoal()
        {
            var model = await BuscarPaginaAsync(Usuario.IdUsuario);
            if (model != null)
                return View(model);
            else
                return View(GerarPaginaDefault());
        }

        public ActionResult _Deletar()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Cadastrar(WhiteLabelAViewModel pagina, HttpPostedFileBase imagem)
        {
            if (imagem != null)
            {
                var image = Image.FromStream(imagem.InputStream, true, true);
                var arquivo = ImageToByteArray(image);

                pagina.Banner = arquivo;
            }

            pagina.IdCliente = Usuario.idCliente;
            pagina.CodigoExterno = Usuario.IdUsuario;
            if (pagina.ID == 0)
                pagina.UsuarioCriacao = Usuario.IdUsuario;

            pagina.UsuarioEdicao = Usuario.IdUsuario;
            pagina.Status = 1;
            pagina.CodigoExterno = Usuario.IdUsuario;
            pagina.Ativo = true;


            var model = await SalvarPaginaAsync(pagina);
            if (model.ID != 0)
            {
                return RedirectToAction("WhiteLabel", "Medico", new { nome = PixCoreValues.UsuarioLogado.Nome, id = PixCoreValues.UsuarioLogado.IdUsuario });
            }
            else
                return RedirectToAction("Index");
        }

        public async Task<WhiteLabelAViewModel> BuscarPaginaAsync(int codigoExterno)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];


                var envio = new
                {
                    Usuario.idCliente,
                    codigoExterno,
                };

                var helper = new ServiceHelper();
                var ret = await helper.PostAsync<WhiteLabelAViewModel>(url,
                    $"/Seguranca/Paginas/BuscarPorCodigoExterno/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                if (ret != null)
                {
                    var midia = await BuscarMidiaLogoAsync(Usuario.IdUsuario);

                    if (midia != null)
                    {
                        ret.Logo = Convert.ToBase64String(midia.Arquivo);
                        ret.Extensao = midia.Extensao.Replace(".", "");
                    }
                }

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<WhiteLabelAViewModel> SalvarPaginaAsync(WhiteLabelAViewModel model)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new { pagina = model };

                var helper = new ServiceHelper();
                return await helper.PostAsync<WhiteLabelAViewModel>(url,
                    $"/Seguranca/Paginas/SalvarPagina/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<WhiteLabelAViewModel> GerarPaginaDefault()
        {
            try
            {
                var whitelabel = new WhiteLabelAViewModel
                {
                    CodigoExterno = Usuario.IdUsuario,
                    Nome = Usuario.Nome,
                    DataCriacao = DateTime.Now,
                    UsuarioCriacao = Usuario.idCliente,
                    IdCliente = Usuario.idCliente,
                    Ativo = true,
                    UsuarioEdicao = Usuario.IdUsuario,
                    Status = 1,
                    Descricao = "",
                    Apresentacao = "",
                    FabebookLink = "",
                    IntagramLink = "",
                    TwitterLink = "",
                    Banner = null
                };

                var url = ConfigurationManager.AppSettings["UrlAPI"];


                var envio = new { pagina = whitelabel };

                var helper = new ServiceHelper();
                var ret = await helper.PostAsync<WhiteLabelAViewModel>(url,
                             $"/Seguranca/Paginas/SalvarPagina/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public async Task<MidiaViewModel> BuscarMidiaLogoAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    codigoExterno = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                    $"/Seguranca/WpMidias/BuscarPorIdExterno/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result.FirstOrDefault(p => p.CategoriaId == 3);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a midia solicitada.", e);
            }
        }
    }
}