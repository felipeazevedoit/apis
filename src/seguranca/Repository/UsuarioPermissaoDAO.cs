using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UsuarioPermissaoDAO
    {
        public static bool Save(UsuarioPermissao obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.UsuarioPermissao.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.UsuarioPermissao.Update(obj);
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
        public static List<UsuarioPermissao> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.UsuarioPermissao.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<UsuarioPermissao>();
            }

        }
        public static bool Remove(UsuarioPermissao obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.UsuarioPermissao.Remove(obj);
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
