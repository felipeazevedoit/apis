using System;
using System.Collections.Generic;
using wpEntity;
using System.Linq;
using wpContextEntrega;

namespace wpRepository
{
    public class ValorRep
    {
        public static bool Save(Valor obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixEntregaContext())
                    {
                        db.Valor.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixEntregaContext())
                    {

                        db.Valor.Update(obj);
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
        public static List<Valor> GetAll()
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    return db.Valor.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Valor>();
            }

        }
        public static bool Remove(Valor obj)
        {
            try
            {
                using (var db = new WebPixEntregaContext())
                {
                    db.Valor.Remove(obj);
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
