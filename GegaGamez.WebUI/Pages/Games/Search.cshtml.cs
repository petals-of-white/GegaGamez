using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GegaGamez.WebUI.Pages.Games
{
    public class SearchModel : PageModel
    {
       

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public List<string> Genres { get; set; } = new();
        public List<string> Games { get; set; } = new();
        public void OnGet()
        {
            // use game search logic here
            //Games = FindGamesByName(SearchString);
        }

        public void OnPost()
        {

        }
    }
}
