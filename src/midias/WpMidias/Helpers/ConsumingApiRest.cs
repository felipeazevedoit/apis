using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;

namespace SaudeComVc_Home.Helpers
{
    public class ConsumingApiRest
    {
        private RestClient client;
        public ConsumingApiRest(string urlBase, string token)
        {
            client = new RestClient(urlBase);
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.AddDefaultParameter("Authorization", string.Format("Bearer {0}", token), ParameterType.HttpHeader);
            }
        }

        public T Execute<T>(string path, dynamic model, Method method, ParameterType parmType)
        {
            try
            {
                var request = new RestRequest(path, method);
                request.RequestFormat = DataFormat.Json;
                if (model != null)
                {
                    if (parmType == ParameterType.QueryString || parmType == ParameterType.UrlSegment)
                    {
                        var _model = Newtonsoft.Json.JsonConvert.DeserializeObject(model);
                    

                    }
                    else
                    {
                       
                            request.AddParameter("application/json; charset=utf-8", model, parmType);
                       
                    }
                }

                IRestResponse<dynamic> ret = client.Execute<dynamic>(request);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ret.Content);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}