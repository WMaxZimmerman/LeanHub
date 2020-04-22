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
    public class GitHubUser
    {
        public static AuthenticationHeaderValue GetAdminCredentials(string credentials)
        {
            var byteArray = Encoding.ASCII.GetBytes(credentials);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
        public static string GetApiAddress(string userName)
        {
            var address = "https://api.github.com/orgs/TestyMcTestOrg/memberships/"+userName;
            return address;
        }

    }
    public class GitHubRepository
    {
        public static string HelloWorld()
        {
            return "Hello World";
        }

        public static string AddUserToOrg(string name)
        {
            return MakeApiCall(name, "PUT");
        }

        public static string RemoveUserFromOrg(string name)
        {
            return MakeApiCall(name, "DELETE");
        }

        public static string GetListOfUsers()
        {
            return MakeApiCall(null, "GET");
        }

        public static string MakeApiCall(string name, string method)
        {
            using(var client = new HttpClient()) 
            {
                var address = GitHubUser.GetApiAddress(name);
                client.DefaultRequestHeaders.Authorization = GitHubUser.GetAdminCredentials("UserName:Password");
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
