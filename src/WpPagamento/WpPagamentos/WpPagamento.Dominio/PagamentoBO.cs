using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpPagamentos.Entidade;
using WpPagamentos.Servico;
using rep = wpPagamentos.Repositorio;

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
                try
                {
                    EredeServ2 rede = new EredeServ2();
                    string ret = await rede.CreditAsync(loja);
                    if (ret != "")
                    {
                        try
                        {
                            loja.propiedades.tidErede = ret;
                            rep.Loja repo = new rep.Loja();
                            repo.Add(loja);
                        }
                        catch (Exception e)
                        {
                            return "Houve falha ao salvar o pagamento";
                        }
                    }
                }
                catch (Exception e)
                {
                    return "Houve falha na captura";
                }
            }
            return "deu certo";
        }
    }
}
