using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Enums;

namespace GegaGamez.Shared;

public class UserAuthResult
{
    public UserAuthResult(User? user, AuthStatus status)
    {
        Status = status;
        User = user;
    }

    public AuthStatus Status { get; internal init; }
    public User? User { get; internal init; }
}
