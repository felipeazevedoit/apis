using wpEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wpService;
using wpRepository;
using wpService.Models;

namespace DomainBusiness
{
    public static class ValorBO
    {
        /// <summary>
        /// Metodo de salvar Valor (Async)
        /// </summary>
        /// <param name="Valor"> Objeto Produtp</param>
        /// <param name="token"> Token valido</param>
        /// <returns>Verdadeiro: Salvou o Valor / Falso: Houve falha</returns>
        public static async Task<bool> SaveAsync(Valor Valor, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
            {
                if (Valor.idCliente != 0)
                    try { return ValorRep.Save(Valor); } catch { return false; }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo de retornar todos os Valors por cliente
        /// </summary>
        /// <param name="idCliente">ID do cliente solicitante</param>
        /// <param name="token">Token Valido</param>
        /// <returns></returns>
        public static async Task<List<Valor>> GetAllAsync(int idCliente, string token)
        {
            if (await SeguracaServ.validaTokenAsync(token))
                return ValorRep.GetAll().Where(x => x.idCliente == idCliente).ToList();
            else
                return new List<Valor>();
        }

        /// <summary>
        /// Metodo de deletar Valor
        /// </summary>
        /// <param name="Valor">Valor que iraser deletado</param>
        /// <param name="token">Token valido</param>
        /// <returns>Verdadeiro: Removeu o Valor / Falso: Houve falha</returns>
        public static async Task<bool> RemoveAsync(Object Valor, string token)
        {
            dynamic objEn = Valor;
            string a = objEn.ID.ToString();
            if (await SeguracaServ.validaTokenAsync(token))
            {
                Valor obj = ValorRep.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return ValorRep.Remove(obj);
            }
            else
                return false;
        }
        
        /// <summary>
        ///Regras:
        ///Montar Objeto tipo PAC
        ///Consultar principal para colher configurações padores de CLiente
        ///Verificar se o metodo esta automatico ou nem 
        ///Retornar uma "Tuple" com todos os meios dde entregas disponiveis 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Tuple<string, double, string, DateTime>>> CalcutatePACFromServiceAsync(int IDProduto, string CEP, int idCliente, int idUsuario, string token)
        {
            
            if (await SeguracaServ.validaTokenAsync(token))
            {
                //Carrega informações de Configuração de envio
                var config = SeguracaServ.GetConfig(idCliente, idUsuario);
                var produto = SeguracaServ.GetProduto(idCliente, idUsuario, IDProduto);

                //Carrega MODEL
                PropEnvioPAC propEnvio = new PropEnvioPAC
                {
                    nCdEmpresa = config.Where(x => x.Chave == "nCdEmpresaPAC").FirstOrDefault().Valor,
                    nCdFormato = Convert.ToInt32(config.Where(x => x.Chave == "Formato").FirstOrDefault().Valor),
                    nCdServico = "PAC",
                    nVlAltura = produto.Altura,
                    nVlComprimento = produto.Comprimento,
                    nVlDiametro = 0,
                    nVlLargura = produto.Largura,
                    nVlPeso = produto.Peso.ToString(),
                    nVlValorDeclarado = 0,
                    sCdAvisoRecebimento = "false",
                    sCdMaoPropria = "false",
                    sCepDestino = config.Where(x => x.Chave == "CEPDestino").FirstOrDefault().Valor,
                    sCepOrigem = CEP,
                    sDsSenha = config.Where(x => x.Chave == "Senha").FirstOrDefault().Valor
                };

                CorreiosServ correios = new CorreiosServ();


                List<Tuple<string, double, string, DateTime>> envio = correios.GetValorFromPAC(propEnvio);

                return envio;
            }
            else
                return new List<Tuple<string, double, string, DateTime>>();

        }
    }
}
