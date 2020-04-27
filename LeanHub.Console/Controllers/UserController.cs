using LeanHub.ApplicationCore.Services;
using NTrospection.CLI.Attributes;

namespace LeanHub.Console.Controllers
{
    [CliController("user", "A collection of methods to interact with members of the organization")]
    public class UserController
    {
        private IUserService _service;

        public UserController()
        {
            _service = new UserService();
        }
        
        public UserController(IUserService service)
        {
            _service = service;
        }

        
        [CliCommand("add", "Adds the given user to the organization")]
        public void AddUser(string name, string username, string password)
        {
            var user = _service.AddUserToOrg(name, username, password);
            var message = $"{user.User.Login} ({user.State})";
            System.Console.WriteLine(message);
        }

        [CliCommand("remove", "Removes the given user from the organization")]
        public void RemoveUser(string name, string username, string password)
        {
            var wasRemoved = _service.RemoveUserFromOrg(name, username, password);
            var message = wasRemoved ?
                $"{name} was successfully removed" :
                $"something went wrong trying to remove {name}";
            System.Console.WriteLine(message);
        }

        [CliCommand("list", "Outputs a list of all users in the organization")]
        public void GetUsers(string username, string password)
        {
            var users = _service.GetUsers(username, password);
            foreach(var user in users)
            {
                System.Console.WriteLine(user.Login);
            }
        }
    }
}
