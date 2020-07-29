using System;
using System.Collections.Generic;
using Entity;
using System.Linq;

namespace Repository
{
    public class PrecoRep
    {
        public static bool Save(Preco obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Preco.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Preco.Update(obj);
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
        public static List<Preco> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Preco.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Preco>();
            }

        }
        public static bool Remove(Preco obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Preco.Remove(obj);
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
