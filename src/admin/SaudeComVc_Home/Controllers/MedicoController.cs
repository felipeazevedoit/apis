using SaudeComVc_Home.Helpers;
using SaudeComVc_Home.Models;
using SaudeComVoce.Helpers;
using SaudeComVoce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SaudeComVc_Home.Controllers
{
    public class MedicoController : Controller
    {
        private static EspecialidadeViewModel _especialidade;

        // GET: Medico
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Medico", "Index", url.AbsoluteUri);

            return View();
        }

        public async Task<ActionResult> _Medico()
        {

            var espc = await BuscarEspecialidadesAsync();
            
            var clinica = await BuscarClincasAsync();

            TempData["Clinicas"] = clinica;

            return PartialView(espc);
        }

        public async Task<ActionResult> ListagemMedico(int? id)
        {
            var codigos = await GetCodigosAsync();
            var medicos = await GetMedicosAsync(codigos);
            var especialidades = await BuscarEspecialidadesAsync();
            TempData["Especialidades"] = especialidades;

            _especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(id));

            if (id == null)
            {
                TempData["Medicos"] = medicos;

                return View(medicos);
            }

            return View(await BuscarMedicoEspecialidades((int)id));
        }

        [HttpGet]
        public async Task<IEnumerable<MedicoViewModel>> BuscarMedicoEspecialidades(int id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    especialidadeId = id,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<MedicoXEspecialidadeViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarMedicosEspecialidades/12/999", envio);

                var codigos = await GetCodigosAsync();
                var medicos = await GetMedicosAsync(codigos);

                var response = medicos.Where(m => m.Especialidade.Equals(_especialidade.Nome)).ToList();

                return response;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

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

        public async Task<ActionResult> WhiteLabel(string nome, int? id)
        {
            var home = new HomeController();
            var codigos = await home.GetCodigosAsync();
            var medicos = await home.GetMedicosAsync(codigos);
            TempData["Medicos"] = medicos;

            if (id != null)
            {
                var nc = new NoticiasController();

                var pagina = await BuscarPaginaAsync((int)id);
                var galeria = await nc.BuscarMidiasAsync((int)id);
                var noticias = await nc.BuscarNoticiasAsync((int)id);
                var historicos = await BuscarHistoricosAsync((int)id);
                var logo = await BuscarMidiaLogoAsync(id);
                return View(new WhiteLabelViewModel((int)id, pagina, galeria, noticias, historicos));
            }

            return View(new WhiteLabelViewModel());
        }

        private async Task<PaginaViewModel> BuscarPaginaAsync(int medicoId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
                codigoExterno = medicoId,
            };

            var helper = new ServiceHelper();
            var result = await helper.PostAsync<PaginaViewModel>(keyUrl, $"/Seguranca/Paginas/BuscarPorCodigoExterno/12/999", envio);

            return result;
        }

        public async Task<IEnumerable<EspecialidadeViewModel>> BuscarEspecialidadesAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<EspecialidadeViewModel>>(url,
                    $"/Seguranca/WpMedicos/BuscarEspecialidades/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }

        public async Task<IEnumerable<EmpresasViewModel>> BuscarClincasAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<EmpresasViewModel>>(url,
                    $"/Seguranca/wpEmpresas/BuscarEmpresas/12/999", envio);

                

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }

        public async Task<ActionResult> _Historico(int medicoId, string medicoNome)
        {
            var historicos = await BuscarHistoricosAsync(medicoId);

            foreach (var item in historicos)
            {
                item.MedicoNome = medicoNome;
            }

            return PartialView(historicos);
        }

        private async Task<List<HistoricoViewModel>> BuscarHistoricosAsync(int codigoExterno)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];
                var envio = new
                {
                    idCliente = 12,
                    codigoExterno,
                };

                var helper = new ServiceHelper();
                var ret = await helper.PostAsync<List<HistoricoViewModel>>(url,
                    $"/Seguranca/wpProfissionais/BuscarPorCodigoExterno/{ 12 }/{ 999 }", envio);

                return ret;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar os históricos disponíveis.");
            }
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

        public async Task<MidiaViewModel> BuscarMidiaLogoAsync(int? id)
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

                var logo = result.FirstOrDefault(p => p.CategoriaId == 3);

                if (logo != null)
                {
                    TempData["Logo"] = Convert.ToBase64String(logo.Arquivo);
                    TempData["ExtensaoLogo"] = logo.Extensao.Replace(".", "");
                }

                return logo;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a midia solicitada.", e);
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

                var user = await BuscarUsuariosAsync(result.Select(u => u.IdUsuario));

                var espc = await BuscarEspcsAsync(result.Select(u => u.ID));

                var nespc = await BuscarEspecialidadesPorIdsAsync(espc.Select(e => e.EspecialidadeId));

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Foto = user.ElementAtOrDefault(i).ProfileAvatar;
                    result.ElementAtOrDefault(i).Extensao = user.ElementAtOrDefault(i).AvatarExtension.Replace(".", "");
                    result.ElementAtOrDefault(i).Especialidade = nespc.ElementAtOrDefault(i).Nome;
                    var pagina = await BuscarPaginaAsync(result.ElementAtOrDefault(i).IdUsuario);
                    result.ElementAtOrDefault(i).DescricaoWhiteLabel = pagina.Apresentacao;


                }

                return result.Where(r => r.Ativo);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<UsuarioViewModel>> BuscarUsuariosAsync(IEnumerable<int> idsUsuarios)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    idCliente = 12,
                    ids = idsUsuarios
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<UsuarioViewModel>>(keyUrl, $"/Seguranca/Principal/BuscarUsuarios/12/999", envio);


                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o usuario.", e);
            }
        }

        public async Task<IEnumerable<EspecialidadeViewModel>> BuscarEspcsAsync(IEnumerable<int> id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                var envio = new
                {
                    idCliente = 12,
                    ids = id
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<EspecialidadeViewModel>>(keyUrl, $"/Seguranca/WpMedicos/BuscarEspcPorIdsAsync/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a espc.", e);
            }
        }

        public async Task<IEnumerable<EspecialidadeViewModel>> BuscarEspecialidadesPorIdsAsync(IEnumerable<int> idsEspc)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    ids = idsEspc
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<IEnumerable<EspecialidadeViewModel>>(url,
                    $"/Seguranca/WpMedicos/BuscarEspcPorIds/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }
    }
}