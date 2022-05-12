using System.Security.Claims;
using GegaGamez.Shared.Entities;
using Microsoft.AspNetCore.Authentication;

namespace GegaGamez.WebUI.Auth;

public interface IAuthManager
{
    (ClaimsPrincipal principal, AuthenticationProperties properties) CreatePrincipalWithAuthProperties(User user);
}
