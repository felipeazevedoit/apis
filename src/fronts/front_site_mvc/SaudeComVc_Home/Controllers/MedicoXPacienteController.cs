using SaudeComVc_Home.Models;
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
    public class MedicoXPacienteController : Controller
    {
        private readonly UsuariosController _usuarioController;
        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;

        public MedicoXPacienteController()
        {
            _usuarioController = new UsuariosController();
        }

        // GET: MedicoXPaciente
        public async Task<ActionResult> Index()
        {
            return View(await BuscarPacientesMedicoAsync());
        }

        public async Task<ActionResult> Visualizar(int? id)
        {
            if (id != null && id > 0)
            {
                var paciente = await BuscarPacienteAsync((int)id);
                var nXp = await BuscarNoticiasVisualizadas(paciente.ID);
                TempData["nVisu"] = nXp.Count();

                //IList<NoticiaViewModel> not = new List<NoticiaViewModel>();

                //for (int i = 0; i < nXp.Count(); i++)
                //{
                //    var noticia = await BuscarNoticiasAsync(nXp.ElementAtOrDefault(i).NoticiaId);
                //    not.Add(noticia);
                //}

                //TempData["NoticiasPaciente"] = not.Take(3).OrderByDescending(p => p.DataCriacao);

                var respostas = await BuscarRespostasAsync(paciente.CodigoExterno);

                if (respostas != null)
                {
                    int num = respostas.Count();

                    TempData["Respostas"] = respostas.Count();
                    TempData["RespostasPorcentagem"] = num * 0.62;
                    TempData["RespostasS"] = respostas.Where(s => s.Descricao.Equals("Sim")).Count();
                    TempData["RespostasN"] = respostas.Where(s => s.Descricao.Equals("Não")).Count();
                }
                else
                {

                    TempData["Respostas"] = 0;
                    TempData["RespostasPorcentagem"] = 0;
                    TempData["RespostasS"] = 0;
                    TempData["RespostasN"] = 0;
                }

                

                //TempData["Respostas"] = 52;
                //TempData["RespostasPorcentagem"] = 52 * 0.62;
                //TempData["RespostasS"] = 20;
                //TempData["RespostasN"] = 42;

                var mxp = await BuscarPacientesMedicoAsync();

                TempData["MedicoXPaciente"] = mxp;

                return View(paciente);
            }

            return View(new PacienteViewModel());

        }

        public async Task<ActionResult> Redirecionar(int? id)
        {
            if (id != null && id > 0)
            {
                var paciente = await BuscarPacienteAsync((int)id);
                var nXp = await BuscarNoticiasVisualizadas(paciente.ID);
                TempData["nVisu"] = nXp.Count();

                //IList<NoticiaViewModel> not = new List<NoticiaViewModel>();

                //for (int i = 0; i < nXp.Count(); i++)
                //{
                //    var noticia = await BuscarNoticiasAsync(nXp.ElementAtOrDefault(i).NoticiaId);
                //    not.Add(noticia);
                //}

                //TempData["NoticiasPaciente"] = not.Take(3).OrderByDescending(p => p.DataCriacao);

                var respostas = await BuscarRespostasAsync(paciente.CodigoExterno);

                if (respostas != null)
                {
                    int num = respostas.Count();

                    TempData["Respostas"] = respostas.Count();
                    TempData["RespostasPorcentagem"] = num * 0.62;
                    TempData["RespostasS"] = respostas.Where(s => s.Descricao.Equals("Sim")).Count();
                    TempData["RespostasN"] = respostas.Where(s => s.Descricao.Equals("Não")).Count();
                }
                else
                {

                    TempData["Respostas"] = 0;
                    TempData["RespostasPorcentagem"] = 0;
                    TempData["RespostasS"] = 0;
                    TempData["RespostasN"] = 0;
                }

                //TempData["Respostas"] = 52;
                //TempData["RespostasPorcentagem"] = 52 * 0.62;
                //TempData["RespostasS"] = 20;
                //TempData["RespostasN"] = 42;

                var mxp = await BuscarPacientesMedicoAsync();

                TempData["MedicoXPaciente"] = mxp;

                return View("Visualizar", paciente);
            }

            return View(new PacienteViewModel());

        }



        public async Task<MedicoViewModel> BuscarMedicoCodigoExternoAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idExterno = Usuario.IdUsuario
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MedicoViewModel>(url,
                    $"/Seguranca/WpMedicos/BuscarPorIdExterno/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
            }
        }

        public async Task<IEnumerable<RespostaViewModel>> BuscarRespostasAsync(int codExt)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<RespostaViewModel>>("http://179.188.38.126:82/api/", $"Respostas/GetByIdExterno12/" + codExt);

                return result;

            }
            catch (Exception e)
            {
                if (e.Message == "O servidor remoto retornou um erro: (406) Não Aceitável.")
                {
                    return null;
                }
                else
                {
                    throw new Exception("Não foi possível listar os pacientes.", e);
                }
            }
        }

        private async Task<NoticiaViewModel> BuscarNoticiasAsync(int id)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    noticiaId = id
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<NoticiaViewModel>(url,
                    $"/Seguranca/WpNoticias/BuscarNoticia/12/'" + Usuario.IdUsuario + "'", envio);

                var midia = await BuscarMidiaAsync(result.ID);
                if (midia != null && midia.ID > 0)
                {
                    midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                    midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                    result.Midia = midia;
                }

                //foreach (var n in result)
                //{
                //    var midia = await BuscarMidiaAsync(n.ID);
                //    if (midia != null && midia.ID > 0)
                //    {
                //        midia.Extensao = midia.Extensao.Replace(".", string.Empty);
                //        midia.ArquivoB64 = Convert.ToBase64String(midia.Arquivo);
                //        n.Midia = midia;
                //    }
                //}

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as notícias disponíveis.");
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
                throw new Exception("Não foi possível buscar a midia solicitada.", e);
            }
        }

        public async Task<IEnumerable<NoticiaXPacienteViewModel>> BuscarNoticiasVisualizadas(int id)
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
                throw new Exception("Não foi possível buscar as notícias disponíveis.");
            }
        }

        public async Task<IEnumerable<PacienteViewModel>> BuscarPacientesMedicoAsync()
        {
            try
            {

                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var medico = await BuscarMedicoCodigoExternoAsync();

                var envio = new
                {
                    idMedico = medico.ID
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MedicoXPacienteViewModel>>(url,
                    $"/Seguranca/WpMedicos/BuscarPacientePorMedico/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);


                //foreach(var p in result)
                //{
                //    var paciente = await BuscarPacienteAsync(p.IdPaciente);
                //    p.Termo = paciente.Termo;
                //}

                var pacientes = await BuscarPacientesAsync(result.Select(r => r.IdPaciente));
                var usuarios = await BuscarUsuariosAsync(pacientes.Select(p => p.CodigoExterno));

                foreach (var item in pacientes)
                {
                    var user = usuarios.FirstOrDefault(u => u.ID.Equals(item.CodigoExterno));
                    item.Termo = user?.Termo;
                    item.Login = user?.Login;
                    TempData[$"{ user.ID }_Nome"] = item.Nome;
                    TempData[$"{ user.ID }_Peso"] = item.Peso;
                    TempData[$"{ user.ID }_Niver"] = (DateTime.UtcNow - item.DataNascimento).Days / 365;
                }

                return pacientes;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> BuscarUsuariosAsync(IEnumerable<int> enumerable)
        {

            //BuscarUsuarios

            //envio ids e idcliente

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                Usuario.idCliente,
                ids = enumerable
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(keyUrl, $"/Seguranca/Principal/BuscarUsuarios/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            return result;
        }

        private async Task<IEnumerable<PacienteViewModel>> BuscarPacientesAsync(IEnumerable<int> ids)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                Usuario.idCliente,
                ids
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<IEnumerable<PacienteViewModel>>(keyUrl, $"/Seguranca/WpPacientes/BuscarPacientesPorIds/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            return result;
        }

        public async Task<PacienteViewModel> BuscarPacienteAsync(int id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    Usuario.idCliente,
                    pacienteId = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<PacienteViewModel>(keyUrl,
                    $"/Seguranca/WpPacientes/BuscarPaciente/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

                var usuario = await BuscarUsuarioAsync(result.CodigoExterno);

                result.Senha = usuario.Senha;
                result.Login = usuario.Login;
                result.ProfileAvatar = usuario.ProfileAvatar;
                result.Extensao = usuario.AvatarExtension;
                result.Termo = usuario.Termo;

                TempData["Sexo"] = new SelectList(BuscarSexos());
                TempData["Termo"] = usuario.Termo;
                TempData[$"{ usuario.ID }_Nome"] = result.Nome;
                TempData[$"{ usuario.ID }_Peso"] = result.Peso;
                TempData["Peso"] = result.Peso;
                TempData["Altura"] = result.Altura;
                TempData[$"{ usuario.ID }_Niver"] = (DateTime.UtcNow - result.DataNascimento).Days / 365;
                TempData["Idade"] = (DateTime.UtcNow - result.DataNascimento).Days / 365;

                TempData["IMC"] = result.Peso / (result.Altura * result.Altura);

                int imc = Convert.ToInt32(result.Peso) / Convert.ToInt32((result.Altura * result.Altura));

                if (imc < 16)
                {
                    TempData["IMCt"] = "Magreza grave";
                }
                else if (imc >= 16 && imc <= 17)
                {
                    TempData["IMCt"] = "Magreza moderada";
                }
                else if (imc > 17 && imc <= 18)
                {
                    TempData["IMCt"] = "Magreza leve";
                }
                else if (imc > 18 && imc <= 25)
                {
                    TempData["IMCt"] = "Saudável";
                }
                else if (imc > 25 && imc <= 30)
                {
                    TempData["IMCt"] = "Sobrepeso";
                }
                else if (imc > 30 && imc <= 35)
                {
                    TempData["IMCt"] = "Obesidade de grau I";
                }
                else if (imc > 35 && imc <= 40)
                {
                    TempData["IMCt"] = "Obesidade de grau II(severa)";
                }
                else if (imc > 40)
                {
                    TempData["IMCt"] = "Obesidade de grau III(mórbida)";
                }



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os pacientes.", e);
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

        private async Task<UsuarioXPerfilViewModel> BuscarPerfilUsuarioAsync(int usuarioId)
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

        public IList<string> BuscarSexos()
        {
            return new List<string>() { "Masculino", "Feminino" };
        }
    }
}