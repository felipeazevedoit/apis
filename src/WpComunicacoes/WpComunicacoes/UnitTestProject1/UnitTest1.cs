using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpComunicacoes.Entidades;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //    [TestMethod]
        //    public async System.Threading.Tasks.Task LoginRestTest()
        //    {
        //        WpComunic.Dominio.ComunicacaoBO comunic = new WpComunic.Dominio.ComunicacaoBO();

        //        List<Propriedades> propriedades = new List<Propriedades>();
        //        Propriedades propriedades1 = new Propriedades();
        //        propriedades1.Nome = "client_id";
        //        propriedades1.tipo = "config";
        //        propriedades1.valor = "";
        //        propriedades1.idCliente = 16;



        //        Propriedades propriedades2 = new Propriedades();
        //        propriedades2.Nome = "password";
        //        propriedades2.tipo = "config";
        //        propriedades2.valor = "";
        //        propriedades2.idCliente = 16;

        //        propriedades.Add(propriedades1);
        //        propriedades.Add(propriedades2);


        //        Entrada entrada = new Entrada
        //        {
        //            Ativo = true,
        //            ID = 0,
        //            DataCriacao = DateTime.Now,
        //            DateAlteracao = DateTime.Now,
        //            Descricao = null,
        //            idCliente = 16,
        //            Nome = null,
        //            propriedades = propriedades,
        //            Status = 1,
        //            UsuarioCriacao = 1,
        //            UsuarioEdicao = 1
        //        };
        //        await comunic.RealizarComunicaoAsync(entrada, 2, "access_token");
        //    }

        //    [TestMethod]
        //    public async System.Threading.Tasks.Task MetodoRestTest()
        //    {
        //        WpComunic.Dominio.ComunicacaoBO comunic = new WpComunic.Dominio.ComunicacaoBO();

        //        List<Propriedades> propriedades = new List<Propriedades>();
        //        Propriedades propriedades1 = new Propriedades();
        //        propriedades1.Nome = "token";
        //        propriedades1.tipo = "token";
        //        propriedades1.valor = "6e46fca9d127fd281ebd2843c8b02787e9ce88e4";
        //        propriedades1.idCliente = 16;

        //        propriedades.Add(propriedades1);


        //        Entrada entrada = new Entrada
        //        {
        //            Ativo = true,
        //            ID = 0,
        //            DataCriacao = DateTime.Now,
        //            DateAlteracao = DateTime.Now,
        //            Descricao = null,
        //            idCliente = 16,
        //            Nome = null,
        //            propriedades = propriedades,
        //            Status = 1,
        //            UsuarioCriacao = 1,
        //            UsuarioEdicao = 1
        //        };
        //         await comunic.RealizarComunicaoAsync(entrada, 2, "occupation");
        //    }


        [TestMethod]
        public async System.Threading.Tasks.Task OracleMetodoProcedureTest()
        {
            WpComunic.Dominio.ComunicacaoBO comunic = new WpComunic.Dominio.ComunicacaoBO();

            //Carrega motorExterno
            WpComunic.Repositorio.MotorExternoRep motorRep = new WpComunic.Repositorio.MotorExternoRep();

            var motor = motorRep.GetMotorExterno(3);

            var metodo = motor.metodo.Where(x => x.ID == 4).FirstOrDefault();

            List<Propriedades> propriedades = new List<Propriedades>();

            Propriedades prop = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "p_CONSULTA",
                Nome = "Consulta",
                valor = "MARCA_EMPRESARIAL",
                tipo = "string",
                Ordem = 1
            };
            Propriedades prop2 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "p_ID_MRC",
                Nome = "idMarca",
                valor = "0",
                tipo = "int",
                Ordem = 2

            };
            Propriedades prop3 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "p_ID_EMP_GCOM",
                Nome = "idEmpresa",
                valor = "0",
                tipo = "int",
                Ordem = 3

            };
            Propriedades prop4 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "p_ID_ETB_GCOM",
                Nome = "id_ETB",
                valor = "0",
                tipo = "int",
                Ordem = 4

            };
            Propriedades prop5 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "DC_MRC",
                Nome = "DC_MRC",
                valor = "",
                tipo = "string",
                Ordem = 5

            };
            Propriedades prop6 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "outCursor",
                Nome = "outCursor",
                valor = "",
                tipo = "cursor",
                Ordem = 6

            };
            Propriedades prop7 = new Propriedades
            {
                idCliente = 16,
                NomeExterno = "P_ID_SHOP",
                Nome = "P_ID_SHOP",
                valor = "0",
                tipo = "int",
                Ordem = 7

            };

            propriedades.Add(prop);
            propriedades.Add(prop2);
            propriedades.Add(prop3);
            propriedades.Add(prop4);
            propriedades.Add(prop5);
            propriedades.Add(prop6);
            propriedades.Add(prop7);
            var Lista = propriedades.OrderBy(x => x.Ordem).ToList();
            metodo.ClasseEntrada.propriedades = Lista;

            motor.metodo = new List<Metodo>();
            motor.metodo.Add(metodo);

            WpComunic.Dominio.ComunicacaoBO comunicacao = new WpComunic.Dominio.ComunicacaoBO();
            var ret = comunicacao.RealizaComunicacao(motor);

        }
    }
}
