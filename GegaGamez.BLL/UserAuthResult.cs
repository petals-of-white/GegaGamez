using GegaGamez.BLL.Enums;
using GegaGamez.Shared.BusinessModels;

namespace GegaGamez.BLL
{
    public class UserAuthResult
    {
        internal UserAuthResult(User? user, AuthStatus status)
        {
            Status = status;
            User = user;
        }

        public AuthStatus Status { get; internal init; }
        public User? User { get; internal init; }
    }
}
