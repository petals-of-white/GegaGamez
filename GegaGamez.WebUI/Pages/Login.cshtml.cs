using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LoginModel(IUserService userService, IAuthManager authManager, IMapper mapper, ILogger<LoginModel> logger)
        {
            _authManager = authManager;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [BindProperty]
        public LoginUserModel LoginForm { get; set; }

        public IActionResult OnGet()
        {
            if (User.IsAuthenticated() == false)
            {
                _logger.LogInformation("Showing loging page for unauthenticated user.");

                return Page();
            }
            else
            {
                _logger.LogInformation("Authenticated user tried to access login page.");
                TempData [Messages.InfoKey] = "You can not do that because you are already logged in!";
                
                return Forbid();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (User.IsAuthenticated() == false)
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.GetByUsername(LoginForm.Username);

                    if (user is null)
                    {
                        _logger.LogInformation($"User with username {LoginForm.Username} was not found.");
                        TempData [Messages.InfoKey] = $"User with username {LoginForm.Username} was not found.";

                        return RedirectToPage("/Login");
                    }
                    else
                    {
                        if (user.Password != LoginForm.Password)
                        {
                            _logger.LogInformation($"Incorrect password for user {user.Username}");
                            TempData [Messages.InfoKey] = $"Incorrect password for user {user.Username}";

                            return RedirectToPage("/Login");
                        }
                        else
                        {
                            (var principal, var properties) = _authManager.CreatePrincipalWithAuthProperties(user);

                            await HttpContext.SignInAsync(principal, properties);

                            _logger.LogInformation($"Successfully signed in user {user.Username}");
                            TempData [Messages.InfoKey] = "Successfully loged in.";

                            return RedirectToPage("/Games/Search");
                        }
                    }
                }
                else
                {
                    _logger.LogInformation($"Validation Errors: {ModelState.ErrorCount}");
                    TempData [Messages.InfoKey] = "validation errors.";

                    return RedirectToPage("/Login");
                }
            }
            else
            {
                TempData [Messages.InfoKey] = "You can not do that because you are already logged in!";

                return Forbid();
            }
        }
    }
}
