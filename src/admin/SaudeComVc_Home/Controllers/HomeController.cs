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
    public class HomeController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;

        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Home", "Index", url.AbsoluteUri);

            var codigos = await GetCodigosAsync();

            var medicos = await GetMedicosAsync(codigos);

            if(PixCoreValues.UsuarioLogado != null && PixCoreValues.UsuarioLogado.IdUsuario > 0)
            {
                var pc = new PacienteController();
                var result = (await pc.BuscarPerguntasAsync()).OrderBy(p => p.ID).Where(p => p.Respostas == null || p.Respostas.Count() == 0).ToList();

                _perguntas = new List<PerguntaViewModel>();

                foreach (var item in result)
                {
                    if (item.PerguntaPaiId == 0 && !string.IsNullOrEmpty(item.Nome))
                    {
                        var perguntasFilho = result.Where(p => p.PerguntaPaiId.Equals(item.ID));
                        item.PerguntasFilho = perguntasFilho;
                        _perguntas.Add(item);
                    }
                }
            }

            TempData["Medicos"] = medicos;

            return View(new HomeViewModel(medicos, _perguntas));
        }

        public ActionResult _Escolha()
        {
            return PartialView();
        }

        public ActionResult _News()
        {
            return PartialView();
        }

        public ActionResult _Perguntas(int id)
        {
            return PartialView(_perguntas.FirstOrDefault(p => p.ID.Equals(id)));
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
                var result = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigos/" + 12 + "/" + 999, envio);
              
                return result.Where(r => r.Ativo);

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
                var result = await helper.PostAsync<RespostaViewModel>("http://formulario.talanservices.com.br/api/", "Respostas/", resposta);

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