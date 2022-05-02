using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GegaGamez.BLL.Services;
using GegaGamez.Shared;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.IdentityModel.Tokens;

namespace GegaGamez.WebUI
{
    internal class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IUserAuthService _userAuthService;

        private readonly string _key;

        public JwtAuthenticationManager(IUserAuthService userAuthService, string key)
        {
            _key = key;
            _userAuthService = userAuthService;
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //public string? Authenticate(string username, string password)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();

        // var tokenKey = Encoding.UTF8.GetBytes(_key);

        // var tokenDescriptor = new SecurityTokenDescriptor { Subject = new ClaimsIdentity(new
        // Claim [] { new Claim(ClaimTypes.Name, username) }), Expires =
        // DateTime.UtcNow.AddHours(1), SigningCredentials = new SigningCredentials( new
        // SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256) };

        // var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return tokenHandler.WriteToken(token);
        //}

        public string GenerateToken(ICollection<Claim> userClaims)
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

        public ICollection<Claim> GetClaims(UserModel user)
        {
            var claims = new Claim []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            return claims;
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

            return ("access_token", token, cookieOptions);
        }

        public UserAuthResult AuthenticateUser(string username, string password)
        {
            return _userAuthService.Authenticate(username, password);
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="MultipleValidationsException"></exception>

        public UserAuthResult RegisterUser(string username, string password)
        {
            UserAuthResult newUser = _userAuthService.CreateNewUser(username, password);
            return newUser;
        }


    }
}
