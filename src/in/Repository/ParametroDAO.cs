using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPixRepository;

namespace Repository
{
    public class ParametroDAO
    {
        public static List<Parametro> GetAll()
        {
            try
            {
                using (var db = new WebPixInContext())
                {
                    return db.Parametro.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Parametro>();
            }

        }

        public static string Save(Parametro obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixInContext())
                    {
                        db.Parametro.Add(obj);
                        db.SaveChanges();
                    }
                    return "Cliente salvo com sucesso";
                }
                else
                {
                    using (var db = new WebPixInContext())
                    {

                        db.Parametro.Update(obj);
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
