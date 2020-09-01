using wpEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wpService;
using wpRepository;

namespace DomainBusiness
{
    public static class TransportadoraBO
    {
        /// <summary>
        /// Metodo de salvar Transportadora (Async)
        /// </summary>
        /// <param name="Transportadora"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o Transportadora / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(Transportadora Transportadora, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (Transportadora.idCliente != 0)
                    try { return TransportadoraRep.Save(Transportadora); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Transportadoras por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Transportadora>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return TransportadoraRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Transportadora>();
        }

        /// <summary>
        /// Metodo de deletar Transportadora
        /// </summary>
        /// <param name="Transportadora">Transportadora que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o Transportadora / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object Transportadora, string token)
        {
            dynamic objEn = Transportadora;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Transportadora obj = TransportadoraRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return TransportadoraRep.Remove(obj);
            }
            else
                return false;
        }
    }
}
