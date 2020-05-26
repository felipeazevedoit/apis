using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using SaudeComVoce.Models;

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

        public T Execute<T>(string path, dynamic model, Method method, ParameterType parmType, List<string> parms = null, bool jsonSerialize = true)
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
                        foreach (var _parm in parms)
                        {
                            request.AddParameter(_parm, _model[_parm].Value, "application/json; charset=utf-8", parmType);
                        }

                    }
                    else
                    {
                        if (jsonSerialize)
                        {
                            string parm = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                            request.AddParameter("application/json; charset=utf-8", parm, parmType);
                        }
                        else
                        {
                            request.AddParameter("application/json; charset=utf-8", model, parmType);
                        }
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