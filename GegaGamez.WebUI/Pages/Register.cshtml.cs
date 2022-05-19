using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class RegisterModel : PageModel
    {
        //private readonly IJwtAuthenticationManager _authManager;
        private readonly IAuthManager _authManager;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private bool IsAnonymous => User.Identity?.IsAuthenticated == false;

        public RegisterModel(IUserService userService, IAuthManager authManager, IMapper mapper)
        {
            _userService = userService;
            _authManager = authManager;
            _mapper = mapper;
        }

        [BindProperty]
        public RegisterUserModel RegisterForm { get; set; }

        public IActionResult OnGet()
        {
            if (IsAnonymous)
                return Page();
            else
                return Forbid();
        }

        public async Task<IActionResult> OnPost()
        {
            if (IsAnonymous)
            {
                if (ModelState.IsValid)
                {
                    User user = _mapper.Map<User>(RegisterForm);

                    try
                    {
                        _userService.CreateUser(user);
                    }
                    catch (Exception ex)
                    {
                        // log...
                        ViewData ["InfoMessage"] = ex.Message;
                        return Page();
                    }

                    UserModel rightUser = _mapper.Map<UserModel>(user);
                    //var cookieResult = _authManager.SignInUser(rightUser);
                    //HttpContext.Response.Cookies
                    //.Append(cookieResult.cookieName, cookieResult.tokenValue, cookieResult.cookieOptions);

                    var (principal, properties) = _authManager.CreatePrincipalWithAuthProperties(user);
                    await HttpContext.SignInAsync(principal, properties);
                    return RedirectToPage("/Games/Search");
                }
                else
                    return RedirectToPage("/Register");
            }
            else
                return Forbid();
        }
    }
}
