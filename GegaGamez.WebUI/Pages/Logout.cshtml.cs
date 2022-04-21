using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet() => NotFound();

    }
}
