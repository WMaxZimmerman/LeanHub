namespace LeanHub.Shared.Helpers
{
    public interface IConfigHelper
    {
        string Username { get; }
        string Password { get; }
    }

    public class ConfigHelper : IConfigHelper
    {
        private IEnvironmentHelper _env;

        public ConfigHelper(IEnvironmentHelper env = null)
        {
            _env = env ?? new EnvironmentHelper();
        }
        
        public string Username => _env.GetValue("USERNAME");

        public string Password => _env.GetValue("PASSWORD");
    }
}
