using LeanHub.Shared.Enums;

namespace LeanHub.Shared.Models
{
    public class ComparedUser: User
    {
        public Action Action { get; set; }

        public override bool Equals(object obj)
        {
            return Equals((ComparedUser)obj);
        }

        private bool Equals(ComparedUser obj)
        {
            return Login == obj.Login && Name == obj.Name && Action == obj.Action;
        }
    }
}
