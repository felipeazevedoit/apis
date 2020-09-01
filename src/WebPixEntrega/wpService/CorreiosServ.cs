using System;
using Correios;
using wpService.Models;
using System.Collections.Generic;

namespace wpService
{
    public class CorreiosServ
    {

        /// <summary>
        /// Metodo de calculo de entrega do PAC muito interessante por sinal 
        /// </summary>
        /// <param name="pac">Objeto de tipo PAC </param>
        /// <returns></returns>
        public List<Tuple<string, double,string, DateTime>> GetValorFromPAC(PropEnvioPAC pac)
        {
            //Acessa a WS dos correios
            CalcPrecoPrazoWSSoapClient Apac = new CalcPrecoPrazoWSSoapClient(CalcPrecoPrazoWSSoapClient.EndpointConfiguration.CalcPrecoPrazoWSSoap);


            var resultado = Apac.CalcPrecoPrazoAsync(
              pac.nCdEmpresa,
              pac.sDsSenha,
              pac.nCdServico,
              pac.sCepOrigem,
              pac.sCepDestino,
              pac.nVlPeso,
              pac.nCdFormato,
              pac.nVlComprimento,
              pac.nVlAltura,
              pac.nVlLargura,
              pac.nVlDiametro,
              pac.sCdMaoPropria,
              pac.nVlValorDeclarado,
              pac.sCdAvisoRecebimento);

            List<Tuple<string, double,string, DateTime>> result = new List<Tuple<string, double,string, DateTime>>();

            foreach(cServico res in resultado.Result.Servicos)
            {
                string serv = res.Codigo == 1 ? "PAC" : "Outros";
                
                //Resultado de serviço entrega 
                Tuple<string, double,string, DateTime> retorno = new Tuple<string, double,string, DateTime>(
                    serv, 
                    Convert.ToDouble(res.Valor),
                    res.PrazoEntrega,
                    Convert.ToDateTime(res.DataMaxEntrega));


                result.Add(retorno);


            }


            return result;

        }

    }
}
