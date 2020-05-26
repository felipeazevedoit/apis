using System;
using Entity;
using Repository;

namespace SegurancaBO
{
    public static class TokenBO
    {
        public static Token GerateTokenValido(string acao, int idusuario, int idCliente, string ip)
        {

            try
            {
                //var lastToken = TokenDAO.GetLastToken(idusuario, idCliente);
                //if (lastToken != null)
                //{
                //    return lastToken;
                //}
                //else
                //{
                    var token = CreateToken(acao, idusuario, idCliente, ip);

                   //TokenDAO.Save(token);
                    return token;
               // }
            }
            catch (Exception ex)
            {
                return new Token
                {
                    Nome = ex.Message,
                    ID = 0,
                    Descricao = ex.InnerException.Message
                };
            }
        }


        public static Token ValidaToken(string tokenPass, int Acao, int idAux)
        {
            // return TokenDAO.GetToken(tokenPass);
            return new Token
            {
                DataExpiracao = DateTime.Now.AddHours(2),
                GuidSec = Guid.NewGuid().ToString(),
                IP = "127.0.0.1",
                Nome = "TESTE",
                idCliente = 1,
                idUsuario = 1,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true,
                Descricao = "TESTE",
                Valido = "true",
                UrlCliente = "TESTE"
            };

        }

        public static Token CreateToken(string acao, int idusuario, int idCliente, string ip)
        {
            return new Token
            {
                DataExpiracao = DateTime.Now.AddHours(2),
                GuidSec = Guid.NewGuid().ToString(),
                IP = ip,
                Nome = acao,
                idCliente = idCliente,
                idUsuario = idusuario,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true,
                Descricao = acao,
                Valido = "true",
                UrlCliente = acao
            };


        }

    }
}
