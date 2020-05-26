using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;

namespace RepositoryTest
{
    [TestClass]
    public class MenuTest
    {

        [TestMethod]
        public void CreateMenuTest()
        {
            var Menu = new Menu
            {
                Nome = "Teste1",
                Descricao = "Teste1",
                Url = "teste/teste",
                idCliente = 1,
                Pai = 1,
                UsuarioCriacao = 1,
                UsuarioEdicao = 1,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true.ToString(),
                Status = 1
            };

            var result = MenuDAO.Save(Menu);

        }
        [TestMethod]
        public void GetAllMenuTest()
        {
            var resut = MenuDAO.GetAll();
        }
        [TestMethod]
        public void EditMenuTest()
        {
            var obj = MenuDAO.GetAll().Find(x => x.ID == 1);
            obj.Nome = "Teste3";
            obj.Descricao = "Teste3";
            obj.Url = "Teste/teste";

            var result = MenuDAO.Save(obj);

        }
        [TestMethod]
        public void RemoveMenuTest()
        {
            var obj = MenuDAO.GetAll().Find(x => x.ID == 1);
            var result = MenuDAO.Remove(obj);

        }
    }
}
