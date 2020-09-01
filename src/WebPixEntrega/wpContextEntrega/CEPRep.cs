using System;
using System.Collections.Generic;
using wpEntity;
using System.Linq;
using wpContextEntrega;

namespace wpRepository
{
    public class CEPRep
    {
        public static bool Save(CEP obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixEntregaContext())
                    {
                        db.CEP.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixEntregaContext())
                    {

                        db.CEP.Update(obj);
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
        public static List<CEP> GetAll()
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    return db.CEP.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<CEP>();
            }

        }
        public static bool Remove(CEP obj)
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    db.CEP.Remove(obj);
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
