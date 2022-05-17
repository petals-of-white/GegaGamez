using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private bool IsAnonymous => User.Identity?.IsAuthenticated == false;

        public LoginModel(IUserService userService, IAuthManager authManager, IMapper mapper)
        {
            _authManager = authManager;
            _userService = userService;
            _mapper = mapper;
        }

        [BindProperty]
        public LoginUserModel LoginForm { get; set; }

        public IActionResult OnGet()
        {
            if (IsAnonymous)
                return Page();
            else
            {
                ViewData ["Error"] = "You can not do that because you are already logged in!";
                return Forbid();
            }
            //if (User?.Identity?.IsAuthenticated == true)
            //    return BadRequest();
            //else
            //    return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (IsAnonymous)
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

                            (var principal, var properties) = _authManager.CreatePrincipalWithAuthProperties(user);
                            await HttpContext.SignInAsync(principal, properties);
                            //var signInResult = SignIn(principal, properties, CookieAuthenticationDefaults.AuthenticationScheme);

                            //var cookieResult = _authManager.SignInUser(rightUser);

                            //HttpContext.Response.Cookies
                            //    .Append(cookieResult.cookieName, cookieResult.tokenValue, cookieResult.cookieOptions);

                            //return signInResult;

                            return RedirectToPage("/Games/Search");
                        }
                    }
                }
                else
                    return RedirectToPage("/Login");
            }
            else
            {
                ViewData ["Error"] = "You can not do that because you are already logged in!";
                return Forbid();
            }
        }
    }
}
