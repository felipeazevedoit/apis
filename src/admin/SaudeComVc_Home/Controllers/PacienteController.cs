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
    public class PacienteController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;

        


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

            var usuario = await BuscarUsuarioAsync(id);

            return View(_perguntas);

        }

        public async Task<ActionResult> FichaSaude(int id)
        {
            _perguntas = await GetPerguntasAsync(id);

            //var usuario = await BuscarUsuarioAsync(id);

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

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View(perguntas);
        }

        private async Task<List<PerguntaViewModel>> GetPerguntasAsync(int id)
        {
            try
            {
                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PerguntaViewModel>>("http://179.188.38.126:82/api/api/", $"Perguntas/12/" + id);

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
            string footer = "--footer-left \"Responsabilidade única e exclusiva do paciente.\" " +  "--footer-right \"Data: [date] [time]\" " + "--footer-center \"Pagina: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new ActionAsPdf("FichaSaude", new { id })
            {
                FileName = "Ficha de Saude.pdf",
                CustomSwitches = footer
            };
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
                result.SenhaConfirmada = result.Senha;

                var paciente = await BuscarPacienteAsync(result.ID);

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

                return Json(result.Where(p => p.CPF.Equals(cpf)), JsonRequestBehavior.AllowGet);
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
            catch(Exception e)
            {
                return null;
            }
        }
    }
}