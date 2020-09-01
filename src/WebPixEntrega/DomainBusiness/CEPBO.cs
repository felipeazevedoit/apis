using wpEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wpService;
using wpRepository;

namespace DomainBusiness
{
    public static class CEPBO
    {
        /// <summary>
        /// Metodo de salvar CEP (Async)
        /// </summary>
        /// <param name="CEP"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o CEP / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(CEP CEP, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (CEP.idCliente != 0)
                    try { return CEPRep.Save(CEP); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os CEPs por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<CEP>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return CEPRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<CEP>();
        }

        /// <summary>
        /// Metodo de deletar CEP
        /// </summary>
        /// <param name="CEP">CEP que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o CEP / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object CEP, string token)
        {
            dynamic objEn = CEP;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                CEP obj = CEPRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return CEPRep.Remove(obj);
            }
            else
                return false;
        }
    }
}
