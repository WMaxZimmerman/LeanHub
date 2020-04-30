using System.Collections.Generic;
using System.Linq;
using LeanHub.DAL.Repositories;
using LeanHub.Shared.Models;

namespace LeanHub.ApplicationCore.Services
{
    public interface ICompareService
    {
        IEnumerable<ComparedUser> CompareUsers();
    }

    public class CompareService: ICompareService
    {
        private IGitHubRepository _hubRepo;
        private ICsvRepository _csvRepo;

        public CompareService(IGitHubRepository hubRepo = null, ICsvRepository csvRepo = null)
        {
            _hubRepo = hubRepo ?? new GitHubRepository();
            _csvRepo = csvRepo ?? new CsvRepository();
        }

        public IEnumerable<ComparedUser> CompareUsers()
        {
            var comparedUsers = new List<ComparedUser>();
            var localUsers = _csvRepo.GetUsers();
            var orgUsers = _hubRepo.GetListOfUsers(_hubRepo.GetCredentials());

            var usersToRemove = orgUsers.Where(u => !localUsers.Any(lu => lu.Login == u.Login))
                .Select(u => new ComparedUser
                {
                    Login = u.Login,
                    Name = u.Name,
                    Action = LeanHub.Shared.Enums.Action.Remove
                }).ToList();
            comparedUsers.AddRange(usersToRemove);

            var usersToAdd = localUsers.Where(u => !orgUsers.Any(ou => ou.Login == u.Login))
                .Select(u => new ComparedUser
                {
                    Login = u.Login,
                    Name = u.Name,
                    Action = LeanHub.Shared.Enums.Action.Add
                }).ToList();
            comparedUsers.AddRange(usersToAdd);
            
            return comparedUsers;
        }
    }
}
