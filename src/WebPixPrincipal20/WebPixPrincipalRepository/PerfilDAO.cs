using System;
using System.Collections.Generic;
using System.Linq;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class PerfilDAO
    {
        public static bool Save(Perfil obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Perfil.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {
                        db.Perfil.Update(obj);
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
        public static List<Perfil> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Perfil.Where(x => x.Ativo == true).ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Perfil>();
            }

        }
        public static bool Remove(Perfil obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Perfil.Remove(obj);
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

        public static Perfil GetById(int id)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = db.Perfil.Where(p => p.ID.Equals(id)).SingleOrDefault();
                    return result;
                }
            }
            catch(Exception e)
            {
                return new Perfil();
            }
        }
    }
}
