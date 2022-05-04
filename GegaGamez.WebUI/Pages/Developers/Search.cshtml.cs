using AutoMapper;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers;

public class SearchModel : PageModel
{
    private readonly IMapper _mapper;
    private IDeveloperService _devService;

    public SearchModel(IDeveloperService devService, IMapper mapper)
    {
        _devService = devService;
        this._mapper = mapper;
    }

    [BindProperty(SupportsGet = true)]
    public string DevSearchName { get; set; }

    public List<DeveloperModel> Developers { get; set; }

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(DevSearchName))
        {
            var devs = _devService.GetAll();
            Developers = _mapper.Map<List<DeveloperModel>>(devs.ToList());
        }
        else
        {
            var devs = _devService.Find(DevSearchName);
            Developers = _mapper.Map<List<DeveloperModel>>(devs.ToList());
        }
    }
}
