using LeanHub.ApplicationCore.Services;
using LeanHub.Shared.Helpers;
using NTrospection.CLI.Attributes;

namespace LeanHub.Console.Controllers
{
    [CliController("user", "A collection of methods to interact with members of the organization")]
    public class UserController
    {
        private IUserService _service;
        private ISyncService _syncService;
        private IConsoleHelper _console;

        public UserController()
        {
            _service = new UserService();
            _console = new ConsoleHelper();
            _syncService = new SyncService();
        }
        
        public UserController(IUserService service, IConsoleHelper console, ISyncService syncService)
        {
            _service = service;
            _console = console;
            _syncService = syncService;
        }

        
        [CliCommand("add", "Adds the given user to the organization")]
        public void AddUser(string name)
        {
            var user = _service.AddUserToOrg(name);
            var message = $"{user.User.Login} ({user.State})";
            _console.WriteLine(message);
        }

        [CliCommand("remove", "Removes the given user from the organization")]
        public void RemoveUser(string name)
        {
            var wasRemoved = _service.RemoveUserFromOrg(name);
            var message = wasRemoved ?
                $"{name} was successfully removed" :
                $"something went wrong trying to remove {name}";
            _console.WriteLine(message);
        }

        [CliCommand("list", "Outputs a list of all users in the organization")]
        public void GetUsers()
        {
            var users = _service.GetUsers();
            foreach(var user in users)
            {
                _console.WriteLine(user.Login);
            }
        }

        [CliCommand("local", "Outputs a list of all users in the organization")]
        public void GetLocalUsers()
        {
            var users = _service.GetLocalUsers();
            foreach(var user in users)
            {
                _console.WriteLine($"{user.Name} ({user.Login})");
            }
        }

        [CliCommand("sync", "Syncs the users in the Org with the users in the CSV.")]
        public void SyncUsers()
        {
            _syncService.SyncUsers();
        }
    }
}
