using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class TokenDAO
    {
        public static bool Save(Token obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Token.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Token.Update(obj);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<Token> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Token.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Token>();
            }

        }

        public static Token GetToken(string token)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Token.Where(t => t.GuidSec.Equals(token) && t.Ativo == true).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                
                throw;
            }

        }

        public static bool Remove(Token obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Token.Remove(obj);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                ////
                return false;
            }

        }

        public static Token GetLastToken(int idUsuario, int idCliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var token = db.Token.Where(c => c.idUsuario == idUsuario && c.idCliente == idCliente && c.Ativo == true).FirstOrDefault();

                    if (token != null)
                    {
                       if(token.DataExpiracao <= DateTime.Now)
                        {
                            token.Ativo = false;
                            token.Valido = "false";
                            token.DateAlteracao = DateTime.Now;
                            db.SaveChanges();

                            return null;
                        }
                    }
                    
                    return token;
                }
            }
            catch(Exception ex)
            {
                return new Token
                {
                    Nome = ex.Message,
                    ID = 0,
                    Descricao = ex.InnerException.Message
                };
            }
        }
    }
}
