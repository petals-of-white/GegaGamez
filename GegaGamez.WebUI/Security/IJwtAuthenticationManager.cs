using GegaGamez.WebUI.Models.Display;

namespace GegaGamez.WebUI.Security
{
    public interface IJwtAuthenticationManager
    {
        (string cookieName, string tokenValue, CookieOptions cookieOptions) SignInUser(UserModel user, bool RememberMe = true);
    }
}
