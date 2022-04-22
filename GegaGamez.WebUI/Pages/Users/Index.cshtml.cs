using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        public List<string> DefaultCollections { get; set; } = new();
        public List<string> UserCollections { get; set; } = new();
        public User? UserProfile { get; set; }

        public IActionResult OnGet(int id)
        {
            // get user
            UserProfile = _userService.GetById(id);

            if (UserProfile is null)
                return NotFound();
            else
            {
                // get collections
                _userService.LoadUsersCollections(UserProfile);
                return Page();
            }
        }
    }
}
