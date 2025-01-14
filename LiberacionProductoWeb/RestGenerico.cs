using Newtonsoft.Json;
using RestSharp;

namespace LiberacionProductoWeb
{
    public class RestGenerico
    {
        public IRestResponse getApi<T, R>(string endPoint)
        {
            var client = new RestClient(endPoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse postApi<T, R>(string endPoint, T entidad)
        {
            var client = new RestClient(endPoint);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(entidad);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
