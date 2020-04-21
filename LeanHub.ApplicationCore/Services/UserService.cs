using LeanHub.DAL.Repositories;

namespace LeanHub.ApplicationCore.Services
{
    public class UserService
    {
        public static string HelloWorld()
        {
            return GitHubRepository.HelloWorld();
        }
        public static string AddUserToOrg(string name)
        {
            return GitHubRepository.AddUserToOrg(name);
        }

        public static string RemoveUserFromOrg(string name)
        {
            return GitHubRepository.RemoveUserFromOrg(name);
        }
    }
}
