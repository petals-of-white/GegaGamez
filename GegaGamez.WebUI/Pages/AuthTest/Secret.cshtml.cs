using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.AuthTest
{
    [Authorize]
    public class SecretModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
