using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;

namespace RepositoryTest
{
    [TestClass]
    public class PerfilTest
    {
        [TestMethod]
        public void CreatePerfilTest()
        {
            var Perfil = new Perfil
            {
                Nome = "Teste3",
                Descricao = "Teste3",
                UsuarioCriacao = 1,
                UsuarioEdicao = 1,
                DataCriacao = DateTime.Now,
                DateAlteracao = DateTime.Now,
                Ativo = true.ToString(),
                Status = 1,
                idCliente = 1
            };

            var result = PerfilDAO.Save(Perfil);

        }
        [TestMethod]
        public void GetAllPerfilTest()
        {
            var resut = PerfilDAO.GetAll();
        }
        [TestMethod]
        public void EditPerfilTest()
        {
            var obj = PerfilDAO.GetAll().Find(x => x.ID == 1);
            obj.Nome = "Teste3";
            obj.Descricao = "Teste3";
            var result = PerfilDAO.Save(obj);

        }
        [TestMethod]
        public void RemovePerfilTest()
        {
            var obj = PerfilDAO.GetAll().Find(x => x.ID == 1);
            var result = PerfilDAO.Remove(obj);

        }
    }
}
