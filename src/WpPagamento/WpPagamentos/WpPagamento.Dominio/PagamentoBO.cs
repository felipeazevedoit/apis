using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpPagamentos.Entidade;
using WpPagamentos.Servico;

namespace WpPagamento.Dominio
{
    public class PagamentoBO
    {
        public async Task<string> GerarPagamentoSimplesErede(Loja loja)
        {
            if(loja.propiedades.Recalculo == true)
            {
                //Logica de recalculo
            }
            else if(loja.propiedades.Meio == true)
            {
                //Logica com middle

            }
            else
            {
                EredeServ2 rede = new EredeServ2();
                await rede.CreditAsync(loja);
            }
            return "deu certo";
        }
    }
}
