using System.Security.Claims;

namespace GegaGamez.WebUI.Security;

public static class SecurityExtensions
{
    public static int? GetId(this ClaimsPrincipal user)
    {
        string? idValue = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return idValue is not null ? Convert.ToInt32(idValue) : null;
    }

    public static bool IsAdmin(this ClaimsPrincipal user) =>
            user.IsInRole(Roles.Admin);

    public static bool IsAuthenticated(this ClaimsPrincipal user) =>
        user.Identity?.IsAuthenticated == true;

    public static bool IsUser(this ClaimsPrincipal user) =>
        user.IsInRole(Roles.User);
}
