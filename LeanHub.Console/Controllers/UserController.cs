using System;
using LeanHub.ApplicationCore.Services;
using NTrospection.CLI.Attributes;

namespace LeanHub.Console.Controllers
{
    [CliController("user", "An example of a CLI Controller")]
    public class UserController
    {
        [CliCommand("add", "A Hello World for a CLI Project")]
        public void AddUser(string name)
        {
            System.Console.WriteLine(UserService.AddUserToOrg(name));
        }

        [CliCommand("remove", "butt")]
        public void RemoveUser(string name)
        {
            System.Console.WriteLine(UserService.RemoveUserFromOrg(name));
        }
    }
}
