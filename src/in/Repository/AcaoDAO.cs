using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPixRepository;

namespace Repository
{
   public class AcaoDAO
    {
       
        public static IEnumerable<Acao> GetAll()
        {
            try
            {
                using (var db = new WebPixInContext())
                {
                    return db.Acao.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Acao>();
            }
        }

        public static string Save(Acao obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixInContext())
                    {
                        db.Acao.Add(obj);
                        db.SaveChanges();
                    }
                    return "Cliente salvo com sucesso";
                }
                else
                {
                    using (var db = new WebPixInContext())
                    {

                        db.Acao.Update(obj);
                        db.SaveChanges();
                        return "Cliente salvo com sucesso";
                    }
                }
            }
            catch (Exception e)
            {
                return "Houve falha";
            }
        }
    }
}
