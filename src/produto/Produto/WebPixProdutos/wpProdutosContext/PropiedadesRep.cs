using System;
using System.Collections.Generic;
using Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PropiedadesRep
    {
        public static int Save(Propiedades obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.dataEdicao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Propiedades.Add(obj);
                        db.SaveChanges();
                    }
                    return obj.ID;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Propiedades.Update(obj);
                        db.SaveChanges();
                        return obj.ID;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static List<Propiedades> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Propiedades.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Propiedades>();
            }

        }
        public static bool Remove(Propiedades obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Propiedades.Remove(obj);
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


        #region Metodos exclusivos para salvamento de tabelas vinculados com outros motores (OLHAR ECO SISTEMA WP)

        public static bool SaveDept(List<Estrutura> departamento, int idUsuario, int idPropiedade,int idCliente)
        {

            foreach (Estrutura obj in departamento)
            {
                obj.dataCriacao = DateTime.Now;
                obj.dataEdicao = DateTime.Now;
                try
                {
                    var query = String.Format(
                                @"insert into EstruturaXPropriedade   
                                    values({0}, {1}, '{2}', '{3}', {4}, {5},{6})",
                                obj.ID,
                                idPropiedade,
                                String.Format("{0:yyyy/MM/dd}", DateTime.Now),
                                String.Format("{0:yyyy/MM/dd}", DateTime.Now),
                                idUsuario,
                                idUsuario,
                                idCliente);


                    using (var db = new WebPixContext())
                    {
                        int noOfRowInserted = db.Database.ExecuteSqlCommand(query);
                    }
                }
                catch (Exception e)
                {
                }

            }
            return true;


        }
        public static bool SaveArquivos(List<Arquivo> arquivosVinculado, int idUsuario, int idPropiedade,int idCliente)
        {

            foreach (Arquivo obj in arquivosVinculado)
            {
                obj.dataCriacao = DateTime.Now;
                obj.dataEdicao = DateTime.Now;
                try
                {
                    var query = String.Format(
                                @"insert into ArquivoXPropiedade   
                                    values({0}, {1}, '{2}', '{3}', {4}, {5},{6})",
                                obj.ID,
                                idPropiedade,
                                String.Format("{0:yyyy/MM/dd}", DateTime.Now),
                                String.Format("{0:yyyy/MM/dd}", DateTime.Now),
                                idUsuario,
                                idUsuario,
                                idCliente);


                    using (var db = new WebPixContext())
                    {
                        int noOfRowInserted = db.Database.ExecuteSqlCommand(query);
                    }
                }
                catch (Exception e)
                {
                }

            }
            return true;


        }

        #endregion


    }
}
