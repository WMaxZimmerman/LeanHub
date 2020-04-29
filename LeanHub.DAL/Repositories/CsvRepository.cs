using System.Collections.Generic;
using System.IO;
using System.Linq;
using LeanHub.Shared.Models;

namespace LeanHub.DAL.Repositories
{
    public interface ICsvRepository
    {
        IEnumerable<User> GetUsers();
    }

    public class CsvRepository : ICsvRepository
    {
        public IEnumerable<User> GetUsers()
        {
            var filePath = "../users.csv";
            var lines = File.ReadLines(filePath).ToList();
            var users = new List<User>();

            for (var i = 1; i < lines.Count; i++)
            {
                var line = lines[i];
                var values = line.Split(',');
                var user = new User
                {
                    Name = values[0],
                    Login = values[1]
                };
                users.Add(user);
            }
            
            return users;
        }
    }
}
