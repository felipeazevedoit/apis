using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class ConfiguracaoDAO
    {
        public static bool Save(Configuracao obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Configuracao.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Configuracao.Update(obj);
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
        public static List<Configuracao> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Configuracao.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Configuracao>();
            }

        }
        public static Dictionary<string, string> GetParametros(int idcliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var lista = db.Configuracao.ToList().Where(x => x.idCliente == idcliente && x.Ativo == true);
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (Configuracao conf in lista)
                    {
                        dic.Add(conf.Chave, conf.Valor);
                    }
                    return dic;
                }
            }
            catch (Exception e)
            {
                ////
                return new Dictionary<string, string>();
            }
           
        }
        public static bool Remove(Configuracao obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Configuracao.Remove(obj);
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
    }
}
