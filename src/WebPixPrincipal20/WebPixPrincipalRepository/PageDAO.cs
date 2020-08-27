using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class PageDAO
    {
        public static bool Save(Page obj)
        {
            try
            {
                if (obj.ID == 0)
                {
                    obj.DataCriacao = DateTime.Now;
                    obj.DateAlteracao = DateTime.Now;
                    using (var db = new WebPixContext())
                    {
                        db.Page.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    obj.DateAlteracao = DateTime.Now;
                    using (var db = new WebPixContext())
                    {
                        db.Page.Update(obj);
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
        public static List<Page> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var b = db.Page.Where(x => x.Ativo == true).ToList();
                    return b;
                }
            }
            catch(Exception e)
            {
                ////
                return new List<Page>();
            }
            
        }
        public static bool Remove(Page obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Page.Remove(obj);
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
