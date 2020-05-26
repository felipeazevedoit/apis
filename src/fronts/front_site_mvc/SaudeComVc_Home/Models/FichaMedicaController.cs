using Rotativa;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Models
{
    public class FichaMedicaController : Controller
    {
        private static List<PerguntaViewModel> _perguntas;

        // GET: FichaMedica
        public async Task<ActionResult> _FichaMedica(int? id)
        {
            _perguntas = await GetPerguntasAsync(id);

            //var qtd = SetPages();

            //ViewBag.Quantidade = qtd;

            //var perguntas = _perguntas.Take(5);

            return PartialView(_perguntas);
        }

        public async Task<ActionResult> FichaSaude(int? id)
        {
            _perguntas = await GetPerguntasAsync(id);

            var usuario = await BuscarUsuarioAsync(id);

            return View(_perguntas);
        }

        public ActionResult PrintIndex(int id)
        {
            string footer = "--footer-right \"Data: [date] [time]\" " + "--footer-center \"Pagina: [page] of [toPage]\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"";
            return new ActionAsPdf("FichaSaude", new { id })
            {
                FileName = "Ficha de Saude.pdf",
                CustomSwitches = footer
            };
        }


        public async Task<ActionResult> FichaPDF(int? id)
        {
            _perguntas = await GetPerguntasAsync(id);

            IDictionary<PerguntaViewModel, PerguntaViewModel> perguntas = new Dictionary<PerguntaViewModel, PerguntaViewModel>();

            for (int i = 0; i < _perguntas.Count(); i++)
            {
                IDictionary<PerguntaViewModel, PerguntaViewModel> filhos = new Dictionary<PerguntaViewModel, PerguntaViewModel>();
                for (int f = 0; f < _perguntas.ElementAtOrDefault(i).PerguntasFilho.Count(); f++)
                {
                    if (i == _perguntas.ElementAtOrDefault(i).PerguntasFilho.Count() - 1)
                    {
                        filhos.Add(_perguntas.ElementAtOrDefault(i).PerguntasFilho.ElementAtOrDefault(f), new PerguntaViewModel());
                    }
                    else
                    {
                        filhos.Add(_perguntas.ElementAtOrDefault(i).PerguntasFilho.ElementAtOrDefault(f), _perguntas.ElementAtOrDefault(i).PerguntasFilho.ElementAtOrDefault(f + 1));
                    }

                    if (f % 2 == 0)
                    {
                        f = f + 1;
                    }
                }

                _perguntas.ElementAtOrDefault(i).PerguntasFilhos = filhos;



            }

            for (int i = 0; i < _perguntas.Count(); i++)
            {
                if (i == _perguntas.Count() - 1)
                {
                    perguntas.Add(_perguntas.ElementAtOrDefault(i), null);
                }
                else
                {
                    perguntas.Add(_perguntas.ElementAtOrDefault(i), _perguntas.ElementAtOrDefault(i + 1));
                }

                if (i % 2 == 0)
                {
                    i = i + 1;
                }
            }

            var usuario = await BuscarUsuarioAsync(id);

            return View(perguntas);
        }

        private async Task<List<PerguntaViewModel>> GetPerguntasAsync(int? id)
        {
            try
            {
                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PerguntaViewModel>>("http://formulario.talanservices.com.br/api/", $"Perguntas/12/" + id);

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


        public ActionResult Search(int i, int p)
        {
            var qtd = SetPages();

            ViewBag.Quantidade = qtd;

            if (_perguntas != null && _perguntas.Count() > 0)
            {

                var perguntas = _perguntas.Skip(i * p).Take(p);

                return PartialView("_FichaMedica", perguntas);
            }

            return RedirectToAction("_FichaMedica");
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

        private async Task<PacienteViewModel> BuscarPacienteAsync(int idUsuario)
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
    }
}