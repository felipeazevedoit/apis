using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPixRepository;

namespace Repository
{
    public class MotorAuxDAO
    {
        public static List<MotorAux> GetAll()
        {
            try
            {
                using (var db = new WebPixInContext())
                {
                    return db.MotorAux.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<MotorAux>();
            }

        }

        public static string Save(MotorAux obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixInContext())
                    {
                        db.MotorAux.Add(obj);
                        db.SaveChanges();
                    }
                    return "Cliente salvo com sucesso";
                }
                else
                {
                    using (var db = new WebPixInContext())
                    {

                        db.MotorAux.Update(obj);
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
