using SaudeComVc_Home.Exceptions;
using SaudeComVc_Home.Helpers;
using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Home", "Index", url.AbsoluteUri);

            //var codigos = await GetCodigosAsync();

            IEnumerable<NoticiaViewModel> noticias = new List<NoticiaViewModel>();
            var nc = new NoticiasController();

            if (PixCoreValues.UsuarioLogado != null && PixCoreValues.UsuarioLogado.IdUsuario > 0)
            {
                var pc = new PacienteController();
                var perguntas = await pc.BuscarPerguntasAsync();

                var result = perguntas.OrderBy(p => p.ID).Where(p => p.Respostas != null && p.Respostas.Count() == 0).ToList();

                _perguntas = AtribuirFilhos(result);

                noticias = nc.BuscarPrivadasAsync(PixCoreValues.UsuarioLogado.IdUsuario).Take(3);

                if (PixCoreValues.UsuarioLogado.idPerfil == 14) //Paciente
                {
                    await ClassificarPaciente(perguntas.ToList());
                }
            }
            else
            {
                //TempData["Medicos"] = medicos;
                noticias = nc.BuscarPublicasAsync().Take(3);
            }

            return View(new HomeViewModel(_perguntas) { Noticias = noticias });
        }

        private List<PerguntaViewModel> AtribuirFilhos(List<PerguntaViewModel> result)
        {
            var perguntas = new List<PerguntaViewModel>();

            foreach (var item in result)
            {
                if (item.PerguntaPaiId == 0)
                {
                    var perguntasFilho = result.Where(p => p.PerguntaPaiId.Equals(item.ID));
                    item.PerguntasFilho = perguntasFilho;
                    perguntas.Add(item);
                }
            }

            return perguntas;
        }

        private async Task ClassificarPaciente(List<PerguntaViewModel> perguntas)
        {
            IList<PacienteXGrupoViewModel> pXg = new List<PacienteXGrupoViewModel>();

            var result = AtribuirFilhos(perguntas);
            var helper = new ServiceHelper();
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
            };

            var grupos = await helper.PostAsync<IEnumerable<GrupoViewModel>>(keyUrl, "/Seguranca/WpPacientes/BuscarGrupos/12/999", envio);

            if (result != null && result.Count() > 0)
            {
                foreach (var item in result)
                {
                    if (item.PerguntasFilho != null && item.PerguntasFilho.Count() > 0 && item.PerguntaPaiId == 0)
                    {
                        var qtd = Convert.ToDecimal(item.PerguntasFilho.Count());
                        var qtdAnswered = Convert.ToDecimal(item.PerguntasFilho.Where(x => "SIM".Equals(x.Respostas.FirstOrDefault()?.Descricao.ToUpper())).Count());

                        var middle = Math.Ceiling(qtd / 2);

                        if (qtdAnswered >= middle)
                        {
                            //Vai pro grupo

                            var pc = await new PacienteController().BuscarPacienteAsync(PixCoreValues.UsuarioLogado.IdUsuario);

                            pXg.Add(new PacienteXGrupoViewModel()
                            {
                                GrupoId = grupos.FirstOrDefault(x => x.PerguntasIds.Split(',').Contains(item.ID.ToString())).ID,
                                PacienteId = pc.ID,
                            });
                        }
                    }
                }

                var envio2 = new
                {
                    pacientesXGrupos = pXg,
                };

                var response = await helper.PostAsync<object>(keyUrl, "/Seguranca/WpPacientes/SalvarGrupos/12/999", envio2);
            }
        }

        public ActionResult Parceiros()
        {
            return View();
        }

        public ActionResult _Escolha()
        {
            return PartialView();
        }

        public ActionResult _News()
        {
            return PartialView();
        }

        public async Task<ActionResult> _Perguntas(int id)
        {
            try
            {
                if (_perguntas == null)
                {
                    var pc = new PacienteController();
                    var perguntas = await pc.BuscarPerguntasAsync();

                    var result = perguntas.OrderBy(p => p.ID).Where(p => p.Respostas != null && p.Respostas.Count() == 0).ToList();

                    _perguntas = AtribuirFilhos(result);
                }
                return PartialView(_perguntas.FirstOrDefault(p => p.ID.Equals(id)));
            }
            catch (Exception e )
            {

                throw e;
            }
        }

        public async Task<ActionResult> ProximaPergunta(int id)
        {
            var result = _perguntas.FirstOrDefault(p => p.ID.Equals(id));

            var pIndex = _perguntas.IndexOf(result);
            var pergunta = _perguntas.Skip(pIndex + 1).FirstOrDefault();

            if (pergunta != null)
            {
                return PartialView("_Perguntas", pergunta);
            }

            var pc = new PacienteController();
            _perguntas = (await pc.BuscarPerguntasAsync()).OrderBy(p => p.ID).ToList();

            foreach (var item in _perguntas)
            {
                if (item.PerguntaPaiId == 0)
                {
                    var perguntasFilho = _perguntas.Where(p => p.PerguntaPaiId.Equals(item.ID));
                    item.PerguntasFilho = perguntasFilho;
                    _perguntas.Add(item);
                }
            }

            throw new PerguntasException("Todas as perguntas foram respondidas.");
        }

        public ActionResult DrAdriano()
        {
            return View();
        }

        public ActionResult DrAdrianoTemp2()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Descrição da sua página...";

            var codigos = await GetCodigosAsync();

            var medicos = await GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<IEnumerable<int>> GetCodigosAsync()
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    idCliente = 12,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<int>>(keyUrl, "/Seguranca/Paginas/BuscarCodigos/" + 12 + "/" + 999, envio);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<MedicoViewModel>> GetMedicosAsync(IEnumerable<int> ids)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    idCliente = 12,
                    ids
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigosExternos/" + 12 + "/" + 999, envio);

                //return result.Where(r => r.Ativo);
                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        //public async Task<IEnumerable<UsuarioViewModel>> BuscarUsuariosAsync(IEnumerable<int> idsUsuarios)
        //{
        //    try
        //    {
        //        var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

        //        var envio = new
        //        {
        //            idCliente = 12,
        //            ids = idsUsuarios
        //        };

        //        var helper = new ServiceHelper();
        //        var result = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(keyUrl, $"/Seguranca/Principal/BuscarUsuarios/12/999", envio);


        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Não foi possível buscar o usuario.", e);
        //    }
        //}

        //public async Task<IEnumerable<EspecialidadeViewModel>> BuscarEspcsAsync(IEnumerable<int> id)
        //{
        //    try
        //    {
        //        var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

        //        var envio = new
        //        {
        //            idCliente = 12,
        //            ids = id
        //        };

        //        var helper = new ServiceHelper();
        //        var result = await helper.PostAsync<IEnumerable<EspecialidadeViewModel>>(keyUrl, $"/Seguranca/WpMedicos/BuscarEspcPorIdsAsync/12/999", envio);

        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Não foi possível buscar a espc.", e);
        //    }
        //}

        [ActionName("Responder")]
        public async Task<ActionResult> ResponderAsync(RespostaViewModel resposta)
        {
            try
            {
                resposta.CodigoExterno = PixCoreValues.UsuarioLogado.IdUsuario;
                resposta.Ativo = true;
                resposta.IdCliente = PixCoreValues.UsuarioLogado.idCliente;
                resposta.EntidadeNome = PixCoreValues.UsuarioLogado.Nome;
                resposta.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                resposta.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;
                resposta.DataCriacao = DateTime.Now;
                resposta.DateAlteracao = DateTime.Now;
                resposta.Status = 1;

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<RespostaViewModel>("http://servicepix.com.br:82/api/", "Respostas/", resposta);

                var pergunta = _perguntas.FirstOrDefault(p => p.ID.Equals(result.PerguntaId));
                if (pergunta != null && pergunta.Respostas != null)
                {
                    _perguntas.Remove(pergunta);
                }
                else
                {
                    var perg = _perguntas.SelectMany(p => p.PerguntasFilho).FirstOrDefault(p => p.ID.Equals(result.PerguntaId));
                    _perguntas.Remove(perg);
                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}