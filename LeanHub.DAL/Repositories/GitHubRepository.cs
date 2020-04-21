using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;


namespace LeanHub.DAL.Repositories
{
    public class GitHubRepository
    {
        public static string HelloWorld()
        {
            return "Hello World";
        }

        public static string AddUserToOrg(string name)
        {
            using(var client = new HttpClient()) 
            {
                var byteArray = Encoding.ASCII.GetBytes("austinsuiter:th3056cr3w");
                var address = "https://api.github.com/orgs/TestyMcTestOrg/memberships/"+name;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("User-Agent", "LeanHub");

                var response = client.PutAsync(address, null).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                return content;
            }
         
        }

        public static string RemoveUserFromOrg(string name)
        {
            using(var client = new HttpClient()) 
            {
                var byteArray = Encoding.ASCII.GetBytes("UserName:Password");
                var address = "https://api.github.com/orgs/TestyMcTestOrg/memberships/"+name;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("User-Agent", "LeanHub");

                var response = client.DeleteAsync(address).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                return content;
            }
         
        }
    }
}
