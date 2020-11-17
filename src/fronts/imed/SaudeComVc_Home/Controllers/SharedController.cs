using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    [AllowAnonymous]
    public class SharedController : Controller
    {
        public class Teste
        {
            public string suggestion { get; set; }
            public string url { get; set; }
        }

        [ChildActionOnly]
        [ActionName("GetHeader")]
        public ActionResult GetHeaderAsync()
        {
            return PartialView("_Header");
        }

        public ActionResult _ColapseMenu()
        {
            return PartialView();
        }

        public ActionResult _Navbar()
        {
            return PartialView();
        }

        public ActionResult _Layout()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Notificacoes()
        {
            try
            {
                var helper = new ServiceHelper();

                var notificacoes = helper.Get<IEnumerable<NotificacaoViewModel>>("http://servicepix.com.br:82/",
                    $"api/Notificacoes/GetByIdExterno/{ 12 }/{ PixCoreValues.UsuarioLogado.IdUsuario }");

                //var nc = new NoticiasController();
                //var noticiasPrivadas = await nc.BuscarPrivadasAsync(PixCoreValues.UsuarioLogado.IdUsuario);

                var mc = new MedicoController();
                var pc = new PacienteController();
                //var medicos = await mc.GetMedicosByIdsExternosAsync(noticiasPrivadas.Select(n => n.CodigoExterno));

                //foreach (var item in noticiasPrivadas)
                //{
                //    item.Medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(item.CodigoExterno));
                //}

                if (PixCoreValues.UsuarioLogado.idPerfil == 14)
                {
                    var paciente = pc.BuscarPaciente(PixCoreValues.UsuarioLogado.IdUsuario);
                    var mxp = mc.BuscarMedicoXPacientes(paciente.ID);

                    if (mxp != null)
                    {
                        var pacienteMedicos = mc.GetMedicosID(mxp.Select(x => x.MedicoId));
                        return PartialView("_Notificacoes", new FeedViewModel(notificacoes.OrderByDescending(n => n.ID), null) { Medicos = pacienteMedicos });
                    }
                }
                else if (PixCoreValues.UsuarioLogado.idPerfil == 0)
                {
                    var paciente = pc.BuscarPaciente(PixCoreValues.UsuarioLogado.IdUsuario);
                    var mxp = mc.BuscarMedicoXPacientes(paciente.ID);

                    if (mxp != null)
                    {
                        var pacienteMedicos = mc.GetMedicos(mxp.Select(x => x.MedicoId));
                        return PartialView("_Notificacoes", new FeedViewModel(notificacoes.OrderByDescending(n => n.ID), null) { Medicos = pacienteMedicos });
                    }
                }
                else
                {
                    var medico = mc.BuscarMedicoByUsuarioSA(PixCoreValues.UsuarioLogado.IdUsuario);
                    var mxp = mc.BuscarPcientesDoMedico(medico.ID);


                    var pacientes = pc.BuscarPacientesPorIds(mxp.Select(x => x.IdPaciente));

                    return PartialView("_Notificacoes", new FeedViewModel(notificacoes.OrderByDescending(n => n.ID), null) { Pacientes = pacientes });
                }

                return PartialView("_Notificacoes", new FeedViewModel(notificacoes.OrderByDescending(n => n.ID), null));


                //return PartialView("_Notificacoes");
            }
            catch (Exception e)
            {
                return PartialView("_Notificacoes");
            }
        }
    }
}