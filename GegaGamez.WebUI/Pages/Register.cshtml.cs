using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public RegisterModel(IUserService userService, IJwtAuthenticationManager authManager, IMapper mapper)
        {
            _userService = userService;
            _authManager = authManager;
            _mapper = mapper;
        }

        [BindProperty]
        public RegisterUserModel RegisterForm { get; set; }

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
                User user = _mapper.Map<User>(RegisterForm);

                try
                {
                    _userService.Create(user);
                }
                catch (Exception ex)
                {
                    // log...
                    ViewData ["Error"] = ex.Message;
                    return Page();
                }

                UserModel rightUser = _mapper.Map<UserModel>(user);

                var cookieResult = _authManager.SignInUser(rightUser);

                HttpContext.Response.Cookies
                    .Append(cookieResult.cookieName, cookieResult.tokenValue, cookieResult.cookieOptions);

                return RedirectToPage("/Games/Search");
            }
            else
                return RedirectToPage("/Register");
        }

        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _userService.Create()
        //        }
        //        _userService
        //        var newUserResult = _authManager.RegisterUser(Username, Password);

        // if (newUserResult.Status == AuthStatus.Success) { var user =
        // _mapper.Map<UserModel>(newUserResult.User); var cookie = _authManager.SignInUser(user);
        // HttpContext.Response.Cookies.Append(cookie.cookieName, cookie.tokenValue, cookie.cookieOptions);

        //            return RedirectToPage("/Games/Search");
        //        }
        //        else
        //        {
        //            return Page();
        //        }
        //    }
        //    else
        //    {
        //        return Page();
        //    }
        //}
    }
}
