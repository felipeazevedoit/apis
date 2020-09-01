using System;
using System.Collections.Generic;
using wpEntity;
using System.Linq;
using wpContextEntrega;

namespace wpRepository
{
    public class TransportadoraRep
    {
        public static bool Save(Transportadora obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixEntregaContext())
                    {
                        db.Transportadora.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixEntregaContext())
                    {

                        db.Transportadora.Update(obj);
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
        public static List<Transportadora> GetAll()
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    return db.Transportadora.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Transportadora>();
            }

        }
        public static bool Remove(Transportadora obj)
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    db.Transportadora.Remove(obj);
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
