using System;

namespace LeanHub.Shared.Helpers
{
    public interface IEnvironmentHelper
    {
        string GetValue(string name);
    }

    public class EnvironmentHelper : IEnvironmentHelper
    {
        public string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
