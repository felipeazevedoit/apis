using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class ArquivoDAO
    {
        public static bool Save(Arquivo obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Arquivo.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Arquivo.Update(obj);
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
        public static List<Arquivo> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Arquivo.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Arquivo>();
            }

        }
        public static bool Remove(Arquivo obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Arquivo.Remove(obj);
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
