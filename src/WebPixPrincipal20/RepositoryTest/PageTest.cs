using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;

namespace RepositoryTest
{
    [TestClass]
    public class PageTest
    {
        [TestMethod]
        public void CreatePageTest()
        {
            var page = new Page
            {
                Nome = "Teste3",
                Descricao = "Teste3",
                Titulo = "Teste3",
                Url = "Teste3",
                Conteudo = "Teste3",
                UsuarioCriacao = 1,
                UsuarioEdicao = 1,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true.ToString(),
                Status = 1
            };

            var result = PageDAO.Save(page);

        }
        [TestMethod]
        public void GetAllPageTest()
        {
            var resut = PageDAO.GetAll();
        }
        [TestMethod]
        public void EditPageTest()
        {
            var obj = PageDAO.GetAll().Find(x => x.ID == 1);
            obj.Nome = "Teste3";
            obj.Descricao = "Teste3";
            obj.Conteudo = "Teste3";

            var result = PageDAO.Save(obj);

        }
        [TestMethod]
        public void RemovePageTest()
        {
            var obj = PageDAO.GetAll().Find(x => x.ID == 1);
            var result = PageDAO.Remove(obj);

        }
    }
}
