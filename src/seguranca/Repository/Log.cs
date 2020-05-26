using System;

namespace Repository
{
    public class Log
    {
        public static bool Save(Entity.Log obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
              
                    using (var db = new WebPixContext())
                    {
                        db.Log.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
               
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
