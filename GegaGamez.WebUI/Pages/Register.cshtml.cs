using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
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
    public class RegisterModel : PageModel
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService, IAuthManager authManager, IMapper mapper, ILogger<RegisterModel> logger)
        {
            _userService = userService;
            _authManager = authManager;
            _mapper = mapper;
            _logger = logger;
        }

        [BindProperty]
        public RegisterUserModel RegisterForm { get; set; }

        public IActionResult OnGet()
        {
            if (User.IsAuthenticated() == false)
            {
                _logger.LogInformation("Showing register page for an unauthenticated user.");

                return Page();
            }
            else
            {
                _logger.LogInformation($"Authenticated user {User.GetId()} tried to access register page. Denying access.");

                return Forbid();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (User.IsAuthenticated() == false)
            {
                if (ModelState.IsValid)
                {
                    User user = _mapper.Map<User>(RegisterForm);

                    try
                    {
                        _userService.CreateUser(user);
                    }
                    catch (UniqueEntityException ex)
                    {
                        _logger.LogWarning(ex, $"Failed to create a user {user.Username} due to unique constraint violation.");
                        TempData [Messages.InfoKey] = "User alreadye exists. Please choose a different username";

                        return Page();
                    }

                    var (principal, properties) = _authManager.CreatePrincipalWithAuthProperties(user);

                    await HttpContext.SignInAsync(principal, properties);

                    _logger.LogInformation($"User {User.GetId()} has signed in.");
                    TempData [Messages.InfoKey] = "Registration was successful";

                    return RedirectToPage("/Games/Search");
                }
                else
                {
                    _logger.LogDebug($"Validation errors: {ModelState.ErrorCount}");
                    TempData [Messages.InfoKey] = "Validation erorrs";

                    return RedirectToPage("/Register");
                }
            }
            else
            {
                _logger.LogInformation($"Authenticated user {User.GetId()} tried to post registration form. Denying access.");

                return Forbid();
            }
        }
    }
}
