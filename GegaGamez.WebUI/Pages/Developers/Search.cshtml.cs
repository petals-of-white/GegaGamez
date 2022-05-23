using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers;

public class SearchModel : PageModel
{
    private readonly IMapper _mapper;
    private readonly ILogger<SearchModel> _logger;
    private IDeveloperService _devService;

    public SearchModel(IDeveloperService devService, IMapper mapper, ILogger<SearchModel> logger)
    {
        _devService = devService;
        _mapper = mapper;
        _logger = logger;
    }

    public List<DeveloperModel> Developers { get; set; }

    [BindProperty(SupportsGet = true)]
    public string DevSearchName { get; set; }

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(DevSearchName))
        {
            _logger.LogDebug($"Developer search string is empty");
            var devs = _devService.FindAll();
            Developers = _mapper.Map<List<DeveloperModel>>(devs.ToList());
        }
        else
        {
            _logger.LogDebug($"Developer search string: {DevSearchName}");
            var devs = _devService.Find(DevSearchName);
            Developers = _mapper.Map<List<DeveloperModel>>(devs.ToList());
        }
        _logger.LogInformation($"{Developers.Count} developers found;");
    }
}
