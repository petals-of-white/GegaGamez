using System.Security.Claims;
using GegaGamez.Shared.Entities;
using Microsoft.AspNetCore.Authentication;

namespace GegaGamez.WebUI.Security;

public interface IAuthManager
{
    (ClaimsPrincipal principal, AuthenticationProperties properties) CreatePrincipalWithAuthProperties(User user);
}
