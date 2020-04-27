using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using LeanHub.Shared.Models;

namespace LeanHub.DAL.Repositories
{

    public interface IGitHubRepository
    {
    
        Member AddUserToOrg(string name, AuthenticationHeaderValue authorization);

        bool RemoveUserFromOrg(string name, AuthenticationHeaderValue authorization);

        List<User> GetListOfUsers(AuthenticationHeaderValue authorization);

        AuthenticationHeaderValue GetCredentials(string username, string password);
    }

    public class GitHubRepository: IGitHubRepository
    {
        private IApiRepository _api;
        private string _baseUrl = "https://api.github.com/orgs/TestyMcTestOrg/";

        public GitHubRepository(IGitHubApi githubApi = null, IApiRepository api = null)
        {
            _api = api ?? new ApiRepository();
        }

        public Member AddUserToOrg(string name, AuthenticationHeaderValue authorization)
        {
            var url = _baseUrl + "memberships/" + name;
            return _api.MakeApiCall<Member>(url, "PUT", authorization);
        }

        public bool RemoveUserFromOrg(string name, AuthenticationHeaderValue authorization)
        {
            var url = _baseUrl + "members/" + name;
            var result = _api.MakeApiCall<Object>(url, "DELETE", authorization);
            return result != null;
        }

        public List<User> GetListOfUsers(AuthenticationHeaderValue authorization)
        {
            var url = _baseUrl + "members";
            return _api.MakeApiCall<List<User>>(url, "GET", authorization);
        }

        public AuthenticationHeaderValue GetCredentials(string username, string password)
        {
            var credString = $"{username}:{password}";
            var byteArray = Encoding.ASCII.GetBytes(credString);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
