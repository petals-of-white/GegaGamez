using System.ComponentModel.DataAnnotations;
using GegaGamez.BLL;
using GegaGamez.BLL.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.AuthTest
{
    public class IndexModel : PageModel
    {
        private readonly IJwtAuthenticationManager _authManager;

        public IndexModel(IJwtAuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return OnPost();
        }

        public IActionResult OnPost()
        {
            UserAuthResult authResult = _authManager.AuthenticateUser(Username, Password);

            if (authResult.Status == AuthStatus.Success)
            {
                var cookie = _authManager.SignInUser(authResult.User!);
                HttpContext.Response.Cookies.Append(cookie.cookieName, cookie.tokenValue, cookie.cookieOptions);
                return RedirectToPage("./Secret");
            }
            else
            {
                // wrong

                return RedirectToPage("/Games/Search");
            }
        }
    }
}
