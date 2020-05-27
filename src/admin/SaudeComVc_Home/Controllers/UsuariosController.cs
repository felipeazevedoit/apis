using SaudeComVc_Home.Helpers;
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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Usuarios", "Index", url.AbsoluteUri);

            var home = new HomeController();

            var codigos = await home.GetCodigosAsync();

            var medicos = await home.GetMedicosAsync(codigos);

            TempData["Medicos"] = medicos;

            return View();
        }

        public async Task<string> AlterarUsuario(UsuarioViewModel model)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                model.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;
                model.Ativo = true;
                model.IdCliente = usuario.idCliente;
                model.Status = 1;

                var envio = new
                {
                    usuario = model,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(keyUrl, "/Seguranca/Principal/salvarUsuario/" + usuario.idCliente + "/" + usuario.IdUsuario, envio);

                PixCoreValues.AtualizarUsuarioLogado(result);

                if(result.ID > 0)
                {
                    var paciente = await BuscarPacienteAsync(result.ID);

                    paciente.Nome = result.Nome;
                    paciente.SobreNome = result.SobreNome;
                    paciente.Login = model.Login;
                    paciente.Senha = model.Senha;
                    paciente.CPF = model.CPF;
                    paciente.Altura = Convert.ToDecimal(model.Altura);
                    paciente.Peso = Convert.ToDecimal(model.Peso);
                    paciente.Senha = model.Senha;
                    if (paciente.Telefone != null)
                    {
                        paciente.Telefone.ID = model.IdTel;
                        paciente.Telefone.Numero = model.Telefone;
                        paciente.Telefone.Nome = model.Nome;
                        paciente.Telefone.IdCliente = model.IdCliente;
                    }
                    if (paciente.Endereco != null)
                    {
                        paciente.Endereco.ID = model.IdEnd;
                        paciente.Endereco.CEP = model.Cep;
                        paciente.Endereco.Bairro = model.Bairro;
                        paciente.Endereco.Local = model.Rua;
                        paciente.Endereco.Cidade = model.Cidade;
                        paciente.Endereco.NumeroLocal = model.Numero;
                        paciente.Endereco.Estado = model.Estado;
                        paciente.Endereco.Uf = model.Estado;
                        paciente.Endereco.Descricao = model.Desc;
                        paciente.Endereco.IdCliente = model.IdCliente;
                        paciente.Endereco.Nome = model.Nome;
                        paciente.Endereco.IdUsuario = model.ID;
                    }

                    var pc = new PacienteController();
                    var convenios = await pc.BuscarConveniosAsync();
                    var convenioId = convenios.FirstOrDefault(c => c.Nome.Equals(model.Convenio))?.ID;

                    paciente.ConvenioId = convenioId ?? 0;

                    var resultPaciente = await AtualizarPacienteAsync(paciente);
                    return resultPaciente;
                }

                return "Não foi possível atualizar os dados do paciente.";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar o usuário";
            }
        }

        public async Task<string> AtualizarPacienteAsync(PacienteViewModel model)
        {
            try
            {
                var usuario = PixCoreValues.UsuarioLogado;
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                model.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;
                model.Ativo = true;
                model.IdCliente = usuario.idCliente;
                model.Status = 1;

                var envio = new
                {
                    paciente = model,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<UsuarioViewModel>(keyUrl, "/Seguranca/WpPacientes/SalvarPaciente/" + usuario.idCliente + "/" + usuario.IdUsuario, envio);

                if(result.ID > 0)
                {
                    return "Dados atualiados com sucesso.";
                }

                return "Não foi possível atuaizar os dados do paciente.";
            }
            catch (Exception e)
            {
                return "Não foi possível editar o usuário.";
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
                var result = await helper.PostAsync<PacienteViewModel>(keyUrl, "/Seguranca/WpPacientes/BuscaPorIdExterno/" + usuario.idCliente + "/" + usuario.IdUsuario, envio);

                return result;

            }
            catch(Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> BuscarUsuariosPorIdsAsync(IEnumerable<int> ids)
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
                var result = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(keyUrl, $"/Seguranca/Principal/BuscarUsuarios/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar os usuarios", e);
            }
        }
    }
}