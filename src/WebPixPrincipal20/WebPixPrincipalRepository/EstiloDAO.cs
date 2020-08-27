using System;
using System.Collections.Generic;
using WebPixPrincipalRepository.Entity;
using System.Linq;

namespace WebPixPrincipalRepository
{
    public class EstiloDAO
    {
        public static bool Save(Estilo obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Estilo.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Estilo.Update(obj);
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
        public static List<Estilo> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Estilo.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Estilo>();
            }

        }
        public static bool Remove(Estilo obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Estilo.Remove(obj);
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
