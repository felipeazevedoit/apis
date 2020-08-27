using SaudeComVc_Home.Exceptions;
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
    public class MidiasController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;

        // GET: Midias
        public ActionResult Index()
        {
            return View();
        }

        public async Task<MidiaViewModel> SalvarMidiaAsync(HttpPostedFileBase fileBase, NoticiaViewModel noticia, bool atualizar)
        {
            try
            {
                var image = Image.FromStream(fileBase.InputStream, true, true);

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var oldMidia = default(MidiaViewModel);
                if (atualizar)
                {
                    var m = await BuscarMidiaAsync(noticia.ID);

                    oldMidia = m ?? new MidiaViewModel();
                }

                var arquivo = ImageToByteArray(image);

                var envio = new
                {
                    midia = new MidiaViewModel()
                    {
                        ID = atualizar ? oldMidia.ID : 0,
                        Arquivo = arquivo,
                        CategoriaId = 1,
                        CodigoExterno = noticia.ID,
                        IdCliente = Usuario.idCliente,
                        Nome = $"Thumbnail_Noticia_{ noticia.ID }",
                        TipoId = 1,
                        UsuarioCriacao = Usuario.IdUsuario,
                        UsuarioEdicao = Usuario.IdUsuario,
                        Extensao = Path.GetExtension(fileBase.FileName),
                        Descricao = fileBase.FileName,
                        Ativo = noticia.Ativo,
                        Status = noticia.Status,
                    },
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
                    $"/Seguranca/WpMidias/SalvarMidia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível salvar a mídia informada.");
            }
        }



        public async Task<MidiaViewModel> SalvarLogoAsync(MidiaViewModel mida)
        {
            try
            {
                //var image = Image.FromStream(fileBase.InputStream, true, true);

                bool atualizar = false;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var oldMidia = default(MidiaViewModel);

                var m = await BuscarMidiaLogoAsync(Usuario.IdUsuario);

                if (m == null)
                {
                    atualizar = false;
                }
                else if (m.ID > 0)
                {
                    atualizar = true;
                }

                oldMidia = m ?? new MidiaViewModel();

                //var arquivo = ImageToByteArray(image);

                var arquivo = Convert.FromBase64String(mida.ArquivoB64);
                var extensao = mida.Extensao;

                var envio = new
                {
                    midia = new MidiaViewModel()
                    {
                        ID = atualizar ? oldMidia.ID : 0,
                        Arquivo = arquivo,
                        CategoriaId = 3,
                        CodigoExterno = Usuario.IdUsuario,
                        IdCliente = Usuario.idCliente,
                        Nome = $"Logo_Medico_{ Usuario.IdUsuario }",
                        TipoId = 1,
                        UsuarioCriacao = Usuario.IdUsuario,
                        UsuarioEdicao = Usuario.IdUsuario,
                        Extensao = "." + extensao,
                        Descricao = "",
                        Ativo = true,
                        Status = 1,
                    },
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
                    $"/Seguranca/WpMidias/SalvarMidia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível salvar a mídia informada.");
            }
        }

        [HttpPost]
        public async Task<MidiaViewModel> CadastrarGaleriaAsync(HttpPostedFileBase fileBase)
        {
            try
            {
                var image = Image.FromStream(fileBase.InputStream, true, true);

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var arquivo = ImageToByteArray(image);
                var rand = new Random().Next().ToString() + Usuario.Nome.Replace(" ", "_");
                var envio = new
                {
                    midia = new MidiaViewModel()
                    {
                        Arquivo = arquivo,
                        CategoriaId = 2,
                        CodigoExterno = Usuario.IdUsuario,
                        IdCliente = Usuario.idCliente,
                        Nome = rand,
                        TipoId = 1,
                        UsuarioCriacao = Usuario.IdUsuario,
                        UsuarioEdicao = Usuario.IdUsuario,
                        Extensao = Path.GetExtension(fileBase.FileName),
                        Descricao = fileBase.FileName,
                        Ativo = true,
                        Status = 1,
                    },
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
                    $"/Seguranca/WpMidias/SalvarMidia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível salvar a mídia informada.");
            }
        }

        public ActionResult Upload()
        {
            return View();
        }

        public async Task<ActionResult> ExcluirAsync(int id)
        {
            var model = await BuscarMidiaEspecificaAsync(id);
            var inativar = await DesativarMidiaAsync(model);
            return PartialView("_ListarGaleriaAsync", await BuscarGaleriaAsync(Usuario.IdUsuario));
        }

        public async Task<PartialViewResult> _ListarGaleriaAsync()
        {
            var codigoExterno = Usuario.IdUsuario;
            var model = await BuscarGaleriaAsync(codigoExterno);
            return PartialView(model);
        }




        public async Task<IEnumerable<MidiaViewModel>> BuscarGaleriaAsync(int? codigoExterno)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    codigoExterno,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                    $"/Seguranca/WpMidias/BuscarPorIdExterno/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                foreach (var item in result)
                {
                    item.ArquivoB64 = Convert.ToBase64String(item.Arquivo);
                }

                return result.Where(c => c.CategoriaId == 2 && c.Ativo == true);
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        public async Task<MidiaViewModel> BuscarMidiaAsync(int? id)
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

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        public async Task<MidiaViewModel> BuscarMidiaPesquisaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    idCliente = 12,
                    codigoExterno = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                    $"/Seguranca/WpMidias/BuscarPorIdExterno/{ 12 }/{ 999 }", envio);

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
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
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        public async Task<IEnumerable<MidiaViewModel>> BuscarMidiasAsync(IEnumerable<int> idsExternos)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    codigos = idsExternos,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                    $"/Seguranca/WpMidias/BuscaPorCodigos/{ 12 }/{ 999 }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
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

        public async Task<MidiaViewModel> DesativarMidiaAsync(MidiaViewModel midia)
        {
            try
            {
                midia.Ativo = false;
                midia.Status = 9;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    midia
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
                    $"/Seguranca/WpMidias/SalvarMidia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }


        public async Task<MidiaViewModel> BuscarMidiaEspecificaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    midiaId = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
                    $"/Seguranca/WpMidias/BuscarMidia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }
    }
}