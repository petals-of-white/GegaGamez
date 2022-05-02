using GegaGamez.Shared;
using GegaGamez.WebUI.Models.Display;

namespace GegaGamez.WebUI
{
    public interface IJwtAuthenticationManager
    {
        (string cookieName, string tokenValue, CookieOptions cookieOptions) SignInUser(UserModel user, bool RememberMe = true);

        public UserAuthResult AuthenticateUser(string username, string password);

        public UserAuthResult RegisterUser(string username, string password);
    }
}
