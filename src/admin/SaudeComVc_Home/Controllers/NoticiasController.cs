using Newtonsoft.Json;
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
    public class NoticiasController : Controller
    {
        

        // GET: Noticias
        public async Task<ActionResult> Index(int id, int? idUsuarioP)
        {
            if (idUsuarioP != null && idUsuarioP > 0)
            {
                int pUsuarioId = Convert.ToInt32(idUsuarioP);

                var pc = new PacienteController();

                var pac = await pc.BuscarPacienteAsync(pUsuarioId);

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


        public async Task<string> BuscarNoticiasPublicasAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<NoticiaViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarPublicas/12/999");

                foreach (var n in result)
                {
                    var midia = await BuscarMidiaAsync(n.ID);
                    midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                    midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                    n.Midia = midia;
                }

                var t = JsonConvert.SerializeObject(result.OrderByDescending(n => n.DataCriacao));

                return t;
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<string> BuscarNoticiasPrivadasAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<NoticiaViewModel>>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticias/12/'" + id +"'");

                foreach (var n in result)
                {
                    var midia = await BuscarMidiaAsync(n.ID);
                    if (midia != null && midia.ID > 0)
                    {
                        midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                        midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                        n.Midia = midia;
                    }
                }

                return JsonConvert.SerializeObject(result.OrderByDescending(n => n.DataCriacao));
            }
            catch (Exception e)
            {
                throw new NoticiaException("Não foi possível buscar as notícias disponíveis.");
            }
        }

        private async Task<MidiaViewModel> BuscarMidiaAsync(int? id)
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
                    $"/Seguranca/WpMidias/BuscarPorIdExterno/12/999", envio);

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
                    var midia = await BuscarMidiaAsync(n.ID);
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
                item.Midia = await BuscarMidiaAsync(item.ID);
            }

            return result;
        }
    }
}