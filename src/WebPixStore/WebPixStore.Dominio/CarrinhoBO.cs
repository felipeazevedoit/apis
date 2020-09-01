using Entity;
using Repository;
using Serivce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPixStore.Dominio
{

    public class CarrinhoBO
    {
        #region Propiedades

        private Dictionary<string, string> motores;

        public Dictionary<string, string> Motores
        {
            get { return GetMotores(); }
        }

        private Dictionary<string, string> dicionario;

        public Dictionary<string, string> Dicionario
        {
            get { return GetDicionario(); }
        }

        #endregion


        /// <summary>
        /// Inicia o Processo do carrinho 
        /// </summary>
        /// <param name="carrinho">Carrinho para inicio</param>
        /// <returns></returns>
        public Carrinho IniciarCarrinho(Carrinho carrinho)
        {
            carrinho.dataCriacao = DateTime.Now;
            carrinho.dataEdicao = DateTime.Now;
            List<Propriedades> Propos = LoadPropiedadesCliente(carrinho.idCliente);
            carrinho.Propiedades = Propos;
            CarrinhoRep rep = new CarrinhoRep();
            var teste = rep.Add(carrinho);
            return teste as Carrinho;

        }

        private List<Propriedades> LoadPropiedadesCliente(int idcliente)
        {
            PropriedadesRep propriedades = new PropriedadesRep();
            ExpressaoRep expressao = new ExpressaoRep();
            List<Propriedades> ret = new List<Propriedades>();
            var pro = propriedades.GetList(x => x.idCliente == idcliente);
            foreach(Propriedades proUni in pro)
            {
                proUni.Expressao = expressao.GetList(x => x.PropriedadesID == proUni.ID).ToList();
                ret.Add(proUni);
            }
            
            return ret;
        }

        /// <summary>
        /// Adiciona um produto no carrinho
        /// </summary>
        /// <param name="idProduto">produto que sera adcionado </param>
        /// <param name="carrinho">carrinho pra onde vai o produto</param>
        /// <returns></returns>
        public async Task<Carrinho> AddProdutoAsync(int idProduto, Carrinho carrinho)
        {

            //Verificar Dipobilidade do produto
            Produto Produto = await ProdutoServ.LoadProduto(carrinho.idCliente, carrinho.UsuarioCriacao, idProduto);
            if (Produto.Estoque > Produto.EstoqueMinimo)
            {
                carrinho.Produtos.Add(Produto);
                carrinho.dataEdicao = DateTime.Now;
                carrinho.Menssagens.Add("Produto Adicionado com sucesso");
                carrinho = await CalculaValorAsync(carrinho);
            }
            else
            {
                carrinho.Menssagens.Add("Produto sem estoque");
            }
            return carrinho;
        }
        /// <summary>
        /// Calcula o Valor Total do Carrinho
        /// </summary>
        /// <param name="carrinho"></param>
        /// <returns></returns>
        private async Task<Carrinho> CalculaValorAsync(Carrinho carrinho)
        {
            //Calcula Sub Total
            List<Produto> produtos = new List<Produto>();
            produtos = await ProdutoServ.LoadProdutos(carrinho.idCliente, 999);
            carrinho.SubTotal = 0;
            //foreach (int id in carrinho.Produtos)
            //{
            //    carrinho.SubTotal += produtos.Where(x => x.ID == id).FirstOrDefault().Preco;
            //    carrinho.Total = carrinho.SubTotal;
            //}

            //Calcula Fotores externos
            //foreach (Expressao id in carrinho.Propiedades.Expressao)
            //{
            //    var ValorPropiedade = CalculaValorPropiedades(carrinho);
            //    carrinho.Total += (Decimal)ValorPropiedade;
            //}

            throw new NotImplementedException();
        }
        /// <summary>
        /// Calcula apenas as propiedade do carrinho (baseando as propiedades)
        /// </summary>
        /// <returns></returns>
        private object CalculaValorPropiedades(Carrinho carrinho)
        {
            //foreach(Expressao prop in carrinho.Propiedades.Expressao)
            //{
            //    Decimal valorFinal = 0;
            //    string expressao = prop.ExpressaoStr;


            //    Propriedades props = new Propriedades()
            //    {
            //        Ativo = true,
            //        dataCriacao = DateTime.Now,
            //        dataEdicao = DateTime.Now,
            //        Descricao = "Calcula valores de Cupons de acrecimo",
            //        //Expressao = new Expressao()
            //        //{
            //        //    ExpressaoStr = expressao,
            //        //    Motor = "WPCupom",
            //        //    Nome = "Cupom de Acrecimo",
            //        //    Descricao = "Este cupom adiciona 10% Do valor total valor do garcon",
            //        //}
            //    };


            //    //Quebra Expreção em maximo de extencoes
            //    string[] pontoExp = expressao.Split(";"); ;//props.Expressao.ExpressaoStr.Split(";");
            //    List<Decimal> result = new List<decimal>();
            //    for (int x = 0; x < pontoExp.Count(); x++)
            //    {
            //        Dictionary<object, int> contas = new Dictionary<object, int>();

            //        string[] termos = pontoExp[x].Split("|");
            //        for (int y = 0; y < termos.Count(); y++)
            //        {
            //            //Quebra de regras
            //            if (termos[y] == "[subTotal]")
            //            {
            //                Decimal subTotal = carrinho.SubTotal;
            //                contas.Add(subTotal, y);
            //                continue;
            //            }
            //            if (termos[y] == "[total]")
            //            {
            //                Decimal subTotal = carrinho.Total;
            //                contas.Add(subTotal, y);
            //                continue;
            //            }
            //            if (termos[y] == "%Valor%")
            //            {
            //                Decimal valor = 10;
            //                contas.Add(valor, y);
            //                continue;
            //            }
            //            if (termos[y] == "[Percent]")
            //            {
            //                Decimal percent = 100;
            //                contas.Add(percent, y);
            //                continue;
            //            }
            //            if (termos[y] == "[negative]")
            //            {
            //                Decimal negative = -1;
            //                contas.Add(negative, y);
            //                continue;
            //            }
            //            if (termos[y] == "[Dolar]")
            //            {
            //                Decimal dolar = (Decimal)3.74;
            //                contas.Add(dolar, y);
            //                continue;
            //            }


            //            //Separa operadores
            //            if (termos[y] == "+")
            //            {
            //                contas.Add("+", y);
            //                continue;
            //            }
            //            if (termos[y] == "*")
            //            {
            //                contas.Add("*", y);
            //                continue;
            //            }
            //            if (termos[y] == "-")
            //            {
            //                contas.Add("-", y);
            //                continue;
            //            }
            //            if (termos[y] == "/")
            //            {
            //                contas.Add("/", y);
            //                continue;
            //            }

            //            //Conta os results
            //            if (termos[y] == "[result*]")
            //            {
            //                contas.Add(result[0], y);
            //                continue;
            //            }
            //            if (termos[y] == "[result**]")
            //            {
            //                contas.Add(result[1], y);
            //                continue;
            //            }
            //            if (termos[y] == "[result***]")
            //            {
            //                contas.Add(result[2], y);
            //                continue;
            //            }

            //        }


            //        Decimal valor1 = 0;
            //        Decimal valor2 = 0;
            //        string operador = "";

            //        foreach (KeyValuePair<object, int> entry in contas)
            //        {
            //            //Isso é chumbado por que ?
            //            //A resposta é simples toda conta matemtica simples requer
            //            //Uma entrada um operador a segunda entreda e por fim o resultado
            //            //Por ex 1 + 1 = 2
            //            //Logo:
            //            //entrada1: 1 
            //            //operador: +
            //            //entrada2: 1 
            //            //resultado: 2
            //            //Logo montamos a conta desta forma :)

            //            if (entry.Value == 0)
            //            {
            //                valor1 = (Decimal)entry.Key;
            //                continue;
            //            }
            //            if (entry.Value == 1)
            //            {
            //                operador = (String)entry.Key;
            //                continue;
            //            }
            //            if (entry.Value == 2)
            //            {
            //                valor2 = (Decimal)entry.Key;
            //                continue;
            //            }

            //        }

            //        //Calcula por operadores :)
            //        if (operador == "+")
            //        {
            //            var res = valor1 + valor2;
            //            result.Add(res);
            //            continue;
            //        }
            //        if (operador == "-")
            //        {
            //            var res = valor1 - valor2;
            //            result.Add(res);
            //            continue;
            //        }

            //        if (operador == "*")
            //        {
            //            var res = valor1 * valor2;
            //            result.Add(res);
            //            continue;
            //        }
            //        if (operador == "/")
            //        {
            //            var res = valor1 / valor2;
            //            result.Add(res);
            //            continue;
            //        }
            //    }

            //    valorFinal = result.Last();
            //    return valorFinal;
            //}
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GetMotores()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GetDicionario()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("SubTotal", "subTotal");
            dic.Add("Total", "[Total]");
            dic.Add("Produto", "prod");


            return dic;
        }
    }
}
