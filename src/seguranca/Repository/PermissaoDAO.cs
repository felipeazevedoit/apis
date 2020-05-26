using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PermissaoDAO
    {
        public static bool Save(Permissao obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;
            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Permissao.Add(obj);
                        db.SaveChanges();
                    }
                    return true;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {

                        db.Permissao.Update(obj);
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

        public async static Task<IEnumerable<Permissao>> GetByIdsAsync(IEnumerable<int> ids)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = await db.Permissao.Where(p => ids.Contains(p.ID) && Convert.ToBoolean(p.VAdmin)).ToListAsync();
                    return result;
                }
            }
            catch(Exception e)
            {
                return new List<Permissao>();
            }
        }

        public static List<Permissao> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Permissao.ToList();
                }
            }
            catch (Exception e)
            {
                ////
                return new List<Permissao>();
            }

        }
        public static bool Remove(Permissao obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Permissao.Remove(obj);
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

        public static async Task<Permissao> CarregarPermissaoByUsuarioAsync(int idUsuario)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var permissao = await db.Set<Permissao>().FromSql(@"
                            select a.* from Permissao a
                            inner join PermissaoXUsuario b on b.idUsuario = {0}
                            where a.id = b.idPermissao", idUsuario).FirstOrDefaultAsync();

                    return permissao;
                }

            }
            catch(Exception e)
            {
                return new Permissao();

            }
        }

        public async static Task<IEnumerable<Permissao>> GetByIdsAndMotor(IEnumerable<int> permissoesIDs, int idMotorAux)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = await db.Permissao.Where(p => permissoesIDs.Contains(p.ID) 
                        && p.IdAux.Equals(idMotorAux)).ToListAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                return new List<Permissao>();
            }
        }
    }
}
