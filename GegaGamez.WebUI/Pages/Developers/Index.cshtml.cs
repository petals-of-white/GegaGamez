using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers
{
    public class IndexModel : PageModel
    {
        private DeveloperService _devService;

        public IndexModel(DeveloperService devService)
        {
            _devService = devService;
        }
        public Developer Developer { get; set; }
        public IActionResult OnGet(int id)
        {
            var dev = _devService.GetById(id);

            if(dev is null)
            {
                return NotFound();
            }
            else
            {
                Developer = dev;
                return Page();
            }
        }
    }
}
