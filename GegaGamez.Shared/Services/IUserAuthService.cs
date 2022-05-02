using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IUserAuthService
{
    UserAuthResult Authenticate(string username, string password);
    UserAuthResult CreateNewUser(string username, string password, string? name = null, Country? country = null, string? about = null);
    UserAuthResult CreateNewUser(User newUser);
}
