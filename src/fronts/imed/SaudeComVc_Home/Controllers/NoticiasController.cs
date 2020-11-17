using Newtonsoft.Json;
using SaudeComVc_Home.Exceptions;
using SaudeComVc_Home.Helpers;
using SaudeComVc_Home.Models;
using SaudeComVoce.Exceptions;
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
    public class NoticiasController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;


        #region Admin  
        [ValidateInput(false)]
        public async Task<ActionResult> CadastrarNoticia(int id = 0)
        {
            if (Usuario.idPerfil == 13)
                ViewBag.Admin = true;
            else
                ViewBag.Admin = false;

            var viewModel = new NoticiaViewModel();
            viewModel = await BuscarNoticiaPorIdAsync(id);

            IList<string> tipos = new List<string>()
            {
                "Púbico",
                "Restrito",
            };

            ViewBag.TiposNoticias = new SelectList(tipos);

            var model = viewModel ?? new NoticiaViewModel();

            model.TipoNoticia = model.Privado ? "Restrito" : "Público";

            var categorias = await BuscarCategoriasAsync();

            TempData["Categorias"] = new SelectList(categorias.Select(e => e.Nome));

            model.Categoria = categorias.SingleOrDefault(c => c.ID.Equals(model.CategoriaId))?.Nome;
            var grupos = await BuscarGruposAsync();

            TempData["Grupos"] = new SelectList(grupos.Select(x => x.Nome));

            if (viewModel != null)
            {
                var grupo = grupos.FirstOrDefault(x => x.ID.Equals(viewModel.GrupoId));

                viewModel.Grupo = grupo == null ? "Selecione" : grupo.Nome;
            }

            return View(viewModel ?? new NoticiaViewModel() { Grupo = "Selecione" });
        }

        private static async Task<IEnumerable<GrupoViewModel>> BuscarGruposAsync()
        {
            var helper = new ServiceHelper();
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
            };

            var grupos = await helper.PostAsync<IList<GrupoViewModel>>(keyUrl, "/Seguranca/WpPacientes/BuscarGrupos/12/999", envio);
            grupos.Add(new GrupoViewModel() { Nome = "Selecione" });

            return grupos;
        }

        public async Task<ActionResult> VisualizarNoticia()
        {
            return View(await BuscarNoticiasAsync());
        }

        public async Task<ActionResult> NoticiasMedico(int idMedico)
        {
            var noticias = await BuscarNoticiasAsync(idMedico);

            return View(noticias);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ListaNoticias()
        {
            if (Usuario.IdUsuario > 0)
            {
                var noticiasPrivadas = BuscarPrivadasAsync(PixCoreValues.UsuarioLogado.IdUsuario);

                var mc = new MedicoController();
                var medicos = await mc.GetMedicosByIdsExternosAsync(noticiasPrivadas.Select(n => n.CodigoExterno));

                foreach (var item in noticiasPrivadas)
                {
                    item.Medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(item.CodigoExterno));
                }

                return View(noticiasPrivadas);
            }
            else
            {
                var noticiasPublicas = BuscarPublicasAsync();

                var mc = new MedicoController();
                var medicos = await mc.GetMedicosByIdsExternosAsync(noticiasPublicas.Select(n => n.CodigoExterno));

                foreach (var item in noticiasPublicas)
                {
                    item.Medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(item.CodigoExterno));
                }

                return View(noticiasPublicas);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Cadastro")]
        public async Task<ActionResult> CadastrarAsync(NoticiaViewModel noticiaViewModel)
        {
            try
            {
                if (noticiaViewModel.Thumbnail != null)
                {
                    noticiaViewModel.IdCliente = Usuario.idCliente;
                    noticiaViewModel.Privado = "Restrito".Equals(noticiaViewModel.TipoNoticia) ? true : false;

                    if (noticiaViewModel.ID == 0)
                        noticiaViewModel.UsuarioCriacao = Usuario.IdUsuario;

                    //if (Usuario.idPerfil != 13 && noticiaViewModel.CodigoExterno != 0)
                    if (Usuario.idPerfil != 13)
                    {
                        noticiaViewModel.CodigoExterno = Usuario.IdUsuario;
                        noticiaViewModel.Ativo = false;
                        noticiaViewModel.Status = 9;
                    }

                    noticiaViewModel.UsuarioEdicao = Usuario.IdUsuario;

                    var categorias = await BuscarCategoriasAsync();
                    noticiaViewModel.CategoriaId = categorias.SingleOrDefault(c => c.Nome.Equals(noticiaViewModel.Categoria)).ID;

                    noticiaViewModel.Status = noticiaViewModel.Ativo ? 1 : 9;
                    //     noticiaViewModel.Tipo = GetTamanhos().SingleOrDefault(t => t.Tamanho.Equals(noticiaViewModel.Tamanho)).ID;
                    if (!string.IsNullOrEmpty(noticiaViewModel.Nome) && !string.IsNullOrEmpty(noticiaViewModel.Conteudo))
                    {
                        var grupos = await BuscarGruposAsync();
                        noticiaViewModel.GrupoId = grupos.FirstOrDefault(x => x.Nome.Equals(noticiaViewModel.Grupo))?.ID;

                        var noticia = await SalvarNoticiaAsync(noticiaViewModel);

                        if (noticia.ID > 0)
                        {
                            if (noticiaViewModel.Thumbnail != null)
                            {
                                var mc = new MidiasController();
                                var midia = await mc.SalvarMidiaAsync(noticiaViewModel.Thumbnail, noticia, noticiaViewModel.ID > 0);
                            }

                            await NotificarPacientesAsync(noticia);

                            return RedirectToAction("VisualizarNoticia", "Noticias");
                        }
                    }
                    else
                    {
                        TempData["noticiaResult"] = "Por favor preencha os campos!";
                        return RedirectToAction("CadastrarNoticia", "Noticias");
                    }
                    return RedirectToAction("CadastrarNoticia", noticiaViewModel);
                }
                else
                {
                    TempData["noticiaResult"] = "Erro! Cadastre uma imagem para essa notícia.";
                    return RedirectToAction("CadastrarNoticia", noticiaViewModel);
                }
                //return RedirectToAction("CadastrarNoticia", noticiaViewModel);
            }
            catch (NoticiaException e)
            {
                TempData["noticiaResult"] = e.Message;
                return RedirectToAction("CadastrarNoticia", noticiaViewModel);
            }
            catch (MidiaException e)
            {
                TempData["noticiaResult"] = e.Message;
                return RedirectToAction("CadastrarNoticia", noticiaViewModel);
            }
            catch (Exception e)
            {
                TempData["noticiaResult"] = "Não foi possível cadastrar a notícia.";
                return RedirectToAction("CadastrarNoticia", noticiaViewModel);
            }
        }

        private async Task NotificarPacientesAsync(NoticiaViewModel noticia)
        {
            var pc = new PacienteController();

            IEnumerable<PacienteViewModel> pacientes = null;

            if (noticia.GrupoId == null || noticia.GrupoId == 0)
            {
                pacientes = await pc.BuscarPacientesAsync();
            }
            else
            {
                pacientes = await pc.BuscarPacientesPorGrupoAsync((int)noticia.GrupoId);
            }

            foreach (var item in pacientes)
            {
                try
                {
                    var notificacao = new NotificacaoViewModel()
                    {
                        Ativo = true,
                        CodigoExterno = item.CodigoExterno,
                        Descricao = noticia.Descricao,
                        IdCliente = noticia.IdCliente,
                        Link = $"http://imed.fit/Noticias/Index/{ noticia.ID }",
                        Nome = noticia.Nome,
                        NotificacaoStatusId = 1,
                        Status = 1,
                        UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario,
                        UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario,
                    };

                    var helper = new ServiceHelper();
                    var result = await helper.PostAsync<NotificacaoViewModel>("http://servicepix.com.br:82/", "api/Notificacoes", notificacao);
                }
                catch (Exception e)
                {

                }
            }
        }

        public IEnumerable<TamanhoViewModel> GetTamanhos()
        {
            try
            {
                return new List<TamanhoViewModel>()
                {
                    new TamanhoViewModel(1, "Pequeno"),
                    new TamanhoViewModel(2, "Médio"),
                    new TamanhoViewModel(3, "Grande"),
                    new TamanhoViewModel(4, "Destaque")
                };
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível recuperar os tamanhos.", e);
            }
        }

        private async Task<IList<NoticiaViewModel>> BuscarNoticiasAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();

                IList<NoticiaViewModel> result = new List<NoticiaViewModel>();

                if (Usuario.idPerfil == 12) //Medico
                {
                    var envio = new
                    {
                        Usuario.idCliente,
                        codigoExterno = Usuario.IdUsuario,
                    };

                    result = await helper.PostAsync<List<NoticiaViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarPorMedico/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);
                }
                else
                {
                    result = await helper.GetAsync<List<NoticiaViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticias/{ Usuario.idCliente }/{ Usuario.IdUsuario }");
                }


                var autores = await GetUsuariosAsync(result.Select(n => n.CodigoExterno));
                var midias = await new MidiasController().BuscarMidiasAsync(result.Select(m => m.CodigoExterno));

                foreach (var item in result)
                {
                    item.Autor = autores.FirstOrDefault(a => a.ID.Equals(item.CodigoExterno))?.Nome;

                    var midia = midias.FirstOrDefault(m => m.CodigoExterno.Equals(item.ID));

                    item.Foto = midia?.ArquivoB64;
                    item.FotoExtension = midia?.Extensao;
                }

                return result.OrderByDescending(n => n.ID).ToList();
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetUsuariosAsync(IEnumerable<int> ids)
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                Usuario.idCliente,
                ids,
            };

            var helper = new ServiceHelper();
            var usuarios = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(url,
                $"/Seguranca/Principal/BuscarUsuarios/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            return usuarios;
        }

        private async Task<NoticiaViewModel> BuscarNoticiaPorIdAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    Usuario.idCliente,
                    noticiaId = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }




        public async Task<ActionResult> BuscarNoticiasEspecificaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    codigoExterno = Usuario.IdUsuario,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<NoticiaViewModel>>(keyUrl,
                    $"/Seguranca/WpNoticias/BuscarPorMedico/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                foreach (var item in result)
                {
                    var mc = new MidiasController();
                    var midia = await mc.BuscarMidiaAsync(item.ID);
                    item.Imagem = midia == null ? string.Empty : Convert.ToBase64String(midia.Arquivo);
                }

                return Json(result.Take(5), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        //public async Task<MidiaViewModel> BuscarMidia(int? id)
        //{
        //    try
        //    {
        //        var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

        //        var envio = new
        //        {
        //            Usuario.idCliente,
        //            codigoExterno = Usuario.IdUsuario,
        //        };

        //        var helper = new ServiceHelper();
        //        var result = await helper.PostAsync<MidiaViewModel>(keyUrl,
        //            $"/Seguranca/WpMidias/BuscarPorIdExterno/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

        //        var midia = result.SingleOrDefault();



        //        return midia == null ? string.Empty : Convert.ToBase64String(midia.Arquivo);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new MidiaException("Não foi possível buscar a midia solicitada.", e);
        //    }
        //}

        private async Task<NoticiaViewModel> SalvarNoticiaAsync(NoticiaViewModel noticiaViewModel)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    noticia = noticiaViewModel,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(keyUrl,
                    $"/Seguranca/WpNoticias/SalvarNoticia/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível salvar a notícia informada.", e);
            }
        }

        public async Task<ActionResult> EditarAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["ErrorMessage"] = "Não foi possível concluir o processo de edição da noticia.";
                    return RedirectToAction("VisualizarNoticia");
                }

                var noticia = await BuscarNoticiaAsync(id);

                return View("CadastrarNoticia", noticia);
            }
            catch (NoticiaException e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return RedirectToAction("VisualizarNoticia");
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = "Não foi possível concluir o processo de edição da noticia.";
                return RedirectToAction("VisualizarNoticia");
            }
        }

        public async Task<ActionResult> DesativarAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["ErrorMessage"] = "Não foi possível concluir o processo de desativação da notícia.";
                    return RedirectToAction("VisualizarNoticia");
                }

                var noticia = await BuscarNoticiaAsync(Convert.ToInt32(id));
                if (await DesativarNoticiaAsync(noticia))
                {
                    return View("VisualizarNoticia", BuscarNoticiasAsync());
                }

                return View("VisualizarNoticia", BuscarNoticiasAsync());
            }
            catch (NoticiaException e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return RedirectToAction("VisualizarNoticia");
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = "Não foi possível concluir o processo de desativação da notícia.";
                return RedirectToAction("VisualizarNoticia");
            }
        }

        private async Task<bool> DesativarNoticiaAsync(NoticiaViewModel noticia)
        {
            try
            {
                noticia.Ativo = false;
                noticia.Status = 9;

                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<object>(url,
                    $"/Seguranca/WpNoticias/Desativar/{ Usuario.idCliente }/{ Usuario.IdUsuario }");

                var response = Convert.ToBoolean(result);

                return response;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível desativar a noticia.", e);
            }
        }

        public async Task<NoticiaViewModel> BuscarNoticiaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    idUsuario = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(keyUrl,
                    $"/Seguranca/WpNoticias/BuscarPorId/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar a noticia selecionada.", e);
            }
        }

        public async Task<ActionResult> PublicarNoticiaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    Usuario.idCliente,
                    idUsuario = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(keyUrl,
                    $"/Seguranca/WpNoticias/Publicar/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                if (Convert.ToBoolean(result))
                {
                    ViewData["ErrorMessage"] = "Noticia publicada com sucesso.";
                    return RedirectToAction("VisualizarNoticia");
                }

                ViewData["ErrorMessage"] = "Não foi possível publicar a noticia selecionada.";
                return RedirectToAction("VisualizarNoticia");
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = "Não foi possível publicar a noticia selecionada.";
                return RedirectToAction("VisualizarNoticia");
            }
        }

        private async Task<IEnumerable<CategoriaViewModel>> BuscarCategoriasAsync()
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var helper = new ServiceHelper();
            var result = await helper.GetAsync<IEnumerable<CategoriaViewModel>>(keyUrl,
                "/Seguranca/WpNoticias/BuscarCategorias/" + PixCoreValues.UsuarioLogado.idCliente + "/" + PixCoreValues.UsuarioLogado.IdUsuario);

            return result;
        }

        #endregion

        #region Home



        // GET: Noticias
        public async Task<ActionResult> Index(int id, int? idUsuarioP)
        {
            if (idUsuarioP != null && idUsuarioP > 0)
            {
                int pUsuarioId = Convert.ToInt32(idUsuarioP);

                var pc = new PacienteController();

                var pac = await pc.BuscarPacienteNAsync(pUsuarioId);

                var vNxP = await BuscarVinculoAsync(pac.ID);

                if (vNxP.Count() > 0)
                {
                    for (int i = 0; i < vNxP.Count(); i++)
                    {
                        if (vNxP.ElementAtOrDefault(i).PacienteId.Equals(pac.ID) && vNxP.ElementAtOrDefault(i).NoticiaId.Equals(id))
                        {

                        }
                        else
                        {
                            var vinculo = await SalvarVinculo(id, pac.ID);
                        }
                    }
                }
                else
                {
                    var vinculo = await SalvarVinculo(id, pac.ID);
                }
            }


            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Noticias", "Index", url.AbsoluteUri);

            var n = await BuscarNoticiaAsync(id);

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View(n);

        }

        public async Task<NoticiaViewModel> BuscarNoticiaAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    noticiaId = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticia/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<NoticiaXPacienteViewModel> SalvarVinculo(int idN, int idP)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    noticiaXpaciente = new
                    {
                        noticiaId = idN,
                        pacienteId = idP
                    }
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaXPacienteViewModel>(url,
                    $"/Seguranca/WpNoticias/SalvarNoticiaXPaciente/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<IEnumerable<NoticiaXPacienteViewModel>> BuscarVinculoAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    pacienteId = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<NoticiaXPacienteViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticiaPacienteIdP/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<string> BuscarFotoAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    idUsuario = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(url,
                    $"/Seguranca/Principal/BuscarUsuarioPorId/12/999", envio);

                if (!string.IsNullOrEmpty(result.AvatarExtension))
                {
                    result.AvatarExtension = result.AvatarExtension.Replace(".", string.Empty);
                }

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> BuscarFotosAsync(IEnumerable<int> ids)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    ids,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(url,
                    $"/Seguranca/Principal/BuscarUsuarios/12/999", envio);

                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.AvatarExtension))
                    {
                        item.AvatarExtension = item.AvatarExtension.Replace(".", string.Empty);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }


        public async Task<UsuarioViewModel> BuscarFotoComentarioAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    idUsuario = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(url,
                    $"/Seguranca/Principal/BuscarUsuarioPorId/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<ActionResult> BuscarComentariosAsync(int id)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    noticiaId = id,

                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<ComentarioViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarComentariosPorNoticia/12/999", envio);

                var users = await BuscarFotosAsync(result.Select(x => x.UsuarioCriacao));

                foreach (var n in result)
                {
                    n.profileAvatar = users.FirstOrDefault(x => x.ID.Equals(n.UsuarioCriacao))?.ProfileAvatar;
                    n.AvatarExtension = users.FirstOrDefault(x => x.ID.Equals(n.UsuarioCriacao))?.AvatarExtension;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }


        public string BuscarNoticiasPublicasAsync()
        {
            try
            {
                var result = BuscarPublicasAsync();

                var t = JsonConvert.SerializeObject(result.OrderByDescending(n => n.DataCriacao));

                return t;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public IEnumerable<NoticiaViewModel> BuscarPublicasAsync()
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"];

            var helper = new ServiceHelper();
            //var result = await helper.GetAsync<IEnumerable<NoticiaViewModel>>(url,
            //    $"/Seguranca/WpNoticias/BuscarPublicas/12/999");

            var serviceConsuming = new ConsumingApiRest(url, string.Empty);
            var result = serviceConsuming.Execute<List<NoticiaViewModel>>("/Seguranca/WpNoticias/BuscarPublicas/12/999", null, RestSharp.Method.GET, RestSharp.ParameterType.QueryString);

            //TODO: Achar uma forma de melhorar essa busca
            if (result != null)
            {
                result
                  .ForEach(n =>
                  {
                      var midia = BuscarMidiaAsync(n.ID);
                      if (midia != null)
                      {
                          midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                          midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                          n.Midia = midia;
                      }
                  });
                return result.Where(n => n.Ativo);
            }
            else
            {
                return null;
            }
            //foreach (var n in result)
            //{
            //    var midia = await BuscarMidiaAsync(n.ID);
            //    midia.Extensao = midia.Extensao.Replace(".", string.Empty);
            //    midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
            //    n.Midia = midia;
            //}
           
            
        }

        public string BuscarNoticiasPrivadasAsync(int id)
        {
            try
            {
                var result = BuscarPrivadasAsync(id);

                return JsonConvert.SerializeObject(result.OrderByDescending(n => n.DataCriacao));
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public IEnumerable<NoticiaViewModel> BuscarPrivadasAsync(int id)
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"];

            //var helper = new ServiceHelper();
            //var result = await helper.GetAsync<IEnumerable<NoticiaViewModel>>(url,
            //    $"/Seguranca/WpNoticias/BuscarNoticias/12/'" + id + "'");

            var consumingApi = new ConsumingApiRest(url, string.Empty);
            var result = consumingApi.Execute<IEnumerable<NoticiaViewModel>>($"/Seguranca/WpNoticias/BuscarNoticias/12/{id}", null, RestSharp.Method.GET, RestSharp.ParameterType.QueryString);

            foreach (var n in result)
            {
                var midia = BuscarMidiaAsync(n.ID);
                if (midia != null && midia.ID > 0)
                {
                    midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                    midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                    n.Midia = midia;
                }
            }

            return result.Where(n => n.Ativo);
        }

        public async Task<List<NoticiaViewModel>> BuscarPrivadasTakeAsync(int id, int lastid, int take = 10)
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"];

            var helper = new ServiceHelper();
            var result2 = await helper.GetAsync<IEnumerable<NoticiaViewModel>>(url,
                $"/Seguranca/WpNoticias/BuscarNoticias/12/'" + id + "'");

            var envio = new List<string>();
            envio.Add("lastid");
            envio.Add("take");
            envio.Add("idCliente");

            var consumingApi = new ConsumingApiRest(url, string.Empty);
            var result = consumingApi.Execute<IEnumerable<NoticiaViewModel>>($"/Seguranca/WpNoticias/GetTake/12/{id}", new
            {
                lastid = lastid,
                take = take,
                idCliente = 12
            },
                RestSharp.Method.POST, RestSharp.ParameterType.RequestBody, envio);

            //foreach (var n in result)
            //{
            //    var midia = BuscarMidiaAsync(n.ID);
            //    if (midia != null && midia.ID > 0)
            //    {
            //        midia.Extensao = midia.Extensao.Replace(".", string.Empty);
            //        midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
            //        n.Midia = midia;
            //    }
            //}

            return result2.Where(n => n.Ativo).Take(10).ToList();
        }

        private MidiaViewModel BuscarMidiaAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    idCliente = 12,
                    codigoExterno = id,
                };

                //var helper = new ServiceHelper();
                //var result2 = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                //    $"/Seguranca/WpMidias/BuscarPorIdExterno/12/999", envio);

                var serviceApi = new ConsumingApiRest(keyUrl, string.Empty);
                var result = serviceApi.Execute<List<MidiaViewModel>>("/Seguranca/WpMidias/BuscarPorIdExterno/12/999", envio, RestSharp.Method.POST, RestSharp.ParameterType.RequestBody);

                return result?.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        private async Task<MidiaViewModel> BuscarMidiaTakeAsync(int? id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                //var envio = new
                //{
                //    idCliente = 12,
                //    codigoExterno = id,
                //    lastid = 0,
                //    take = 1
                //};

                ////var helper = new ServiceHelper();
                ////var result2 = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl,
                ////    $"/Seguranca/WpMidias/BuscarPorIdExterno/12/999", envio);

                //var serviceApi = new ConsumingApiRest(keyUrl, string.Empty);
                //var result = serviceApi.Execute<List<MidiaViewModel>>("/Seguranca/WpMidias/GetByCodExternoFromTake/12/999/0/1", envio, RestSharp.Method.POST, RestSharp.ParameterType.RequestBody);

                var envio = new List<string>();
                envio.Add("lastid");
                envio.Add("take");
                envio.Add("idCliente");
                envio.Add("codigoExterno");

                var consumingApi = new ConsumingApiRest(keyUrl, string.Empty);
                var result = consumingApi.Execute<List<MidiaViewModel>>($"/Seguranca/WpMidias/GetByCodExternoFromTake/12/{Usuario.IdUsuario}", new { lastid = 0, take = 1, idCliente = 12, codigoExterno = id },
                    RestSharp.Method.POST, RestSharp.ParameterType.RequestBody, envio);

                return result.SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar a midia solicitada.", e);
            }
        }

        public async Task<ComentarioViewModel> InserirComentarioAsync(string nome, string descricao, string idUsuario, string noticiaId)
        {
            try
            {
                var envio = new ComentarioViewModel()
                {
                    Nome = nome,
                    Descricao = descricao,
                    DataCriacao = DateTime.UtcNow,
                    DateAlteracao = DateTime.UtcNow,
                    UsuarioCriacao = Convert.ToInt32(idUsuario),
                    UsuarioEdicao = Convert.ToInt32(idUsuario),
                    Ativo = true,
                    IdCliente = 12,
                    Status = 1,
                    NoticiaId = Convert.ToInt32(noticiaId)
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<ComentarioViewModel>(keyUrl, $"/Seguranca/WpNoticias/SalvarComentario/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }

        public async Task<ActionResult> BuscarNoticiaRelacionadaAsync(NoticiaObject noticia)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    PixCoreValues.UsuarioLogado.idCliente,
                    noticia.codigoExterno,
                    tags = noticia.NoticiaTags.Split(','),
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<NoticiaViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarRelacionadas/12/999", envio);

                foreach (var n in result)
                {
                    var midia = BuscarMidiaAsync(n.ID);
                    midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                    midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                    n.Midia = midia;
                }


                return Json(result.OrderBy(n => n.DataCriacao), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<IEnumerable<MidiaViewModel>> BuscarMidiasAsync(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
                codigoExterno = id,
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<IEnumerable<MidiaViewModel>>(keyUrl, $"/Seguranca/WpMidias/BuscarPorIdExterno/12/999", envio);

            return result.Where(c => c.CategoriaId == 2 && c.Ativo);
        }

        public async Task<IEnumerable<NoticiaViewModel>> BuscarNoticiasAsync(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
                codigoExterno = id,
            };

            var helper = new ServiceHelper();
            var result = (await helper.PostAsync<IEnumerable<NoticiaViewModel>>(keyUrl, $"/Seguranca/WpNoticias/BuscarPorMedico/12/999", envio)).Where(n => n.Ativo);

            foreach (var item in result)
            {
                var midia = BuscarMidiaAsync(item.ID);
                midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                item.Midia = midia;
            }

            return result;
        }
        #endregion

    }
}