using System;
using System.Collections.Generic;
using Entity;
using System.Linq;

namespace Repository
{
    public class ProdutoSkuRep
    {
        public static bool Save(ProdutoSku obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.ProdutoSku.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.ProdutoSku.Update(obj);
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
        public static List<ProdutoSku> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.ProdutoSku.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<ProdutoSku>();
            }

        }
        public static bool Remove(ProdutoSku obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.ProdutoSku.Remove(obj);
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
