using System.ComponentModel.DataAnnotations;
using GegaGamez.BLL.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IJwtAuthenticationManager _authManager;

        public LoginModel(IJwtAuthenticationManager authenticationManager)
        {
            _authManager = authenticationManager;
        }

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                {
                    return BadRequest();
                }
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            var authResult = _authManager.AuthenticateUser(Username, Password);

            if (authResult.Status == AuthStatus.Success)
            {
                var cookie = _authManager.SignInUser(authResult.User!);
                HttpContext.Response.Cookies.Append(cookie.cookieName, cookie.tokenValue, cookie.cookieOptions);

                return RedirectToPage("/Games/Search");
            }
            else
            {
                return Page();
            }
            //string? token = _authenticationManager.Authenticate(Username, Password);

            //if(token is null)
            //{
            //    return Unauthorized();
            //}

            //else
            //{
            //    return Page();
            //}
        }
    }
}
