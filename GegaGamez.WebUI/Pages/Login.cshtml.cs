using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
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
                        this.UserDoesNotExist(LoginForm.Username);

                        return RedirectToPage("/Login");
                    }
                    else
                    {
                        if (user.Password != LoginForm.Password)
                        {
                            _logger.LogInformation($"Incorrect password for user {user.Username}");
                            this.IncorrectPassword();

                            return RedirectToPage("/Login");
                        }
                        else
                        {
                            (var principal, var properties) = _authManager.CreatePrincipalWithAuthProperties(user);

                            await HttpContext.SignInAsync(principal, properties);

                            _logger.LogInformation($"Successfully signed in user {user.Username}");
                            this.LoggedIn(user.Username);

                            return RedirectToPage("/Games/Search");
                        }
                    }
                }
                else
                {
                    _logger.LogInformation($"Validation Errors: {ModelState.ErrorCount}");
                    this.ValidationError();

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
