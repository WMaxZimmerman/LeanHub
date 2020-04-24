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
   
    public interface IGitHubRepository
    {
    
        string AddUserToOrg(string name, AuthenticationHeaderValue authorization);

        string RemoveUserFromOrg(string name, AuthenticationHeaderValue authorization);

        string GetListOfUsers(AuthenticationHeaderValue authorization);

    }

    public class GitHubRepository: IGitHubRepository
    {
        private IGitHubApi _githubApi;

        public GitHubRepository(IGitHubApi githubApi = null)
        {
            _githubApi = githubApi ?? new GitHubApi();
        }
      

        public string AddUserToOrg(string name, AuthenticationHeaderValue authorization)
        {
            return _githubApi.MakeApiCall(name, "PUT", authorization);
        }

        public string RemoveUserFromOrg(string name, AuthenticationHeaderValue authorization)
        {
            return _githubApi.MakeApiCall(name, "DELETE", authorization);
        }

        public string GetListOfUsers(AuthenticationHeaderValue authorization)
        {
            return _githubApi.MakeApiCall(null, "GET", authorization);
        }        
    }
}
