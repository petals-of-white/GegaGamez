using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers;

public class IndexModel : PageModel
{
    private readonly IMapper _mapper;
    private IDeveloperService _devService;

    public IndexModel(IDeveloperService devService, IMapper mapper)
    {
        _devService = devService;
        _mapper = mapper;
    }

    public DeveloperModel Developer { get; set; }

    public IActionResult OnGet(int id)
    {
        var dev = _devService.GetById(id);

        if (dev is null)
        {
            return NotFound();
        }
        else
        {
            Developer = _mapper.Map<DeveloperModel>(dev);
            return Page();
        }
    }
}
