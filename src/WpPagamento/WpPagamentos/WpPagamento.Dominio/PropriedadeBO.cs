using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpPagamentos.Entidade;
using WpPagamentos.Servico;
using Rep = wpPagamentos.Repositorio;

namespace WpPagamento.Dominio
{
    public static class PropriedadesBO
    {
        /// <summary>
        /// Metodo de salvar Propriedades (Async)
        /// </summary>
        /// <param name="Propriedades"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o Propriedades / Falso: Houve falha</returns>
        public static async Task<object> SaveAsync(Propriedades Propriedades, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Rep.Propriedades rep = new Rep.Propriedades();
                if (Propriedades.idCliente != 0)
                    try { return rep.Add(Propriedades); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Propriedadess por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Propriedades>> GetAllAsync(int idCliente, string token)
        {
            Rep.Propriedades PropriedadesRep = new Rep.Propriedades();

            if (await SeguracaServ.validaTokenAsync(token))
                return PropriedadesRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Propriedades>();
        }

        /// <summary>
        /// Metodo de deletar Propriedades
        /// </summary>
        /// <param name="Propriedades">Propriedades que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o Propriedades / Falso: Houve falha</returns>
        public static async Task<object> RemoveAsync(Object Propriedades, string token)
        {
            dynamic objEn = Propriedades;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Rep.Propriedades PropriedadesRep = new Rep.Propriedades();

                Propriedades obj = PropriedadesRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return true;
            }
            else
                return false;
        }
    }
}
