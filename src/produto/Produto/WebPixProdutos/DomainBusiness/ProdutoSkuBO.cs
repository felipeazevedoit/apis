using Entity;
using wpServices;
using System.Threading.Tasks;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DomainBusiness
{
    public static class ProdutoSkuBO
    {
        /// <summary>
        /// Metodo de salvar ProdutoSku (Async)
        /// </summary>
        /// <param name="ProdutoSku"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o ProdutoSku / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(ProdutoSku ProdutoSku, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (ProdutoSku.idCliente != 0)
                    try { return ProdutoSkuRep.Save(ProdutoSku); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os ProdutoSkus por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<ProdutoSku>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return ProdutoSkuRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<ProdutoSku>();
        }

        /// <summary>
        /// Metodo de deletar ProdutoSku
        /// </summary>
        /// <param name="ProdutoSku">ProdutoSku que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o ProdutoSku / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object ProdutoSku, string token)
        {
            dynamic objEn = ProdutoSku;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                ProdutoSku obj = ProdutoSkuRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return ProdutoSkuRep.Remove(obj);
            }
            else
                return false;
        }
    }
}
