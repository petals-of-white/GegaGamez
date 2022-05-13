using System.Security.Claims;
using GegaGamez.Shared.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GegaGamez.WebUI.Security;

public class AuthManager : IAuthManager
{
    private ClaimsPrincipal CreatePrincipal(User user)
    {
        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

        foreach (Role role in user.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role.Name));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(claimsIdentity);

        return principal;
    }

    public (ClaimsPrincipal principal, AuthenticationProperties properties) CreatePrincipalWithAuthProperties(User user)
    {
        var principal = CreatePrincipal(user);
        var properties = new AuthenticationProperties
        {
            ExpiresUtc = DateTime.UtcNow.AddHours(3),
            IsPersistent = true,
            IssuedUtc = DateTime.UtcNow
        };

        return (principal, properties);
    }
}
