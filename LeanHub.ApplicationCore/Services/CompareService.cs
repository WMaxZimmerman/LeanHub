using System.Collections.Generic;
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
            var auth = _hubRepo.GetCredentials();
            var orgUsers = _hubRepo.GetListOfUsers(auth);
            
            return comparedUsers;
        }
    }
}
