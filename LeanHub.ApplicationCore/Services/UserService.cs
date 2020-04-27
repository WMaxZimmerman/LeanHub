using LeanHub.DAL.Repositories;

namespace LeanHub.ApplicationCore.Services
{
    public interface IUserService
    {
      
        string AddUserToOrg(string name);

        string RemoveUserFromOrg(string name);
    }
    public class UserService: IUserService
    {
        private IGitHubRepository _repo;
        private IGitHubUserService _gitHubService;

        public UserService(IGitHubRepository repo = null, IGitHubUserService gitHubUserService = null)
        {
            _repo = repo ?? new GitHubRepository();

            _gitHubService = gitHubUserService ?? new GitHubUserService();
        }        
   
        public string AddUserToOrg(string name)
        {
            var auth = _gitHubService.GetAdminCredentials("username:password");
            return _repo.AddUserToOrg(name, auth);
        }

        public string RemoveUserFromOrg(string name)
        {
            var auth = _gitHubService.GetAdminCredentials("username:password");
            return _repo.RemoveUserFromOrg(name, auth);
        }
    }
}
