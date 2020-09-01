using wpEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wpService;
using wpRepository;

namespace DomainBusiness
{
    public static class PropiedadeBO
    {
        /// <summary>
        /// Metodo de salvar Propiedade (Async)
        /// </summary>
        /// <param name="Propiedade"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o Propiedade / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(Propiedade Propiedade, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (Propiedade.idCliente != 0)
                    try { return PropiedadeRep.Save(Propiedade); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Propiedades por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Propiedade>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return PropiedadeRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Propiedade>();
        }

        /// <summary>
        /// Metodo de deletar Propiedade
        /// </summary>
        /// <param name="Propiedade">Propiedade que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o Propiedade / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object Propiedade, string token)
        {
            dynamic objEn = Propiedade;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Propiedade obj = PropiedadeRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return PropiedadeRep.Remove(obj);
            }
            else
                return false;
        }
    }
}
