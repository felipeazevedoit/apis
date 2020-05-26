using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static UsuarioXPerfil SaveUsuarioXPerfil(UsuarioXPerfil obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DataEdicao = DateTime.Now;
            try
            {
                if (obj.Id == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.UsuarioXPerfil.Add(obj);
                        db.SaveChanges();
                    }
                    return obj;
                }
                else
                {
                    using (var db = new WebPixContext())
                    {
                        db.UsuarioXPerfil.Update(obj);
                        db.SaveChanges();
                        return obj;
                    }
                }
            }
            catch (Exception e)
            {
                return new UsuarioXPerfil();
            }
        }

        public async static Task<Perfil> CarregaPerfilByUsuario(int idUsuario, int idCliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var perfil = await db.Set<Perfil>().FromSql(@"
                            select a.* from Perfil a
                            inner join UsuarioXPerfil b on b.idUsuario = {0}
                            where a.id = b.IdPerfil", idUsuario).FirstOrDefaultAsync();

                    return perfil;
                }

            }
            catch (Exception e)
            {
                return new Perfil();
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
            catch (Exception e)
            {
                return new Perfil();
            }
        }

        public async static void DesvinculaPerfil(UsuarioXPerfil usuarioXPerfil)
        {
            using (var db = new WebPixContext())
            {
                db.UsuarioXPerfil.Remove(usuarioXPerfil);
                await db.SaveChangesAsync();
            }
        }

        public static UsuarioXPerfil GetPerfilByUsuario(int idUsuario)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = db.UsuarioXPerfil.Where(x => x.IdUsuario.Equals(idUsuario)).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                return new UsuarioXPerfil();
            }
        }

        public async static Task<IEnumerable<UsuarioXPerfil>> GetUsersIdsAsync(int idPerfil)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = await db.UsuarioXPerfil.Where(x => x.IdPerfil.Equals(idPerfil)).ToListAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                return new List<UsuarioXPerfil>();
            }
        }

        public static IEnumerable<UsuarioXPerfil> GetPerfisByUsuarios(IEnumerable<int> idsUsuarios)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var result = db.UsuarioXPerfil.Where(x => idsUsuarios.Contains(x.IdUsuario)).ToList();
                    return result;
                }

            }
            catch (Exception e)
            {
                return new List<UsuarioXPerfil>();
            }
        }


    }
}
