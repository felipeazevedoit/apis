using System;
using System.IO;
using System.Threading.Tasks;
using WpPagamentos.Entidade;
using WpPagamentos.Servico;
using rep = wpPagamentos.Repositorio;
using SelectPdf;
using System.Net;
using Aspose.Pdf.Facades;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace WpPagamento.Dominio
{
    public class PagamentoBO
    {
        public async Task<bool> GerarPagamentoSimplesErede(Loja loja)
        {
            if (loja.propiedades.Recalculo == true)
            {
                //Logica de recalculo
            }
            else if (loja.propiedades.Meio == true)
            {
                //Logica com middle

            }
            else
            {
                try
                {
                    EredeServ2 rede = new EredeServ2();
                    //loja.propiedades.Valor = (double)(loja.propiedades.Valor * 0.01) + loja.propiedades.Valor;
                    string ret = await rede.CreditAsync(loja);
                    if (ret != "")
                    {
                        try
                        {
                            loja.propiedades.tidErede = ret;
                            loja.propiedades.dataCriacao = DateTime.Now;
                            loja.propiedades.dataEdicao = DateTime.Now;
                            loja.meioPagamento.dataCriacao = DateTime.Now;
                            loja.meioPagamento.dataEdicao = DateTime.Now;
                            loja.dataCriacao = DateTime.Now;
                            loja.dataEdicao = DateTime.Now;
                            rep.Propriedades PropriRep = new rep.Propriedades();
                            var id = PropriRep.Add(loja.propiedades);
                            rep.Loja repo = new rep.Loja();
                            repo.Add(loja);
                            return true;
                        }
                        catch (Exception e)
                        {
                            //Colocar log aqui 
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    //Colocar log aqui 
                    return false;
                }
            }
            //Colocar log aqui 
            return false;
        }
        public string GerarBoleto(Loja loja)
        {
            if (loja.propiedades.Recalculo == true)
            {
                //Logica de recalculo
            }
            else if (loja.propiedades.Meio == true)
            {
                //Logica com middle

            }
            else
            {
                try
                {
                    string html = "";
                    List<string> CaminhoPdf = new List<string>();
                   

                    string valorPedido = Math.Round((loja.propiedades.Valor / loja.propiedades.Parcela), 2).ToString().Replace(".", ",");
                    string cliente = GetPropValue(loja.meioPagamento.Configuracao, "Nome").ToString();
                    string endereco1 = GetPropValue(loja.meioPagamento.Configuracao, "endereco1").ToString();
                    string endereco2 = GetPropValue(loja.meioPagamento.Configuracao, "endereco2").ToString();
                    string StrdataVencimento = GetPropValue(loja.meioPagamento.Configuracao, "dataVencimento").ToString();
                    DateTime dtVencimento = Convert.ToDateTime(StrdataVencimento);
                    List<int> cards = new List<int>();
                    DateTime[] vencimento = new DateTime[loja.propiedades.Parcela];
                    string dtVencimentoAPI = "";
                    for (int x = 0; x <= loja.propiedades.Parcela - 1; x++)
                    {
                        
                        if (x == 0)
                        {
                            dtVencimentoAPI = dtVencimento.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            dtVencimento = Convert.ToDateTime(dtVencimentoAPI);
                            dtVencimentoAPI = dtVencimento.AddDays(30).ToString("dd/MM/yyyy");

                        }
                        HtmlToPdf pdf = new HtmlToPdf();
                        PdfDocument document = pdf.ConvertUrl("http://149.56.67.0:8012/bol/boleto_itau.php?valorCobrado=" + valorPedido + @"&cliente=" + cliente + "&endereco1=" + endereco1 + "&endereco2=" + endereco2 + "&datavencimento=" + dtVencimentoAPI + "&pedido=" + loja.idPedido);
                        document.Save(@"C:\boletos_Pagamentos\" + loja.idPedido + "_" + x + ".pdf");
                        CaminhoPdf.Add(@"C:\boletos_Pagamentos\" + loja.idPedido + "_" + x + ".pdf");
                    }


                    PdfFileEditor pdfEditor = new PdfFileEditor();

                    pdfEditor.Concatenate(CaminhoPdf.ToArray(), @"C:\boletos_Pagamentos\" + loja.idPedido + ".pdf");

                    for (int x = 0; x <= CaminhoPdf.Count; x++)
                    {
                        if (File.Exists(CaminhoPdf[x]))
                        {
                            File.Delete(CaminhoPdf[x]);
                        }
                    }



                }

                catch (Exception e)
                {
                    //Colocar log aqui 
                    return "Houve uma falha ao gerar o boleto";
                }
            }
            //Colocar log aqui 
            return "Houve falha ao gerar boleto";
        }
        public object GetPropValue(object src, string propName)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(src);

            JObject obj = JObject.Parse(json);
            object VA = obj[propName];

            return VA;
        }
    }
}
