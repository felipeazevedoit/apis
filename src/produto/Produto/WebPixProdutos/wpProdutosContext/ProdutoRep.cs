using System;
using System.Collections.Generic;
using Entity;
using System.Linq;

namespace Repository
{
    public class ProdutoRep
    {
        public static bool Save(Produto obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Produto.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Produto.Update(obj);
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
        public static List<Produto> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Produto .ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Produto>();
            }

        }
        public static bool Remove(Produto obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Produto.Remove(obj);
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
