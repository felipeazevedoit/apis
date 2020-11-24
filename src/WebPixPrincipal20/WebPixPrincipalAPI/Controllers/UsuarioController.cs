using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using Microsoft.AspNetCore.Cors;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;
using WebPixPrincipalAPI.Model;
using System;
using CustomExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebPixPrincipalBLL;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class UsuarioController : Controller
    {
        private readonly RemetenteObject _remetente;
        public UsuarioController(IOptions<RemetenteObject> remetente)
        {
            _remetente = remetente.Value;
        }

        [ActionName("SaveUsuario")]
        [HttpPost("{token}")]
        public async Task<JsonResult> SaveUsuario([FromBody]Usuario usuario, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                try
                {
                    var newUsuario = UsuarioDAO.Save(usuario);

                    if (newUsuario != null && newUsuario.ID > 0 && usuario.ProfileAvatar != null && usuario.ProfileAvatar.Count() > 0 && !string.IsNullOrEmpty(usuario.AvatarExtension))
                    {
                        newUsuario.Avatar = FileSystemManager.SaveFile($"{ newUsuario.Nome }_{ newUsuario.ID }.{ usuario.AvatarExtension }", usuario.ProfileAvatar);
                    }

                    if (usuario.EnviarEmail)
                    {
                        if (newUsuario != null && newUsuario.ID > 0 && newUsuario.idCliente != 15)
                        {
                            var email = new EmailHandler(_remetente);
                            await email.EnviarEmailAsync(newUsuario);
                        }
                        else if(newUsuario != null && newUsuario.ID > 0 && newUsuario.idCliente == 15)
                        {
                            var email = new EmailHandler(_remetente);
                            await email.EnviarEmailUlabelAsync(newUsuario);
                        }
                    }

                    if(usuario.idCliente == 12 && "saudesite".Equals(usuario.Origem))
                    {
                        var perfis = await Seguranca.GetUsuariosAsync(13);
                        var admins = UsuarioDAO.GetByIds(12, perfis.Select(x => x.IdUsuario));

                        var email = new EmailHandler(_remetente);
                        await email.EnviarEmail(usuario.Tipo, newUsuario.ID, admins);
                    }

                    return Json(newUsuario);
                }
                catch (Exception e)
                {
                    return Json("Encontramos algum problema ao salvar o usuario. Entre em contato com o suporte");
                }
            }
            else
            {
                return Json("Você nao tem acesso a esse plugin");
            }
        }

        [ActionName("GetAllUsuario")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Usuario>> GetAllUsuario(int idcliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var usuarios = UsuarioDAO.GetAll(idcliente);

                foreach (var item in usuarios)
                {
                    FileSystemManager.GetFile($"{ item.Nome }_{ item.ID }", out var path, out var extension, out var file);
                    item.AvatarExtension = extension;
                    item.Avatar = path;
                    item.ProfileAvatar = file;
                }

                return usuarios;
            }
            else
                return new List<Usuario>();

        }

        [ActionName("GetUsuarioById")]
        [HttpGet("{idCliente:int}/{idUsuario:int}/{token}")]
        public async Task<Usuario> GetUsuarioById(int idCliente, int idUsuario, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var usuario = UsuarioDAO.GetById(idCliente, idUsuario);
                
                if (usuario != null)
                {
                    FileSystemManager.GetFile($"{ usuario.Nome }_{ usuario.ID }", out var path, out var extension, out var file);
                    usuario.AvatarExtension = extension;
                    usuario.Avatar = path;
                    usuario.ProfileAvatar = file;
                }

                return usuario;
            }
            else
            {
                return new Usuario();
            }
        }

        [ActionName("LoginUsuario")]
        [HttpPost("{token}")]
        public async Task<Usuario> LoginUsuario([FromBody]object ObjLogin, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                dynamic obj = ObjLogin;
                string login = obj.Login;
                string senha = obj.Senha;
                int idCliente = obj.idCliente;
                Usuario user = UsuarioDAO.GetUsuario(login, senha, idCliente);
                if (user != null)
                {
                    //FileSystemManager.GetFile($"{ user.Nome }_{ user.ID }", out var path, out var extension, out var file);
                   // user.AvatarExtension = extension;
                    //user.Avatar = path;
                    //user.ProfileAvatar = file;

                    return user;
                }
                else
                    return new Usuario();


            }
            else
                return new Usuario();
        }

        [ActionName("DeletarUsuario")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarUsuario([FromBody]object Usuario, string token)
        {
            dynamic objEn = Usuario;
            string a = objEn.idUsuario.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Usuario obj = UsuarioDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                var msg = new { msg = UsuarioDAO.Remove(obj) };

                return Json(msg);
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }

        [ActionName("GetByUsuariosIds")]
        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IEnumerable<Usuario>> GetByUsuariosIds([FromRoute]string token, [FromBody]IEnumerable<int> ids, int idCliente)
        {
            if(await Seguranca.validaTokenAsync(token))
            {
                var usuarios = UsuarioDAO.GetByIds(idCliente, ids);

                foreach (var item in usuarios)
                {
                    FileSystemManager.GetFile($"{ item.Nome }_{ item.ID }", out var path, out var extension, out var file);
                    item.AvatarExtension = extension;
                    item.Avatar = path;
                    item.ProfileAvatar = file;
                }

                return usuarios;
            }
            else
            {
                return new List<Usuario>();
            }
        }

        [ActionName("GerarNovaSenha")]
        [HttpPost("{idCliente:int}/{token}")]
        public async Task<IActionResult> GerarNovaSenhaAsync([FromRoute]int idCliente,[FromRoute]string token, [FromBody]Usuario usuario)
        {
            try
            {
                if (await Seguranca.validaTokenAsync(token))
                {
                    var user = UsuarioDAO.GetByEmail(usuario.Login, idCliente);
                    var genericString = Guid.NewGuid().ToString();
                    var newPassword = genericString.Length > 5 ? genericString.Substring(0, 5) : genericString;

                    user.Senha = newPassword;

                    if(user != null && user.ID > 0)
                    {
                        var updated = UsuarioDAO.Save(user);
                        if(updated != null && updated.ID> 0)
                        {
                            Email emai = new Email();
                            emai.Conteudo = "Solicitação de nova senha \r \r Nova Senha : " + newPassword;
                            emai.Titulo = "Nova Senha";
                            string remetente = "noreplay@imedfit.com";
                            string destinatario = user.Email;
                            
                            
                            updated.Senha = user.Senha;
                            var email = new EmailBO();
                            await email.EnviaSimplesEmailAsync(emai,remetente,destinatario,idCliente);
                        }
                    }

                    return Ok(user);
                }

                return StatusCode(400, "Não houve permissão para acessar o recurso solicitado.");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [ActionName("VerificarEmail")]
        [HttpPost("{token}")]
        public async Task<IActionResult> VerificarEmailAsync([FromRoute]string token, [FromBody]Usuario usuario)
        {
            try
            {
                if (await Seguranca.validaTokenAsync(token))
                {
                    var user = UsuarioDAO.GetByEmail(usuario.Login, usuario.idCliente);
                    
                    if(user == null || user.ID == 0)
                    {
                        return Ok(false);
                    }

                    return Ok(true);
                }

                return StatusCode(400, "Não houve permissão para acessar o recurso solicitado.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
