using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GegaGamez.WebUI.Models.Display;
using Microsoft.IdentityModel.Tokens;

namespace GegaGamez.WebUI.Auth
{
    internal class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string _key;
        public const string CookieName = "jwt";


        private string GenerateToken(ICollection<Claim> userClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.UTF8.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddHours(1),
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ICollection<Claim> GetClaims(UserModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var roles = new List<string>();

            roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
            return claims;
        }

        

    public JwtAuthenticationManager(string key)
    {
        _key = key;
    }

    /// <summary>
    /// Returns a cookie with a JWT token
    /// </summary>
    /// <param name="user"></param>
    /// <param name="RememberMe"></param>
    /// <returns></returns>
    public (string cookieName, string tokenValue, CookieOptions cookieOptions) SignInUser(UserModel user, bool RememberMe = true)
    {
        var claims = GetClaims(user);
        var token = GenerateToken(claims);
        var cookieOptions = new CookieOptions { HttpOnly = true };

        return (CookieName, token, cookieOptions);
    }
}
}
