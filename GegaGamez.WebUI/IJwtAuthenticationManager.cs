using GegaGamez.BLL;
using GegaGamez.Shared.BusinessModels;

namespace GegaGamez.WebUI
{
    public interface IJwtAuthenticationManager
    {
        (string cookieName, string tokenValue, CookieOptions cookieOptions) SignInUser(User user, bool RememberMe = true);

        public UserAuthResult AuthenticateUser(string username, string password);

        public UserAuthResult RegisterUser(string username, string password);
    }
}
