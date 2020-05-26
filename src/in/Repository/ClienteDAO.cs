using System;
using System.Collections.Generic;
using Entity;
using System.Linq;

namespace WebPixRepository
{
    public class ClienteDAO
    {
        public static List<Cliente> GetAll()
        {
            try
            {
                using (var db = new WebPixInContext())
                {
                    return db.Cliente.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Cliente>();
            }

        }

        public static string Save(Cliente obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixInContext())
                    {
                        db.Cliente.Add(obj);
                        db.SaveChanges();
                    }
                    return "Cliente salvo com sucesso";
                }
                else
                {
                    using (var db = new WebPixInContext())
                    {

                        db.Cliente.Update(obj);
                        db.SaveChanges();
                        return "Cliente salvo com sucesso";
                    }
                }
            }
            catch (Exception e)
            {
                return "Houve falha";
            }
        }
    }
}
