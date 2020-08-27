using Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using RestSharp;
using RestSharpEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebPixPrincipalRepository;
using WebPixSeguranca.Model;


namespace WebPixSeguranca.Helper.Auxiliares
{
    public class Auxiliares
    {

        public static async Task<MotorAuxViewModel> GetInfoMotorAux(string aux, int idcliente)
        {
            try
            {
                ConfigurationHelper helper = new ConfigurationHelper();
                using (HttpClient client = new HttpClient())
                {
                   
                    client.BaseAddress = new Uri(helper.GetConfiguration("URLIn"));
                    var url = "api/motoraux/acessarmotor/" + aux;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                        var lstData = JsonConvert.DeserializeObject<MotorAuxViewModel>(data.Result.ToString());
                        return lstData;
                    }
                    else
                        return new MotorAuxViewModel();
                }

            }
            catch (Exception ex)
            {
                return new MotorAuxViewModel();
            }
        }

        public static async Task<bool> verificaPermissaoAsync(AcaoViewModel acao, int idusuario, int idCliente)
        {
            var permissao = await PermissaoDAO.CarregarPermissaoByUsuarioAsync(idusuario);
            bool retorno = false;

            if (acao.TipoAcao == 4) //Novo id Para Ações Publicas (corrigir)
                return true;

            string[] acoes = permissao.idTipoAcao.Split(',');
            foreach (string Lacao in acoes)
            {
                if (acao.TipoAcao == int.Parse(Lacao))
                {
                    retorno = true;
                }

            }

            return retorno;

        }

        public static async Task<bool> VerificaUsuarioPermissaoAsync(AcaoViewModel acao, int idusuario, int idCliente)
        {
            if (acao.TipoAcao == 4)
                return true;

            var perfil = await PerfilDAO.CarregaPerfilByUsuario(idusuario, idCliente);
            var permissoesIDs = perfil.idPermissao.Split(',').Select(id => Convert.ToInt32(id));
            var permissoes = await PermissaoDAO.GetByIdsAndMotor(permissoesIDs, acao.idMotorAux);

            var retorno = false;

            foreach (var item in permissoes)
            {
                var tipoAcoes = item.idTipoAcao.Split(',').Select(id => Convert.ToInt32(id));

                if (tipoAcoes.Contains(acao.TipoAcao))
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        public static async Task<object> GetRetornoAuxAsync(MotorAuxViewModel motorAux, AcaoViewModel acoesUsuario, Token token, object conteudo, int idCliente)
        {
            List<ParametroViewModel> listaAcaoes = acoesUsuario.Parametro.OrderBy(x => x.Ordem).ToList();

            RestClient client = new RestClient(motorAux.Url);
            var url = acoesUsuario.Caminho;
            RestRequest request = null;

            if (listaAcaoes.Any(x => x.Tipo == "body"))
            {
                if (conteudo != null)
                {
                    foreach (ParametroViewModel parametro in listaAcaoes)
                    {
                        if (parametro.Tipo == "get")
                            url += "/" + GetPropValue(conteudo, parametro.Nome).ToString();
                    }
                }
                url += "/" + token.GuidSec;
                request = new RestRequest(url, Method.POST);

                try
                {
                    request.AddParameter("application/json", GerareteObjPost(conteudo, listaAcaoes), ParameterType.RequestBody);
                }
                catch (Exception e)
                {

                }

            }
            else
            {

                if (conteudo != null)
                {
                    foreach (ParametroViewModel parametro in listaAcaoes)
                    {

                        if (parametro.Nome == "idCliente")
                            url += "/" + idCliente;
                        else if (parametro.Tipo == "get" && parametro.Nome != "idCliente")
                            url += "/" + GetPropValue(conteudo, parametro.Nome).ToString();


                    }
                }
                else
                {
                    foreach (ParametroViewModel parametro in listaAcaoes)
                    {
                        if (parametro.Tipo == "get" && parametro.Nome == "idCliente")
                            url += "/" + idCliente;
                    }
                }
                url += "/" + token.GuidSec;
                request = new RestRequest(url, Method.GET);
            }


            request.RequestFormat = DataFormat.Json;

            try
            {
                var response = await client.ExecuteTaskAsync(request);
                Object deserializedObject = null;
                JsonSerializer jss = new JsonSerializer();

                deserializedObject = JsonConvert.DeserializeObject<Object>(response.Content);
                return deserializedObject;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static object GerareteObjPost(object conteudo, List<ParametroViewModel> list)
        {
            JObject obj = JObject.Parse(conteudo.ToString());
            Object post = null;
            foreach (ParametroViewModel p in list)
            {
                if (p.Tipo == "get")
                {
                    obj.Remove(p.Nome);
                }
                else
                {
                    post = obj[p.Nome];
                    break;
                }
            }

            return post;


        }
        public static object GetPropValue(object src, string propName)
        {
            JObject obj = JObject.Parse(src.ToString());
            object VA = obj[propName];

            return VA;
        }
        
       
    }
}
