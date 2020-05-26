using Newtonsoft.Json;
using SaudeComVoce.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace SaudeComVoce.Helpers
{
    public static class PixCoreValues
    {
        private static readonly HttpContext _current = HttpContext.Current;

        #region Propiedades
        public static LoginViewModel UsuarioLogado
        {
            get => VerificaLogado();
        }

        private static int _idCliente;
        public static int IdCliente
        {
            get
            {
                string url = _current.Request.Url.Host;
                int porta = _current.Request.Url.Port;
                string protocolo = _current.Request.Url.Scheme;

                var urlDoCliente = protocolo + "://" + url + ":" + porta.ToString() + HttpContext.Current.Request.Url.PathAndQuery;
                var DefaultSiteUrl = protocolo + "://" + url + ":" + porta.ToString() + "/";

                if (!string.IsNullOrEmpty(_current.Request.Cookies["IdClienteSaudeSite"].Value))
                {
                    var cookiesValido = _current.Request.Cookies["IdClienteSaudeSite"].Value;
                    var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                    _idCliente = jss.Deserialize<int>(cookiesValido);
                    return _idCliente;
                }
                else
                {
                    _current.Response.Redirect(DefaultSiteUrl);
                    return 0;
                }

            }

        }

        private static string _defaultSiteUrl;
        public static string DefaultSiteUrl
        {
            get
            {
                string url = _current.Request.Url.Host;
                int porta = _current.Request.Url.Port;
                string protocolo = _current.Request.Url.Scheme;
                if (porta != 80)
                {
                    _defaultSiteUrl = protocolo + "://" + url + ":" + porta.ToString() + "/";
                }
                else
                {
                    _defaultSiteUrl = protocolo + "://" + url + "/";
                }
                return _defaultSiteUrl;
            }
        }
        #endregion 

        [OutputCache(Duration = 120)]
        public async static Task<bool> LoginAsync(this ControllerBase page, LoginViewModel user)
        {
            user.idCliente = IdCliente;

            using (var client = new WebClient())
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();
                var url = keyUrl + "/Seguranca/Principal/loginUsuario/" + IdCliente + "/" + 999;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var envio = new { ObjLogin = user };
                var data = JsonConvert.SerializeObject(envio);
                var result = client.UploadString(url, "POST", data);
                var Usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(result);

                Usuario.UsuarioXPerfil = await GetPerfilUsuarioAsync(Usuario.ID);

                if (Usuario.ID != 0 && Usuario.Ativo && Usuario.Status != 9)
                {
                    if (Usuario.Ativo)
                    {
                        user.idCliente = Usuario.IdCliente;
                        user.IdUsuario = Usuario.ID;
                        user.Nome = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(Usuario.Nome));
                        user.Avatar = Usuario.Avatar;
                        user.idPerfil = Usuario.UsuarioXPerfil == null ? 0 : Usuario.UsuarioXPerfil.IdPerfil;
                        user.ProfileAvatar = Usuario.ProfileAvatar;
                        user.AvatarExtension = Usuario.AvatarExtension.Replace(".", string.Empty);

                        if (_current.Response.Cookies.AllKeys.Contains($"UsuarioLogadoSaudeSite-{user.IdUsuario}") && _current.Request.Cookies[$"UsuarioLogadoSaudeSite-{user.IdUsuario}"] != null)
                        {
                            _current.Request.Cookies[$"UsuarioLogadoSaudeSite-{user.IdUsuario}"].Value = string.Empty;
                        }

                        _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{user.IdUsuario}"].Value = JsonConvert.SerializeObject(user);
                        _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{user.IdUsuario}"].Expires = DateTime.Now.AddMinutes(30); // add expiry time
                        _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{user.IdUsuario}"].HttpOnly = true;                       

                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public async static Task<UsuarioXPerfilViewModel> GetPerfilUsuarioAsync(int id)
        {
            var keyUrl = ConfigurationManager.AppSettings["UrlAPI"].ToString();

            var helper = new ServiceHelper();
            var usuarioXPerfil = await helper.GetAsync<UsuarioXPerfilViewModel>(keyUrl, $"/Perfil/GetPerfilByUsuario/{ id }");

            return usuarioXPerfil;
        }

        public async static void AtualizarUsuarioLogado(UsuarioViewModel usuario)
        {
            usuario.UsuarioXPerfil = await GetPerfilUsuarioAsync(usuario.ID);
            string userId = HttpContext.Current.User?.Identity.Name;

            var login = new LoginViewModel()
            {
                idCliente = usuario.IdCliente,
                idPerfil = usuario.UsuarioXPerfil == null ? 0 : usuario.UsuarioXPerfil.IdPerfil,
                IdUsuario = usuario.ID,
                Nome = usuario.Nome,
                Avatar = usuario.Avatar,
                ProfileAvatar = usuario.ProfileAvatar,
                AvatarExtension = usuario.AvatarExtension,
            };

            _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value = null;
            _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value = new JavaScriptSerializer().Serialize(login);
        }

        public static void AtualizarUsuario(UsuarioViewModel usuario)
        {
            string userId = _current.Request.Cookies["UsuarioLogado"]?.Value;

            var login = new LoginViewModel()
            {
                idCliente = usuario.IdCliente,
                idPerfil = usuario.UsuarioXPerfil == null ? 0 : usuario.UsuarioXPerfil.IdPerfil,
                IdUsuario = usuario.ID,
                Nome = usuario.Nome,
                Avatar = usuario.Avatar,
            };

            _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value = null;
            _current.Response.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value = new JavaScriptSerializer().Serialize(login);
        }

        public static LoginViewModel VerificaLogado()
        {
            string userId = HttpContext.Current.User?.Identity.Name;

            if (_current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"] != null && !string.IsNullOrEmpty(_current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value))
            {
                var cookiesValido = _current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value;

                var Usuario = JsonConvert.DeserializeObject<LoginViewModel>(cookiesValido);
                return Usuario;
            }
            else
            {
                //current.Response.Redirect("http://localhost:49983/login/login");
                return new LoginViewModel();
            }
        }

        public static int VerificaUrlCliente(string urlDoCliente)
        {
            return 12;
        }

        public static void RenderUrlPage(HttpContext context)
        {
            var keyUrlIn = ConfigurationManager.AppSettings["UrlAPI"].ToString();
            var urlAPIIn = keyUrlIn + "/Seguranca/Principal/buscarEstilo/" + IdCliente + "/" + 999;
            var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var result = client.DownloadString(string.Format(urlAPIIn));

            var cliente = JsonConvert.DeserializeObject<PageViewModel[]>(result);

            string url = _current.Request.Url.Host;
            int porta = _current.Request.Url.Port;
            string protocolo = _current.Request.Url.Scheme;
            var urlDoCliente = "";

            if (porta != 80)
            {
                urlDoCliente = protocolo + "://" + url + ":" + porta.ToString() + _current.Request.Url.PathAndQuery;
            }
            else
            {
                urlDoCliente = protocolo + "://" + url + _current.Request.Url.PathAndQuery;
            }

            var page = cliente.Where(x => x.Url == urlDoCliente).FirstOrDefault();
            if (page != null)
            {
                if (_current.Request.Url.AbsoluteUri != (urlDoCliente + "page/index/" + page.ID.ToString()))
                {

                    _current.Response.Status = "301 Moved Permanently";
                    _current.Response.AddHeader("Location", DefaultSiteUrl + "page/index/" + page.ID.ToString());
                }
            }
            else
            {
                //TODO: Necessário? página criada localmente via html...
                //HttpContext.Current.Response.StatusCode = 404;
            }
        }

        public static void Sair()
        {
            string userId = _current.Request.Cookies["UsuarioLogado"]?.Value;

            if (_current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"] != null
                && !string.IsNullOrEmpty(_current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value))
            {
                _current.Request.Cookies[$"UsuarioLogadoSaudeSite-{userId}"].Value = null;
            }

            //if (_current.Request.Cookies["IdClienteSaudeSite"] != null
            //    && !string.IsNullOrEmpty(_current.Request.Cookies["IdClienteSaudeSite"].Value))
            //{
            //    _current.Request.Cookies["IdClienteSaudeSite"].Value = null;
            //}
        }
    }
}