using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IMapper _mapper;

        private readonly IJwtAuthenticationManager _authManager;
        private readonly IUserService _userService;

        public LoginModel(IUserService userService, IJwtAuthenticationManager authManager, IMapper mapper)
        {
            _authManager = authManager;
            _userService = userService;
            _mapper = mapper;
        }

        [BindProperty]
        public LoginUserModel LoginForm { get; set; }

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
            if (ModelState.IsValid)
            {
                var user = _userService.GetByUsername(LoginForm.Username);

                if (user is null)
                    return RedirectToPage("/Login");
                else
                {
                    if (user.Password != LoginForm.Password)
                        return RedirectToPage("/Login");
                    else
                    {
                        UserModel rightUser = _mapper.Map<UserModel>(user);

                        var cookieResult = _authManager.SignInUser(rightUser);

                        HttpContext.Response.Cookies
                            .Append(cookieResult.cookieName, cookieResult.tokenValue, cookieResult.cookieOptions);

                        return RedirectToPage("/Games/Search");
                    }
                }
            }
            else
                return RedirectToPage("/Login");
        }
    }
}
