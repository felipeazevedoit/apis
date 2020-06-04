using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wpServices;

namespace DomainBusiness
{
    public static class PrecoBO
    {
        /// <summary>
        /// Metodo de salvar Preco (Async)
        /// </summary>
        /// <param name="Preco"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o Preco / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(Preco Preco, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (Preco.idCliente != 0)
                    try { return PrecoRep.Save(Preco); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Precos por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Preco>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return PrecoRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Preco>();
        }

        /// <summary>
        /// Metodo de deletar Preco
        /// </summary>
        /// <param name="Preco">Preco que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o Preco / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object Preco, string token)
        {
            dynamic objEn = Preco;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Preco obj = PrecoRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return PrecoRep.Remove(obj);
            }
            else
                return false;
        }
    }
}
