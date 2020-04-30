using System.Collections.Generic;
using System.Linq;
using LeanHub.DAL.Repositories;
using LeanHub.Shared.Models;

namespace LeanHub.ApplicationCore.Services
{
    public interface IUserService
    {
        Member AddUserToOrg(string name);

        bool RemoveUserFromOrg(string name);

        List<User> GetUsers();

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
   
        public Member AddUserToOrg(string name)
        {
            var auth = _repo.GetCredentials();
            return _repo.AddUserToOrg(name, auth);
        }

        public bool RemoveUserFromOrg(string name)
        {
            var auth = _repo.GetCredentials();
            return _repo.RemoveUserFromOrg(name, auth);
        }

        public List<User> GetUsers()
        {
            var auth = _repo.GetCredentials();
            return _repo.GetListOfUsers(auth);
        }

        public List<User> GetLocalUsers()
        {
            return _csvRepo.GetUsers().ToList();
        }
    }
}
