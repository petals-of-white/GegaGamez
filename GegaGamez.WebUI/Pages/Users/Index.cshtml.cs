using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users
{
    public class IndexModel : PageModel
    {
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            Page();
        }
    }
}
