using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(ILogger<LogoutModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnGet()
        {
            int userId = User.GetId().GetValueOrDefault();

            await HttpContext.SignOutAsync();

            _logger.LogInformation($"User {userId} was signed out.");
            TempData [Messages.InfoKey] = "Signed out successfully";

            return RedirectToPage("/Games/Search");
        }
    }
}
