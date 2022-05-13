using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            //HttpContext.Response.Cookies.Delete("access_token");
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Games/Search");
        }
    }
}
