using System.Collections.Generic;
using System.Linq;
using LeanHub.DAL.Repositories;
using LeanHub.Shared.Models;

namespace LeanHub.ApplicationCore.Services
{
    public interface IUserService
    {
        Member AddUserToOrg(string name, string username, string password);

        bool RemoveUserFromOrg(string name, string username, string password);

        List<User> GetUsers(string username, string password);

        List<User> GetLocalUsers();
    }

    public class UserService: IUserService
    {
        private IGitHubRepository _repo;
        private ICsvRepository _csvRepo;

        public UserService(IGitHubRepository repo = null, ICsvRepository csvRepo = null)
        {
            _repo = repo ?? new GitHubRepository();
            _csvRepo = csvRepo ?? new CsvRepository();
        }        
   
        public Member AddUserToOrg(string name, string username, string password)
        {
            var auth = _repo.GetCredentials(username, password);
            return _repo.AddUserToOrg(name, auth);
        }

        public bool RemoveUserFromOrg(string name, string username, string password)
        {
            var auth = _repo.GetCredentials(username, password);
            return _repo.RemoveUserFromOrg(name, auth);
        }

        public List<User> GetUsers(string username, string password)
        {
            var auth = _repo.GetCredentials(username, password);
            return _repo.GetListOfUsers(auth);
        }

        public List<User> GetLocalUsers()
        {
            return _csvRepo.GetUsers().ToList();
        }
    }
}
