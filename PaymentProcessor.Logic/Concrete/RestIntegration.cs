using Newtonsoft.Json;
using PaymentProcessor.Logic.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PaymentProcessor.Logic.Concrete
{
   public  class RestIntegration: IRestIntegration
    {
        public T UrlPost<T>(string url, object theObject)
        {
            Trace.TraceInformation($"Inside method URLPOST; url ={url}; request{theObject}");
            string MySerializedObject = string.Empty;
            try
            {
                MySerializedObject = JsonConvert.SerializeObject(theObject);
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.Parameters.Clear();
                request.AddHeader("content-type", "application/json");
                 request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", MySerializedObject, ParameterType.RequestBody);
              
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var GenericObject = JsonConvert.DeserializeObject<T>(response.Content);                   
                    return GenericObject;
                }
                Trace.TraceInformation($"http response not successful {response.Content}; throwing this exception ");
                throw new Exception(response.Content);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"An Error Occured on Post Request {ex.Message} \n {ex.StackTrace} ");
                return default(T);
            }
        }
    }
}
