using SaudeComVc_Home.Exceptions;
using SaudeComVc_Home.Helpers;
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
    public class UsuariosController : Controller
    {
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;

        public async Task<ActionResult> EditarAsync(int? id)
        {
            try
            {
                var result = await BuscarPerfisAsync();

                ViewBag.Perfis = new SelectList(result.Select(p => p.Nome));

                if (id == null)
                {
                    ViewData["ErrorMessage"] = "Não foi possível concluir o processo de edição do usuário.";
                    return RedirectToAction("EditarAsync");
                }

                var usuarioFiltrado = await BuscarUsuarioAsync(Convert.ToInt32(id));

                return View("EditarAsync", usuarioFiltrado);
            }
            catch (MidiaException e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return RedirectToAction("EditarAsync");
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = "Não foi possível concluir o processo de edição do usuário.";
                return RedirectToAction("EditarAsync");
            }
        }

        public async Task<IEnumerable<PerfilViewModel>> BuscarPerfisAsync()
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<PerfilViewModel>>(keyUrl, $"/Perfil/GetAllPerfil/{ Usuario.idCliente }");

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os perfis.", e);
            }
        }

        public async Task<UsuarioViewModel> BuscarUsuarioAsync(int id)
        {
            try
            {
                var result = await GetUsuarioAsync(id);

                result.UsuarioXPerfil = await BuscarPerfilUsuarioAsync(result.ID);

                result.Perfil = (await BuscarPerfilAsync(result.UsuarioXPerfil.IdPerfil)).Nome;

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os usuários.", e);
            }
        }

        public async Task<UsuarioXPerfilViewModel> BuscarPerfilUsuarioAsync(int usuarioId)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var usuarioXPerfil = await helper.GetAsync<UsuarioXPerfilViewModel>(keyUrl, $"/Perfil/GetPerfilByUsuario/{ usuarioId }");

                return usuarioXPerfil;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o perfil do usuário.", e);
            }
        }

        public async Task<UsuarioViewModel> GetUsuarioAsync(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

            var envio = new
            {
                Usuario.idCliente,
                idUsuario = id,
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<UsuarioViewModel>(keyUrl, $"/Seguranca/Principal/BuscarUsuarioPorId/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            return result;
        }

        public async Task<UsuarioViewModel> GetUsuarioCadAsync(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

            var envio = new
            {
                idCliente = 12,
                idUsuario = id,
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<UsuarioViewModel>(keyUrl, $"/Seguranca/Principal/BuscarUsuarioPorId/{ 12 }/{ 999 }", envio);

            return result;
        }

        private async Task<PerfilViewModel> BuscarPerfilAsync(int idPerfil)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<PerfilViewModel>(keyUrl, $"/Perfil/GetPerfilByID/{ idPerfil }");

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o perfil do usuário.", e);
            }
        }

        #region home
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
                    paciente.Sexo = model.Sexo;
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

                return "Usuario editado com sucesso.";
            }
            catch (Exception e)
            {
                return "Não foi possível alterar o usuário";
            }
        }

        public async Task<string> AlterarUsuarioMedico(UsuarioViewModel model)
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

                return "Usuario editado com sucesso.";
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
        #endregion
    }
}