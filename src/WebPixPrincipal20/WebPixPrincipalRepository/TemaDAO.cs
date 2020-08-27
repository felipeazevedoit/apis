using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
   public class TemaDAO
    {
        public static bool Save(Tema obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Tema.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    obj.DateAlteracao = DateTime.Now;
                    using (var db = new WebPixContext())
                    {
                        db.Tema.Update(obj);
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
        public static List<Tema> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Tema.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Tema>();
            }

        }
        public static bool Remove(Tema obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Tema.Remove(obj);
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
