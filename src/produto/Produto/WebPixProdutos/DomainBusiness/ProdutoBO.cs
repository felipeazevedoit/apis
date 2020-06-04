using Entity;
using wpServices;
using System.Threading.Tasks;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DomainBusiness
{
    public static class ProdutoBO
    {
        /// <summary>
        /// Metodo de salvar produto (Async)
        /// </summary>
        /// <param name="produto"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o produto / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(Produto produto, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {



                if (produto.idCliente != 0)
                    try { return ProdutoRep.Save(produto); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Produtos por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Produto>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return ProdutoRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Produto>();
        }

        /// <summary>
        /// Metodo de deletar produto
        /// </summary>
        /// <param name="produto">Produto que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o produto / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(object produto, string token)
        {
            dynamic objEn = produto;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Produto obj = ProdutoRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return ProdutoRep.Remove(obj);
            }
            else
                return false;
        }
    }
}




