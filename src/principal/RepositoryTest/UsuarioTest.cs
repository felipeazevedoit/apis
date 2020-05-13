using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;


namespace RepositoryTest
{
    [TestClass]
   public class UsuarioTest
    {
        [TestMethod]
        public void CreateUsuarioTest()
        {
            var Usuario = new Usuario
            {
                Nome = "Teste5",
                Descricao = "Teste5",
                Email = "test@test4.tes",
                Login = "Teste5",
                Senha = "Teste5",
                //PerfilUsuario =2,
                SobreNome = "Teste5",
                UsuarioCriacao = 1,
                UsuarioEdicao = 1,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true.ToString(),
                Status = 1,
                idCliente = 1
            };

            var result = UsuarioDAO.Save(Usuario);

        }
        [TestMethod]
        public void GetAllUsuarioTest()
        {
            var resut = UsuarioDAO.GetAll();
        }
        [TestMethod]
        public void EditUsuarioTest()
        {
            var obj = UsuarioDAO.GetAll().Find(x => x.ID == 2);
            obj.Nome = "Teste3";
            obj.Descricao = "Teste3";
            var result = UsuarioDAO.Save(obj);

        }
        [TestMethod]
        public void RemoveUsuarioTest()
        {
            var obj = UsuarioDAO.GetAll().Find(x => x.ID == 2);
            var result = UsuarioDAO.Remove(obj);

        }
    }
}
