using System.Linq;

namespace LeanHub.ApplicationCore.Services
{
    public interface ISyncService
    {
        void SyncUsers();
    }

    public class SyncService: ISyncService
    {
        private ICompareService _compare;
        private IUserService _user;

        public SyncService(ICompareService compare = null, IUserService user = null)
        {
            _compare = compare ?? new CompareService();
            _user = user ?? new UserService();
        }

        public void SyncUsers()
        {
            var users = _compare.CompareUsers();

            foreach (var user in users.Where(u => u.Action == Shared.Enums.Action.Remove))
            {
                _user.RemoveUserFromOrg(user.Login);
            }

            foreach (var user in users.Where(u => u.Action == Shared.Enums.Action.Add))
            {
                _user.AddUserToOrg(user.Login);
            }
        }
    }
}
