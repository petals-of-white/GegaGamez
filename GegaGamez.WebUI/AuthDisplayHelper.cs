using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GegaGamez.WebUI
{
    public class AuthDisplayHelper
    {
        private readonly ClaimsPrincipal _user;

        public AuthDisplayHelper(ClaimsPrincipal user)
        {
            _user = user;
        }

        public bool IsAuthenticated => _user?.Identity?.IsAuthenticated == true;

        public int? UserId
        {
            get
            {
                int? userId = null;

                if (IsAuthenticated)
                {
                    userId = Convert.ToInt32(
                        _user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
                }

                return userId;
            }
        }

        public bool IsAuthorized(int id) => id == UserId;
    }
}
