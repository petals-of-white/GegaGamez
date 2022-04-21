using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers
{
    public class SearchModel : PageModel
    {
        private DeveloperService _devService;

        public SearchModel(DeveloperService devService)
        {
            _devService = devService;
        }

        [BindProperty(SupportsGet = true)]
        public string DevSearchName { get; set; }

        public List<Developer> Developers { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(DevSearchName))
            {
                Developers = _devService.GetAll().ToList();
            }
            else
            {
                Developers = _devService.FindByName(DevSearchName).ToList();
            }
        }
    }
}
