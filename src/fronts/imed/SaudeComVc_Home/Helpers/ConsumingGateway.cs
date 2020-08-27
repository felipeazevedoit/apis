using SaudeComVc_Home.Models.Novo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SaudeComVc_Home.Helpers
{
    public class ConsumingGateway
    {
        public static T Call<T>(string modulo, string controller, string actionName, dynamic model) where T: class
        {
            try
            {
                var keyUrl = ConfigurationManager.AppSettings["UrlDomainGateway"].ToString();
                var pathUrl = ConfigurationManager.AppSettings["UrlRequestGateway"].ToString();

                var request = new SaudeComVc_Home.Models.RequestModel { Modulo = modulo, ControllerName = controller, ActionName = actionName, Model = Newtonsoft.Json.JsonConvert.SerializeObject(model) };

                var consumingApi = new ConsumingApiRest(keyUrl, string.Empty);
                var ret = consumingApi.Execute<RequestResponse<T>>(pathUrl, request, RestSharp.Method.POST, RestSharp.ParameterType.RequestBody);

                if (ret.Success)
                    return ret.Data;

                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }
    }
}