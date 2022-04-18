using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<string> DefaultCollections { get; set; } = new();
        public List<string> UserCollections { get; set; } = new();
        public void OnGet()
        {
            // get user
            // get collections
        }
    }
}
