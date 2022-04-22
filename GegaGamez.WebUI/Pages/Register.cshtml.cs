using System.ComponentModel.DataAnnotations;
using GegaGamez.BLL.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly IJwtAuthenticationManager _authManager;

        public RegisterModel(IJwtAuthenticationManager authManager)
        {
            _authManager = authManager;
        }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Please verify your password")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var newUserResult = _authManager.RegisterUser(Username, Password);

                if (newUserResult.Status == AuthStatus.Success)
                {
                    var cookie = _authManager.SignInUser(newUserResult.User!);
                    HttpContext.Response.Cookies.Append(cookie.cookieName, cookie.tokenValue, cookie.cookieOptions);

                    return RedirectToPage("/Games/Search");
                }

                else
                {
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
