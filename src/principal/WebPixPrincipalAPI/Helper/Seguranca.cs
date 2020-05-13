using Newtonsoft.Json;
using RestSharp;
using RestSharpEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPixPrincipalRepository;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalAPI.Helper
{
    public class Seguranca
    {
        public static async Task<bool> validaTokenAsync(string token)
        {
            var list = ConfiguracaoDAO.GetAll().Where(x => x.Chave == "URLSeguranca").FirstOrDefault();
            RestClient client = new RestClient(list.Valor);
            var url = "/api/token/ValidaToken/" + token;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync2(request);

            return Convert.ToBoolean(response.Content);
        }

        public static async Task<IEnumerable<UsuarioXPerfil>> GetUsuariosAsync(int idPerfil)
        {

            var list = ConfiguracaoDAO.GetAll().Where(x => x.Chave == "URLSeguranca").FirstOrDefault();
            RestClient client = new RestClient(list.Valor);
            var url = "/api/Perfil/GetUsersIdsByPerfil/" + idPerfil;
            RestRequest request = null;
            request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteTaskAsync2(request);

            return JsonConvert.DeserializeObject<IEnumerable<UsuarioXPerfil>>(response.Content);
        }
    }
}
