using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace LeanHub.DAL.Repositories
{
    public interface IGitHubApi
    {
        string MakeApiCall(string address, string method, AuthenticationHeaderValue authorization);
    }

   public class GitHubApi: IGitHubApi
   {
        public string MakeApiCall(string address, string method, AuthenticationHeaderValue authorization)
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

                return content;
            }
        } 
    }
}
