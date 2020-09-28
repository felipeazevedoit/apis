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
    public static class MeioPagamentoBO
    {
        /// <summary>
        /// Metodo de salvar MeioPagamento (Async)
        /// </summary>
        /// <param name="MeioPagamento"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o MeioPagamento / Falso: Houve falha</returns>
        public static async Task<object> SaveAsync(MeioPagamento MeioPagamento, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Rep.MeioDePagamento rep = new Rep.MeioDePagamento();
                if (MeioPagamento.idCliente != 0)
                    try { return rep.Add(MeioPagamento); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os MeioPagamentos por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<MeioPagamento>> GetAllAsync(int idCliente, string token)
        {
            Rep.MeioDePagamento MeioPagamentoRep = new Rep.MeioDePagamento(); 

            if (await SeguracaServ.validaTokenAsync(token))
                return MeioPagamentoRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<MeioPagamento>();
        }

        /// <summary>
        /// Metodo de deletar MeioPagamento
        /// </summary>
        /// <param name="MeioPagamento">MeioPagamento que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o MeioPagamento / Falso: Houve falha</returns>
        public static async Task<object> RemoveAsync(Object MeioPagamento, string token)
        {
            dynamic objEn = MeioPagamento;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Rep.MeioDePagamento MeioPagamentoRep = new Rep.MeioDePagamento();

                MeioPagamento obj = MeioPagamentoRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return true;
            }
            else
                return false;
        }
    }
}
