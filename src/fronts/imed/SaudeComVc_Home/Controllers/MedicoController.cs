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
    [AllowAnonymous]
    public class MedicoController : Controller
    {
        private static EspecialidadeViewModel _especialidade;
        private readonly UsuariosController _usuarioController;

        private readonly LoginViewModel Usuario = PixCoreValues.UsuarioLogado;
        // GET: Medico
        public async Task<ActionResult> Index()
        {
            var url = System.Web.HttpContext.Current.Request.Url;

            var log = await Log.GerarLogAsync("Medico", "Index", url.AbsoluteUri);

            return View();
        }

        public ActionResult ResultadosMedicos()
        {
            return View();
        }

        public async Task<ActionResult> _listarMedicos()
        {
            return PartialView("_listarMedicos", await BuscarMedicosAsync());
        }

        public async Task<ActionResult> _Medico()
        {

            var espc = await BuscarEspecialidadesAsync();

            var clinica = await BuscarClincasAsync();

            TempData["Clinicas"] = clinica;

            return PartialView(espc);
        }

        public async Task<ActionResult> ListagemMedico(int? id, string cidade, string espc)
        {
            var especialidades = await BuscarEspecialidadesAsync();
            TempData["Especialidades"] = especialidades;

            if (cidade != null && espc != null && cidade != "" && espc != "")
            {
                var codigos = await GetCodigosAsync();
                var medicos = await GetMedicosListagem(codigos);    

                List<MedicoViewModel> mc = new List<MedicoViewModel>();

                for (int i = 0; i < medicos.Count(); i++)
                {
                    if (medicos.ElementAtOrDefault(i).Endereco.Cidade == cidade && medicos.ElementAtOrDefault(i).Especialidade == espc)
                    {
                        mc.Add(medicos.ElementAtOrDefault(i));
                    }
                }


                var rnd = new Random();

                var query =
                    from i in mc
                    let r = rnd.Next()
                    orderby r
                    select i;

                _especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(id));

                if (id == null)
                {
                    TempData["Medicos"] = medicos;

                    return View(query);
                }
            }
            else if (espc != null && espc != "")
            {
                var codigos = await GetCodigosAsync();
                var medicos = await GetMedicosListagem(codigos);

                List<MedicoViewModel> mc = new List<MedicoViewModel>();

                for (int i = 0; i < medicos.Count(); i++)
                {
                    if (medicos.ElementAtOrDefault(i).Especialidade == espc)
                    {
                        mc.Add(medicos.ElementAtOrDefault(i));
                    }
                }


                var rnd = new Random();

                var query =
                    from i in mc
                    let r = rnd.Next()
                    orderby r
                    select i;

                _especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(id));

                if (id == null)
                {
                    TempData["Medicos"] = medicos;

                    return View(query);
                }
            }
            else if (cidade != null && cidade != "")
            {
                var codigos = await GetCodigosAsync();
                var medicos = await GetMedicosListagem(codigos);

                List<MedicoViewModel> mc = new List<MedicoViewModel>();

                for (int i = 0; i < medicos.Count(); i++)
                {
                    if (medicos.ElementAtOrDefault(i).Endereco.Cidade == cidade)
                    {
                        mc.Add(medicos.ElementAtOrDefault(i));
                    }
                }

                var rnd = new Random();

                var query =
                    from i in mc
                    let r = rnd.Next()
                    orderby r
                    select i;

                _especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(id));

                if (id == null)
                {
                    TempData["Medicos"] = medicos;

                    return View(query);
                }
            }
            else
            {
                var codigos = await GetCodigosAsync();
                var medicos = await GetMedicosListagem(codigos);

                var rnd = new Random();

                var query =
                    from i in medicos
                    let r = rnd.Next()
                    orderby r
                    select i;

                _especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(id));

                if (id == null)
                {
                    TempData["Medicos"] = medicos;

                    return View(query);
                }
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
                var medicos = GetMedicos(codigos);

                var response = medicos.Where(m => m.Especialidade.Equals(_especialidade.Nome)).ToList();

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<MedicoViewModel>> BuscarMedicosAsync()
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var helper = new ServiceHelper();
                var result = await helper.GetAsync<IEnumerable<MedicoXEspecialidadeViewModel>>(url,
                    $"/Seguranca/WpMedicos/BuscarMedicos/{ Usuario.idCliente }/{ Usuario.IdUsuario }");

                var usuarios = await GetUsuariosAsync(result.Select(p => p.Medico.IdUsuario));

                foreach (var item in result)
                {
                    item.Medico.Login = usuarios.SingleOrDefault(u => u.ID.Equals(item.Medico.IdUsuario))?.Login;
                }

                var perfis = await GetPerfisUsuariosAsync(result.Select(m => m.Medico.IdUsuario));

                foreach (var item in result)
                {
                    item.Medico.IdPerfil = perfis.SingleOrDefault(p => p.IdUsuario.Equals(item.Medico.IdUsuario))?.IdPerfil;
                }

                return result.Select(x => x.Medico);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível listar os medicos.", e);
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

        public async Task<MedicoViewModel> BuscarMedicoByUsuario(int idUsuario)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    idExterno = idUsuario,
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MedicoViewModel>(keyUrl, "/Seguranca/WpMedicos/BuscarPorIdExterno/12/999", envio);

                var mXe = BuscarEspcsSimples(result.ID);

                var especialidades = await BuscarEspecialidadesAsync();

                result.Especialidade = especialidades.FirstOrDefault(e => e.ID.Equals(mXe.FirstOrDefault(mxe => mxe.MedicoId.Equals(result.ID)).EspecialidadeId)).Nome;

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MedicoViewModel BuscarMedicoByUsuarioSA(int idUsuario)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    idExterno = idUsuario,
                };

                var helper = new ServiceHelper();
                var result = helper.Post<MedicoViewModel>(keyUrl, "/Seguranca/WpMedicos/BuscarPorIdExterno/12/999", envio);

         
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<MedicoXPaciente> BuscarMedicoXPacientes(int idPaciente)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

                var envio = new
                {
                    idCliente = 12,
                    idPaciente,
                };

                var helper = new ServiceHelper();
                var result = helper.Post<IEnumerable<MedicoXPaciente>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorPaciente/12/999", envio);

                return result;
            }
            catch (Exception e)
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

            var medico = medicos.FirstOrDefault(m => m.CodigoExterno.Equals(id) || m.Nome.Equals(nome));

            var vinculo = default(MedicoXPaciente);

            if (medico != null)
            {
                var curc = new CurriculoController();

                var result = await curc.BuscarCurriculoAsync(Convert.ToInt32(medico.CodigoExterno));

                if (result == null || result.Count() == 0)
                {
                    TempData["curriculo"] = null;
                }
                else
                {
                    TempData["curriculo"] = "a";
                }

                if (PixCoreValues.UsuarioLogado.IdUsuario > 0 && PixCoreValues.UsuarioLogado.idPerfil == 14)
                {
                    var pc = new PacienteController();
                    var paciente = pc.BuscarPaciente(PixCoreValues.UsuarioLogado.IdUsuario);

                    if (paciente != null)
                    {
                        var mxp = BuscarPcientesDoMedico(medico.ID);
                        vinculo = mxp.FirstOrDefault(x => x.IdPaciente.Equals(paciente.ID));
                    }
                }

                //}

                //if (id != null)
                //{
                var nc = new NoticiasController();

                int codE = Convert.ToInt32(medico.CodigoExterno);

                var pagina = BuscarPagina(codE);
                var galeria = await nc.BuscarMidiasAsync(codE);
                var noticias = await nc.BuscarNoticiasAsync(codE);

                if (Usuario.IdUsuario > 0 && Usuario.idPerfil == 14)
                {
                    var pac = await BuscarPacienteAsync(Usuario.IdUsuario);
                    var pXp = await BuscarPaginaXPacienteAsync(pac.ID);

                    var pxpM = new PaginaXPacienteViewModel();

                    if (pXp != null)
                    {
                        for (int i = 0; i < pXp.Count(); i++)
                        {
                            pxpM.ID = pXp.ElementAt(i).ID;
                        }
                        pxpM.PacienteId = pac.ID;
                        pxpM.PaginaId = pagina.ID;
                        pxpM.DataVisualizacao = DateTime.UtcNow;
                    }
                    else
                    {
                        pxpM.ID = 0;
                        pxpM.PacienteId = pac.ID;
                        pxpM.PaginaId = pagina.ID;
                        pxpM.DataVisualizacao = DateTime.UtcNow;
                    }

                    var vinculoPxP = await SalvarPaginaXPacienteAsync(pxpM);

                }


                return View(new WhiteLabelViewModel((int)id, pagina, galeria, noticias, medico) { Vinculo = vinculo });
            }

            return View(new WhiteLabelViewModel() { Vinculo = vinculo });
        }

        public async Task<PaginaXPacienteViewModel> SalvarPaginaXPacienteAsync(PaginaXPacienteViewModel model)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new { pagina = model };

                var helper = new ServiceHelper();
                return await helper.PostAsync<PaginaXPacienteViewModel>(url,
                    $"/Seguranca/Paginas/SalvarPaginaXPaciente/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);

            }
            catch (Exception)
            {

                throw;
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

        public async Task<IEnumerable<PaginaXPacienteViewModel>> BuscarPaginaXPacienteAsync(int idPac)
        {
            try
            {
                var envio = new
                {
                    idPaciente = idPac
                };

                var url = ConfigurationManager.AppSettings["UrlAPI"];



                var helper = new ServiceHelper();
                var ret = await helper.PostAsync<IEnumerable<PaginaXPacienteViewModel>>(url,
                             $"/Seguranca/Paginas/BuscarPaginaXPacienteIdPac/{ Usuario.idCliente }/{ Usuario.IdUsuario }", envio);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MedicoXPaciente> BuscarPcientesDoMedico(int idMedico)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
                idMedico,
            };

            var helper = new ServiceHelper();
            var result = helper.Post<IEnumerable<MedicoXPaciente>>(keyUrl, $"/Seguranca/WpMedicos/BuscarPacientePorMedico/12/999", envio);

            return result;
        }

        public PaginaViewModel BuscarPagina(int medicoId)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var envio = new
            {
                idCliente = 12,
                codigoExterno = medicoId,
            };

            var helper = new ServiceHelper();
            var result = helper.Post<PaginaViewModel>(keyUrl, $"/Seguranca/Paginas/BuscarPorCodigoExterno/12/999", envio);

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

        public IEnumerable<MedicoViewModel> GetMedicos(IEnumerable<int> ids)
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
                var result = helper.Post<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigosExternos/" + 12 + "/" + 999, envio);

                var user = BuscarUsuarios(result.Select(u => u.IdUsuario));

                var espc = BuscarEspcs(result.Select(u => u.ID));

                var nespc = BuscarEspecialidadesPorIds(espc.Select(e => e.EspecialidadeId));

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Foto = user.ElementAtOrDefault(i).ProfileAvatar;
                    result.ElementAtOrDefault(i).Extensao = user.ElementAtOrDefault(i).AvatarExtension.Replace(".", "");
                    if (nespc.ElementAtOrDefault(i) != null)
                    {
                        result.ElementAtOrDefault(i).Especialidade = nespc.ElementAtOrDefault(i).Nome;
                    }
                    var pagina = BuscarPagina(result.ElementAtOrDefault(i).IdUsuario);
                    result.ElementAtOrDefault(i).DescricaoWhiteLabel = pagina.Apresentacao;
                }

                return result.Where(r => r.Ativo);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<MedicoViewModel>> GetMedicosListagem(IEnumerable<int> ids)
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
                var result = helper.Post<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigosExternos/" + 12 + "/" + 999, envio);

                var contador = result.Where(r => r.Ativo).Count();

                var user = BuscarUsuarios(result.Select(u => u.IdUsuario));

                var espc = BuscarEspcsMxE(result.Select(u => u.ID));

                //var nespc = BuscarEspecialidadesPorIds(espc.Select(e => e.EspecialidadeId));

                var tespc = await BuscarEspecialidadesAsync();

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Foto = user.ElementAtOrDefault(i).ProfileAvatar;
                    result.ElementAtOrDefault(i).Extensao = user.ElementAtOrDefault(i).AvatarExtension.Replace(".", "");
                    result.ElementAtOrDefault(i).Especialidade = tespc.FirstOrDefault(e => e.ID.Equals(espc.FirstOrDefault(a => a.MedicoId.Equals(result.ElementAtOrDefault(i).ID)).EspecialidadeId)).Nome;
                    var pagina = BuscarPagina(result.ElementAtOrDefault(i).IdUsuario);
                    result.ElementAtOrDefault(i).DescricaoWhiteLabel = pagina.Apresentacao;
                }

                return result.Where(r => r.Ativo);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public IEnumerable<MedicoViewModel> GetMedicosID(IEnumerable<int> ids)
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
                var result = helper.Post<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigos/" + 12 + "/" + 999, envio);

                var user = BuscarUsuarios(result.Select(u => u.IdUsuario));

                var espc = BuscarEspcs(result.Select(u => u.ID));

                var nespc = BuscarEspecialidadesPorIds(espc.Select(e => e.EspecialidadeId));

                for (int i = 0; i < result.Count(); i++)
                {
                    result.ElementAtOrDefault(i).Foto = user.ElementAtOrDefault(i).ProfileAvatar;
                    result.ElementAtOrDefault(i).Extensao = user.ElementAtOrDefault(i).AvatarExtension.Replace(".", "");
                    if (nespc.ElementAtOrDefault(i) != null)
                    {
                        result.ElementAtOrDefault(i).Especialidade = nespc.ElementAtOrDefault(i).Nome;
                    }
                    var pagina = BuscarPagina(result.ElementAtOrDefault(i).IdUsuario);
                    result.ElementAtOrDefault(i).DescricaoWhiteLabel = pagina.Apresentacao;
                }

                return result.Where(r => r.Ativo);

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<MedicoViewModel>> GetMedicosByIdsExternosSimpleAsync(IEnumerable<int> ids)
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
                var result = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigosExternos/" + 12 + "/" + 999, envio);

                return result.Where(r => r.Ativo);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public async Task<IEnumerable<MedicoViewModel>> GetMedicosByIdsExternosAsync(IEnumerable<int> ids)
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
                var result = await helper.PostAsync<IEnumerable<MedicoViewModel>>(keyUrl, "/Seguranca/WpMedicos/BuscarPorCodigosExternos/" + 12 + "/" + 999, envio);

                var users = BuscarUsuarios(result.Select(u => u.IdUsuario));

                var especialidades = BuscarEspcs(result.Select(u => u.ID));

                var resultEsp = BuscarEspecialidadesPorIds(especialidades.Select(e => e.EspecialidadeId));

                for (int i = 0; i < result.Count(); i++)
                {
                    var medico = result.ElementAtOrDefault(i);
                    var user = users.ElementAtOrDefault(i);
                    var esp = resultEsp.ElementAtOrDefault(i);

                    if (medico != null)
                    {
                        medico.Foto = user?.ProfileAvatar;
                        medico.Extensao = user?.AvatarExtension.Replace(".", "");
                        medico.Especialidade = esp?.Nome;
                        var pagina = BuscarPagina(medico.IdUsuario);

                        if (pagina != null)
                        {
                            medico.DescricaoWhiteLabel = pagina.Apresentacao;
                        }
                    }
                }

                return result.Where(r => r.Ativo);
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o paciente.", e);
            }
        }

        public IEnumerable<UsuarioViewModel> BuscarUsuarios(IEnumerable<int> idsUsuarios)
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
                var result = helper.Post<IEnumerable<UsuarioViewModel>>(keyUrl, $"/Seguranca/Principal/BuscarUsuarios/12/999", envio);


                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar o usuario.", e);
            }
        }

        public IEnumerable<EspecialidadeViewModel> BuscarEspcs(IEnumerable<int> id)
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
                var result = helper.Post<IEnumerable<EspecialidadeViewModel>>(keyUrl, $"/Seguranca/WpMedicos/BuscarEspcPorIdsAsync/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a espc.", e);
            }
        }

        public IEnumerable<MedicoXEspecialidadeViewModel> BuscarEspcsMxE(IEnumerable<int> id)
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
                var result = helper.Post<IEnumerable<MedicoXEspecialidadeViewModel>>(keyUrl, $"/Seguranca/WpMedicos/BuscarEspcPorIdsAsync/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a espc.", e);
            }
        }

        public IEnumerable<MedicoXEspecialidadeViewModel> BuscarEspcsSimples(int id)
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString(); ;

                List<int> idEs = new List<int>();

                idEs.Add(id);

                var envio = new
                {
                    idCliente = 12,
                    ids = idEs
                };

                var helper = new ServiceHelper();
                var result = helper.Post<IEnumerable<MedicoXEspecialidadeViewModel>>(keyUrl, $"/Seguranca/WpMedicos/BuscarEspcPorIdsAsync/12/999", envio);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar a espc.", e);
            }
        }

        public IEnumerable<EspecialidadeViewModel> BuscarEspecialidadesPorIds(IEnumerable<int> idsEspc)
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
                var result = helper.Post<IEnumerable<EspecialidadeViewModel>>(url,
                    $"/Seguranca/WpMedicos/BuscarEspcPorIds/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }

        public async Task<MedicoViewModel> GetMedico(int idMedico)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["UrlAPI"];

                var envio = new
                {
                    idCliente = 12,
                    medicoId = idMedico
                };

                var helper = new ServiceHelper();
                var result = await helper.PostAsync<MedicoViewModel>(url,
                    $"/Seguranca/WpMedicos/BuscarMedico/12/999", envio);



                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível buscar as especialidades");
            }
        }
    }
}