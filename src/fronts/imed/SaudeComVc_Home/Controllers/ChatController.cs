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
    public class ChatController : Controller
    {
        // GET: Chat
        public async Task<ActionResult> Index()
        {
            if (PixCoreValues.UsuarioLogado == null || PixCoreValues.UsuarioLogado.IdUsuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var perfil = await PixCoreValues.GetPerfilUsuarioAsync(PixCoreValues.UsuarioLogado.IdUsuario);

            if (perfil.IdPerfil == 12) //Medico
            {
                var mc = new MedicoController();
                //var medico = (await mc.GetMedicosByIdsExternosAsync(new List<int>() { PixCoreValues.UsuarioLogado.IdUsuario })).FirstOrDefault();
                var medico = mc.BuscarMedicoByUsuarioSA(PixCoreValues.UsuarioLogado.IdUsuario);
                var mxp = mc.BuscarPcientesDoMedico(medico.ID);

                var pc = new PacienteController();
                var pacientes = pc.BuscarPacientesPorIds(mxp.Select(x => x.IdPaciente));

                var uc = new UsuariosController();
                var usuarios = await uc.BuscarUsuariosPorIdsAsync(pacientes.Select(x => x.CodigoExterno));

                return View(new ChatViewModel(usuarios));

            }
            else if (perfil.IdPerfil == 14) //Paciente
            {
                var pc = new PacienteController();
                var paciente = pc.BuscarPaciente(PixCoreValues.UsuarioLogado.IdUsuario);

                var mc = new MedicoController();
                var mxp = mc.BuscarMedicoXPacientes(paciente.ID);
                var medicos = mc.GetMedicosID(mxp.Select(x => x.MedicoId));

                var uc = new UsuariosController();
                var usuarios = await uc.BuscarUsuariosPorIdsAsync(medicos.Select(x => x.IdUsuario));

                return View(new ChatViewModel(usuarios));
            }

            return View(new ChatViewModel());
        }

        [HttpGet]
        public async Task<ActionResult> BuscarMensagensAsync(MensagemViewModel viewModel)
        {
            var helper = new ServiceHelper();

            var mensagensDestinatario = await helper.GetAsync<IEnumerable<MensagemViewModel>>("http://201.73.1.17:84/",
                    $"api/Mensagens/GetByEntidade/{ 12 }/{viewModel.DestinatarioId}/{ viewModel.RemetenteId }");

            var mensagensRemetente = await helper.GetAsync<IEnumerable<MensagemViewModel>>("http://201.73.1.17:84/",
                    $"api/Mensagens/GetByEntidade/{ 12 }/{viewModel.RemetenteId}/{ viewModel.DestinatarioId }");

            var mensagens = mensagensDestinatario.Concat(mensagensRemetente);

            var uc = new UsuariosController();
            var usuario = await uc.BuscarUsuarioAsync(viewModel.DestinatarioId);
            TempData["NomeDestinatario"] = usuario.Nome;
            if (usuario.ProfileAvatar != null)
            {
                TempData["DestinatarioFoto"] = usuario.ProfileAvatar;
                TempData["ExtensaoDestinatario"] = usuario.AvatarExtension.Replace(".", "");
            }


            return Json(mensagens.OrderBy(x => x.DataCriacao), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> BuscarDados(int id)
        {
            var uc = new UsuariosController();
            var usuario = await uc.BuscarUsuarioAsync(id);

            return Json(usuario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EnviarMensagemAsync(MensagemViewModel viewModel)
        {
            try
            {
                viewModel.IdCliente = 12;
                viewModel.UsuarioCriacao = PixCoreValues.UsuarioLogado.IdUsuario;
                viewModel.UsuarioEdicao = PixCoreValues.UsuarioLogado.IdUsuario;
                viewModel.Ativo = true;
                viewModel.Status = 1;
                viewModel.GerarNotificacao = true;
                viewModel.LinkNotificacao = "/Chat/Index";

                var helper = new ServiceHelper();
                var mensagemEnviada = await helper.PostAsync<MensagemViewModel>("http://201.73.1.17:84/", "api/Mensagens", viewModel);

                return Json(mensagemEnviada, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<ActionResult> BuscarRemetente()
        {
            var perfil = await PixCoreValues.GetPerfilUsuarioAsync(PixCoreValues.UsuarioLogado.IdUsuario);

            if (perfil.IdPerfil == 12) //Medico
            {
                var mc = new MedicoController();
                var medico = (await mc.GetMedicosByIdsExternosAsync(new List<int>() { PixCoreValues.UsuarioLogado.IdUsuario })).FirstOrDefault();
                var mxp = mc.BuscarPcientesDoMedico(medico.ID);

                var pc = new PacienteController();
                var pacientes = pc.BuscarPacientesPorIds(mxp.Select(x => x.IdPaciente));

                var uc = new UsuariosController();
                var usuarios = await uc.BuscarUsuariosPorIdsAsync(pacientes.Select(x => x.CodigoExterno));

                return Json(usuarios, JsonRequestBehavior.AllowGet);

            }
            else if (perfil.IdPerfil == 14) //Paciente
            {
                var pc = new PacienteController();
                var paciente = pc.BuscarPaciente(PixCoreValues.UsuarioLogado.IdUsuario);

                var mc = new MedicoController();
                var mxp = mc.BuscarMedicoXPacientes(paciente.ID);
                var medicos = mc.GetMedicos(mxp.Select(x => x.MedicoId));

                var uc = new UsuariosController();
                var usuarios = await uc.BuscarUsuariosPorIdsAsync(medicos.Select(x => x.IdUsuario));

                //return View(new ChatViewModel(usuarios));
                return Json(usuarios, JsonRequestBehavior.AllowGet);

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}