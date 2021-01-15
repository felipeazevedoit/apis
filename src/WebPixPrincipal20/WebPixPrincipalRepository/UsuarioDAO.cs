using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class UsuarioDAO
    {
        public static Usuario Save(Usuario obj)
        {
            obj.DataCriacao = DateTime.Now;
            obj.DateAlteracao = DateTime.Now;

            if (!string.IsNullOrEmpty(obj.Senha))
            {
                var password = HashPassword(obj.Senha);

                obj.Salt = password.Key;
                obj.Password = password.Value;
            }

            try
            {
                if (obj.ID == 0)
                {
                    using (var db = new WebPixContext())
                    {
                        db.Usuario.Add(obj);
                        db.SaveChanges();
                        return obj;
                    }
                }
                else
                {
                    obj.DateAlteracao = DateTime.Now;
                    using (var db = new WebPixContext())
                    {
                        db.Usuario.Update(obj);
                        db.SaveChanges();
                        return obj;
                    }
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }
        public static List<Usuario> GetAll()
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var usuarios = db.Usuario.Where(x => x.Ativo == true).ToList();
                    return usuarios;
                }
            }
            catch (Exception e)
            {
                return new List<Usuario>();
            }

        }
        public static List<Usuario> GetAll(int idCliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    return db.Usuario.Where(x => x.Ativo && x.idCliente.Equals(idCliente)).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Usuario>();
            }

        }
        public static bool Remove(Usuario obj)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    db.Usuario.Remove(obj);
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

        public static Usuario GetById(int idCliente, int id)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var user = db.Usuario.Where(x => x.idCliente.Equals(idCliente) && x.ID.Equals(id)).SingleOrDefault();
                    return user;
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }

        public static IEnumerable<Usuario> GetByIds(int idCliente, IEnumerable<int> ids)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var users = db.Usuario.Where(x => x.idCliente.Equals(idCliente) && ids.Contains(x.ID));
                    return users.ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Usuario>();
            }
        }

        public static Usuario GetUsuario(string login, string senha, int idCliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var user = db.Usuario.Where(x => x.idCliente.Equals(idCliente) && x.Login.Equals(login)).First();
                    //var user =  users.FirstOrDefault();

                    if (user != null)
                    {
                        var result = VerifyPassword(user.Salt, user.Password, senha);

                        if (result)
                        {
                            return user;
                        }
                    }
                    var teste = GetByEmail(login);
                    throw new Exception("Usuário não encontrado.");
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }

        public static Usuario GetByEmail(string login)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var users = db.Usuario.Where(x => x.Login.Equals(login));
                    return users.FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                return new Usuario();
            }
        }

        public static Usuario GetByEmail(string login, int idCliente)
        {
            try
            {
                using (var db = new WebPixContext())
                {
                    var users = db.Usuario.Where(x => x.Login.Equals(login) && x.idCliente.Equals(idCliente));
                    return users.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return new Usuario();
            }
        }

        public static KeyValuePair<string, string> HashPassword(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new KeyValuePair<string, string>(Convert.ToBase64String(salt), hashed);
        }

        private static bool VerifyPassword(string salt, string password, string senha)
        {
            var saltBytes = Convert.FromBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return true; //hashed.Equals(password);
        }
    }
}
