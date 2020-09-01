using System;
using System.Collections.Generic;
using wpEntity;
using System.Linq;
using wpContextEntrega;

namespace wpRepository
{
    public class PropiedadeRep
    {
        public static bool Save(Propiedade obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixEntregaContext())
                    {
                        db.Propiedade.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixEntregaContext())
                    {

                        db.Propiedade.Update(obj);
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
        public static List<Propiedade> GetAll()
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    return db.Propiedade.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Propiedade>();
            }

        }
        public static bool Remove(Propiedade obj)
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    db.Propiedade.Remove(obj);
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
