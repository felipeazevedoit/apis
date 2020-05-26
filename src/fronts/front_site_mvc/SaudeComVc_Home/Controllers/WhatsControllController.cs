using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
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
    public class WhatsControllController : Controller
    {
        // GET: WhatsControll
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> WhatsControll(int idMedico)
        {
            if (PixCoreValues.UsuarioLogado == null || PixCoreValues.UsuarioLogado.IdUsuario == 0)
            {
                return RedirectToAction("Index", "Login");
            }

            var pC = new PacienteController();

            var paciente = await pC.BuscarPacienteAsync(PixCoreValues.UsuarioLogado.IdUsuario);

            var convenios = await pC.BuscarConveniosAsync();

            paciente.Convenio = convenios.FirstOrDefault(e => e.ID.Equals(paciente.ConvenioId)).Nome;

            var mC = new MedicoController();

            var medico = await mC.BuscarMedicoByUsuario(idMedico);

            paciente.NomeMedico = medico.Nome;

            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                nome = paciente.Nome,
                telefone = paciente.Telefone.Numero,
                email = PixCoreValues.UsuarioLogado.Login,
                obs = "Peso: " + paciente.Peso + ", Altura: " + paciente.Altura + ", CPF: " + paciente.CPF + ", Plano de Saúde: " + paciente.Convenio,
                relacionamento = medico.Especialidade.ToUpper(),
                status = "A FALAR",
                produto = medico.Nome.ToUpper().TrimEnd(),
                sub_produto = "",
                valor = "",
                data_nascimento = paciente.DataNascimento,
                token = "ae7046a7-3817-4b91-bfe0-0d02f6b09109"
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<WhatsControlViewModel>("",$"http://www.whatscontrol.com/clientes/criar.json", envio);

            if (result.mensagem == "Criado com sucesso")
            {
                return Json(paciente, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
    }
}