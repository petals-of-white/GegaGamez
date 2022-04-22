using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users
{
    public class SearchModel : PageModel
    {
        private readonly UserService _userService;

        public SearchModel(UserService users)
        {
            _userService = users;
        }

        [BindProperty(SupportsGet = true)]
        public string? UsernameSearchString { get; set; }

        public List<User> Users { get; set; } = new();

        public void OnGet()
        {
            //if ()
            //Users = string.IsNullOrEmpty(UsernameSearchString) ? Users : _users
            //    .FindByUsername(UsernameSearchString).ToList();

            if (string.IsNullOrWhiteSpace(UsernameSearchString))
            {
                Users = _userService.GetAll().ToList();
            }
            else
            {
                Users = _userService.FindByUsername(UsernameSearchString).ToList();
            }
        }
    }
}
