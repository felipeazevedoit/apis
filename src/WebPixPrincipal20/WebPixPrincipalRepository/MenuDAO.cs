using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class MenuDAO
    {
        public static bool Save(Menu obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Menu.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Menu.Update(obj);
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
        public static List<Menu> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Menu.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Menu>();
            }

        }
        public static bool Remove(Menu obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Menu.Remove(obj);
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
