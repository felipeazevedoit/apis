using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpCotacao.Entidades;
using WpCotacao.Repositorio;
using WpCotacao.Servico;

namespace WpCotacao.Dominio
{
    public class UsuarioBO
    {
        public async Task<Usuario> Save(Usuario usuario,string token)
        {
            if (usuario.Id != 0)
            {
                if (await SeguracaServ.validaTokenAsync(token))
                {
                    if (usuario.idCliente != 0)
                    {
                        UsuarioRep rep = new UsuarioRep();
                        try { return (Usuario)rep.Add(usuario); } catch { return new Usuario(); }
                    }
                    else
                        return new Usuario();
                }
                else
                    return new Usuario();
            }
            else
            {
                if (await SeguracaServ.validaTokenAsync(token))
                {
                    if (usuario.idCliente != 0)
                    {
                        UsuarioRep rep = new UsuarioRep();
                        try
                        {
                            rep.Update(usuario);
                            return usuario;
                        }
                        catch
                        {
                            return new Usuario();
                        }
                    }
                    else
                        return new Usuario();
                }
                else
                    return new Usuario();
            }
           
        }

        public async Task<List<Usuario>> GetAll(int idCliente, string token)
        {
            UsuarioRep rep = new UsuarioRep();
            if (await SeguracaServ.validaTokenAsync(token))
                return rep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Usuario>();
        }
        public async Task<Usuario> Get(int idCliente, string token,int idUsuario)
        {
            UsuarioRep rep = new UsuarioRep();
            if (await SeguracaServ.validaTokenAsync(token))
                return rep.GetAll().FirstOrDefault(x => x.idCliente == idCliente && x.Id == idUsuario);
            else
                return new Usuario();
        }
    }
}
