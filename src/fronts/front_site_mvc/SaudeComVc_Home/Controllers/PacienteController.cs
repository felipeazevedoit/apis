using Rotativa;
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
    public class PacienteController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;


        [OutputCache(Duration = 100, VaryByParam = "none")]
        public async Task<ActionResult> Feed()
        {
            if (Usuario == null || Usuario.IdUsuario == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var nc = new NoticiasController();
            var noticiasPrivadas = nc.BuscarPrivadasTakeAsync(PixCoreValues.UsuarioLogado.IdUsuario, 0);

            var mc = new MedicoController();
            var medicos = await mc.GetMedicosByIdsExternosSimpleAsync(noticiasPrivadas.Select(n => n.CodigoExterno));

            foreach (var item in noticiasPrivadas)
            {
                item.Medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(item.CodigoExterno));
            }

            var convenios = await BuscarConveniosAsync();
            var pac = await BuscarPacienteAsync(Usuario.IdUsuario);

            TempData["Convenio"] = convenios.FirstOrDefault(c => c.ID.Equals(pac.ConvenioId))?.Nome;

            var perguntas = await BuscarPerguntasAsync();

            var result = perguntas.OrderBy(p => p.ID).Where(p => p.Respostas != null && p.Respostas.Count() == 0).ToList();

            _perguntas = AtribuirFilhos(result);

            return View(new FichaFeed(_perguntas) { Noticias = noticiasPrivadas});
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

        [HttpGet]
        public async Task<JsonResult> FeedPost(int lastId)
        {
            var nc = new NoticiasController();
            var noticiasPrivadas = nc.BuscarPrivadasTakeAsync(PixCoreValues.UsuarioLogado.IdUsuario, lastId);

            var mc = new MedicoController();
            var medicos = await mc.GetMedicosByIdsExternosSimpleAsync(noticiasPrivadas.Select(n => n.CodigoExterno));

            foreach (var item in noticiasPrivadas)
            {
                item.Medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(item.CodigoExterno));
            }

            var convenios = await BuscarConveniosAsync();
            var pac = await BuscarPacienteAsync(Usuario.IdUsuario);

            TempData["Convenio"] = convenios.FirstOrDefault(c => c.ID.Equals(pac.ConvenioId))?.Nome;

            JsonResult jsonResult = Json(noticiasPrivadas, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public async Task UpdateNotificacaoStatusAsync(NotificacaoViewModel notificacao)
        {
            var helper = new ServiceHelper();

            var oldNotificacao = await helper.GetAsync<NotificacaoViewModel>("http://201.73.1.17:85/", $"api/Notificacoes/{ 12 }/{ notificacao.ID }");

            oldNotificacao.DateAlteracao = DateTime.Now;
            oldNotificacao.NotificacaoStatusId = 2;

            var result = await helper.PostAsync<NotificacaoViewModel>("http://201.73.1.17:85/", "/api/Notificacoes", oldNotificacao);
        }

        [HttpPost]
        public async Task EnviarNotificacoes(UsuarioViewModel usuario)
        {
            try
            {
                var helper = new ServiceHelper();

                var notificacaoCadastro = new NotificacaoViewModel()
                {
                    Ativo = true,
                    CodigoExterno = usuario.ID,
                    Nome = "Seja Bem-Vindo",
                    Descricao = "Você agora tem acesso a conteúdos esclusívos do Imed.fit",
                    IdCliente = 12,
                    Link = "http://Imed.fit",
                    NotificacaoStatusId = 1,
                    Status = 1,
                    UsuarioCriacao = usuario.ID,
                    UsuarioEdicao = usuario.ID,
                    DataCriacao = DateTime.Now,
                    DateAlteracao = DateTime.Now,
                };

                var result = await helper.PostAsync<NotificacaoViewModel>("http://201.73.1.17:85/", "api/Notificacoes", notificacaoCadastro);

                var notificacaoTelemedicina = new NotificacaoViewModel()
                {
                    Ativo = true,
                    CodigoExterno = usuario.ID,
                    Nome = "Telemedicina",
                    Descricao = "Conheça a telemedicina junto a Teldoctor.",
                    IdCliente = 12,
                    Link = "https://teldoctor.com.br/",
                    NotificacaoStatusId = 1,
                    Status = 1,
                    UsuarioCriacao = usuario.ID,
                    UsuarioEdicao = usuario.ID,
                    DataCriacao = DateTime.Now,
                    DateAlteracao = DateTime.Now,
                };

                result = await helper.PostAsync<NotificacaoViewModel>("http://201.73.1.17:85/", "api/Notificacoes", notificacaoTelemedicina);
            }
            catch (Exception e)
            {

            }
        }

        // GET: Paciente
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Paciente", "Index", url.AbsoluteUri);

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }

        public async Task<ActionResult> FichadeSaudeVisualizar(int id)
        {
            _perguntas = await GetPerguntasAsync(id);

            //var usuario = await BuscarUsuarioAsync(id);

            return View(_perguntas);

        }

        public async Task<ActionResult> FichaSaude(int id)
        {
            _perguntas = await GetPerguntasAsync(id);

            var usuario = await BuscarUsuarioAsync(id);

            return View(_perguntas);
        }

        public async Task<ActionResult> _Paciente()
        {
            var convenios = await BuscarConveniosAsync();

            return PartialView(convenios);
        }

        public async Task<IEnumerable<ConvenioViewModel>> BuscarConveniosAsync()
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<ConvenioViewModel>>(keyUrl,
                    $"/Seguranca/WpPacientes/BuscarConvenios/{ 12 }/{ 999 }", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os convênios.", e);
            }
        }

        public async Task<ActionResult> Formulario()
        {
            if (PixCoreValues.UsuarioLogado == null || PixCoreValues.UsuarioLogado.IdUsuario == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await BuscarPerguntasAsync();

            _perguntas = new List<PerguntaViewModel>();

            var usuario = await BuscarUsuarioAsync(Usuario.IdUsuario);

            foreach (var item in result)
            {
                if (item.PerguntaPaiId == 0)
                {
                    var perguntasFilho = result.Where(p => p.PerguntaPaiId.Equals(item.ID));
                    item.PerguntasFilho = perguntasFilho;
                    _perguntas.Add(item);
                }
            }

            var qtd = SetPages();

            ViewBag.Quantidade = qtd;

            var perguntas = _perguntas.Take(3);

            return View(perguntas);
        }

        private async Task<List<PerguntaViewModel>> GetPerguntasAsync(int id)
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

        public async Task<IEnumerable<PerguntaViewModel>> BuscarPerguntasAsync()
        {
            var helper = new ServiceHelper();
            var result = await helper.GetAsync<IEnumerable<PerguntaViewModel>>("http://179.188.38.126:82/api/", $"Perguntas/12/{ PixCoreValues.UsuarioLogado.IdUsuario }");
            return result;
        }

        public async Task<ActionResult> Editar(int id)
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Paciente", "Editar", url.AbsoluteUri);

            var usuario = await BuscarUsuarioAsync(id);

            var home = new HomeController();
            var codigos = await home.GetCodigosAsync();
            var medicos = await home.GetMedicosAsync(codigos);
            var convenios = await BuscarConveniosAsync();

            if (usuario != null && usuario.ID > 0)
            {
                var paciente = await BuscarPacienteAsync(usuario.ID);
                usuario.Altura = paciente.Altura.ToString();
                usuario.Peso = paciente.Peso.ToString();
                usuario.CPF = paciente.CPF;
                usuario.Sexo = paciente.Sexo;
                usuario.Convenio = convenios.FirstOrDefault(c => c.ID.Equals(paciente.ConvenioId))?.Nome;
                usuario.Rua = paciente.Endereco?.Descricao;
                usuario.Telefone = paciente.Telefone?.Numero;
                usuario.Cep = paciente.Endereco?.CEP;
                usuario.Numero = paciente.Endereco == null ? 0 : paciente.Endereco.NumeroLocal;
                usuario.IdTel = paciente.Telefone == null ? 0 : paciente.Telefone.ID;
                usuario.IdEnd = paciente.Endereco == null ? 0 : paciente.Endereco.ID;
            }

            TempData["Medicos"] = medicos;
            TempData["Convenios"] = new SelectList(convenios.Select(x => x.Nome));

            return View(usuario);
        }

        public ActionResult PrintIndex(int id)
        {
            string footer = "--footer-left \"Responsabilidade única e exclusiva do paciente.\" " + "--footer-right \"Data: [date] [time]\" " + "--footer-center \"Pagina: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new ActionAsPdf("FichaSaude", new { id })
            {
                FileName = "Ficha de Saude.pdf",
                CustomSwitches = footer
            };
        }

        public async Task<PacienteViewModel> BuscarPacienteAsync(int idUsuario)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    usuario.idCliente,
                    codigoExterno = idUsuario,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<PacienteViewModel>(keyUrl,
                    "/Seguranca/WpPacientes/BuscaPorIdExterno/" + usuario.idCliente + "/" + usuario.IdUsuario, envio);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<UsuarioViewModel> BuscarUsuarioAsync(int id)
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
                

                var paciente = BuscarPaciente(result.ID);

                TempData["NomeFicha"] = paciente.Nome;
                TempData["Data"] = paciente.DataNascimento.ToShortDateString();
                TempData["Idade"] = (DateTime.UtcNow - paciente.DataNascimento).Days / 365;
                TempData["CPF"] = paciente.CPF;
                TempData["Peso"] = paciente.Peso;
                TempData["Altura"] = paciente.Altura;
                TempData["Foto"] = result.ProfileAvatar;
                TempData["Extensao"] = result.AvatarExtension.Replace(".", "");

                return result;
            }
            catch (Exception e)
            {
                return new UsuarioViewModel();
            }
        }

        public PacienteViewModel BuscarPaciente(int idUsuario)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    idCliente = 12,
                    codigoExterno = idUsuario,
                };

                var helper = new ServiceHelper();
                var result = helper.Post<PacienteViewModel>(keyUrl,
                    "/Seguranca/WpPacientes/BuscaPorIdExterno/" + 12 + "/" + 999, envio);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }


        [HttpPost]
        public async Task<UsuarioXPerfilViewModel> VincularPerfilAsync(int usuarioId, int perfilId, int vinculoId = 0)
        {
            try
            {
                var envio = new UsuarioXPerfilViewModel()
                {
                    Id = vinculoId,
                    DataCriacao = DateTime.UtcNow,
                    DataEdicao = DateTime.UtcNow,
                    IdPerfil = perfilId,
                    IdUsuario = usuarioId,
                    UsuarioCriacao = usuarioId,
                    UsuarioEdicao = usuarioId,
                };

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioXPerfilViewModel>(keyUrl, $"/Perfil/SaveUsuarioXPerfil/", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
            }
        }

        public async Task<ActionResult> ValidarCpfAsync(string cpf)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PacienteViewModel>>(url,
                    $"/Seguranca/WpPacientes/BuscarPacientes/12/999");

                var final = result.Where(p => p.CPF != null);

                return Json(final.Where(p => p.CPF.Equals(cpf)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
            }
        }

        public async Task<ActionResult> ValidarEmailAsync(string email)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    usuario = new
                    {
                        Login = email,
                        idCliente = 12,
                    },
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<object>(url,
                    $"/Seguranca/Principal/VerificarUsuario/12/999", envio);

                return Json(Convert.ToBoolean(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
            }
        }

        //[HttpPost]
        //public async Task<MedicoViewModel> VincularMedicoXPacienteAsync(string usuario, int idUsuario)
        //{
        //    try
        //    {
        //        var envio = new
        //        {
        //            medicoXpaciente = new MedicoViewModel()
        //            {
        //                Nome = Usuario.Nome,
        //                DataCriacao = DateTime.UtcNow,
        //                DateAlteracao = DateTime.UtcNow,
        //                UsuarioCriacao = Usuario.IdUsuario,
        //                UsuarioEdicao = Usuario.IdUsuario,
        //                Status = 1,
        //                IdCliente = 12,
        //                Ativo = true,
        //                CodigoExterno = Usuario.IdUsuario,
        //                EmailConvidado = email,
        //                Token = "",
        //                IdConvidado = 0,
        //                Visualizado = false,
        //                NomeConvidado = nome,
        //                EmailMedico = Usuario.Login
        //            }
        //        };

        //        var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

        //        var helper = new ServiceHelper();
        //        var result = await helper.PostAsync<MedicoViewModel>(keyUrl, $"/Perfil/SaveUsuarioXPerfil/", envio);

        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Não foi possível vincular o usuário ao perfil selecionado.", e);
        //    }
        //}

        public async Task<ActionResult> Search(int i, int p, int num)
        {
            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            var qtd = SetPages();

            ViewBag.Quantidade = qtd;

            if (_perguntas != null && _perguntas.Count() > 0)
            {

                var perguntas = _perguntas.Skip(i * p).Take(p);

                TempData["NumeroPagina"] = num;

                return View("Formulario", perguntas);
            }

            return RedirectToAction("Formulario");
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

        //Chamar pelo ignorar tb
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
                var result = await helper.PostAsync<RespostaViewModel>("http://179.188.38.126:82/api/", "Respostas/", resposta);

                var pergunta = _perguntas.FirstOrDefault(p => p.ID.Equals(result.PerguntaId));
                if (pergunta != null && pergunta.Respostas != null)
                {
                    pergunta.Respostas.Clear();
                    pergunta.Respostas.Add(result);
                }
                else
                {
                    var perg = _perguntas.SelectMany(p => p.PerguntasFilho).FirstOrDefault(p => p.ID.Equals(result.PerguntaId));
                    perg.Respostas.Clear();
                    perg.Respostas.Add(result);
                }

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PacienteViewModel>> BuscarPacientesAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PacienteViewModel>>(url,
                    $"/Seguranca/WpPacientes/BuscarPacientes/{ Usuario.idCliente }/{ Usuario.IdUsuario }");

                var usuarios = await GetUsuariosAsync(result.Select(p => p.CodigoExterno));

                foreach (var item in result)
                {
                    item.Login = usuarios.SingleOrDefault(u => u.ID.Equals(item.CodigoExterno))?.Login;
                }

                var perfis = await GetPerfisUsuariosAsync(result.Select(m => m.CodigoExterno));

                foreach (var item in result)
                {
                    item.IdPerfil = perfis.FirstOrDefault(p => p.IdUsuario.Equals(item.CodigoExterno))?.IdPerfil;
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
            }
        }

        public async Task<IEnumerable<PacienteViewModel>> BuscarPacientesPorGrupoAsync(int grupoId)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();

                var envio = new
                {
                    idCliente = 12,
                    grupoId,
                };

                var result = await helper.PostAsync<IEnumerable<PacienteViewModel>>(url,
                    $"/Seguranca/WpPacientes/BuscarPorGrupo/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                var usuarios = await GetUsuariosAsync(result.Select(p => p.CodigoExterno));

                foreach (var item in result)
                {
                    item.Login = usuarios.SingleOrDefault(u => u.ID.Equals(item.CodigoExterno))?.Login;
                }

                var perfis = await GetPerfisUsuariosAsync(result.Select(m => m.CodigoExterno));

                foreach (var item in result)
                {
                    item.IdPerfil = perfis.FirstOrDefault(p => p.IdUsuario.Equals(item.CodigoExterno))?.IdPerfil;
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
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

        public async Task<IEnumerable<UsuarioXPerfilViewModel>> GetPerfisUsuariosAsync(IEnumerable<int> ids)
        {
            var url = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var helper = new ServiceHelper();
            var usuariosXPerfis = await helper.PostAsync<IEnumerable<UsuarioXPerfilViewModel>>(url, "/Perfil/GetUsuariosXPerfis/", ids);

            return usuariosXPerfis;
        }

        public async Task<PacienteViewModel> BuscarPacienteNAsync(int idUsuario)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;

                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var envio = new
                {
                    usuario.idCliente,
                    codigoExterno = idUsuario,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<PacienteViewModel>(keyUrl,
                    "/Seguranca/WpPacientes/BuscaPorIdExterno/" + usuario.idCliente + "/" + usuario.IdUsuario, envio);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public IEnumerable<PacienteViewModel> BuscarPacientesPorIds(IEnumerable<int> ids)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    ids
                };

                var helper = new ServiceHelper();
                var result = helper.Post<IEnumerable<PacienteViewModel>>(keyUrl, $"/Seguranca/WpPacientes/BuscarPacientesPorIds/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar os usuarios", e);
            }
        }
    }
}