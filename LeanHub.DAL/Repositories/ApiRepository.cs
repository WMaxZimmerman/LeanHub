using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LeanHub.DAL.Repositories
{
    public interface IApiRepository
    {
        T MakeApiCall<T>(string address, string method, AuthenticationHeaderValue authorization);
    }

   public class ApiRepository: IApiRepository
   {
        public T MakeApiCall<T>(string address, string method, AuthenticationHeaderValue authorization)
        {
            using(var client = new HttpClient()) 
            {
                client.DefaultRequestHeaders.Authorization = authorization;
                client.DefaultRequestHeaders.Add("User-Agent", "LeanHub");

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(address);
                request.Method = new HttpMethod(method);
                var response = client.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(content);

                return result;
            }
        } 
    }
}
