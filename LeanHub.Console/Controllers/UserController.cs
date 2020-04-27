using System;
using LeanHub.ApplicationCore.Services;
using NTrospection.CLI.Attributes;

namespace LeanHub.Console.Controllers
{
    [CliController("user", "An example of a CLI Controller")]
    public class UserController
    {
        private IUserService _service;
        public UserController(IUserService service = null)
        {
            _service = service ?? new UserService();
        }

        
        [CliCommand("add", "A Hello World for a CLI Project")]
        public void AddUser(string name)
        {
            System.Console.WriteLine(_service.AddUserToOrg(name));
        }

        [CliCommand("remove", "butt")]
        public void RemoveUser(string name)
        {
            System.Console.WriteLine(_service.RemoveUserFromOrg(name));
        }
    }
}
