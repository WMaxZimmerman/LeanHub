using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;


namespace LeanHub.ApplicationCore.Services
{
    public interface IGitHubUserService
    {
        AuthenticationHeaderValue GetAdminCredentials(string credentials);
        string GetApiAddress(string userName);
    }

    public class GitHubUserService: IGitHubUserService
    {
        private IGitHubUserService _gitHubService;
        
        public AuthenticationHeaderValue GetAdminCredentials(string credentials)
        {
            var byteArray = Encoding.ASCII.GetBytes(credentials);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
        public string GetApiAddress(string userName)
        {
            var address = "https://api.github.com/orgs/TestyMcTestOrg/memberships/"+userName;
            return address;
        }
        public GitHubUserService(IGitHubUserService gitHubService = null)
        {
            _gitHubService = gitHubService ?? new GitHubUserService();
        }
    }
}