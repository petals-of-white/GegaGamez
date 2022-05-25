using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Developers;

public class IndexModel : PageModel
{
    private readonly IDeveloperService _devService;
    private readonly IGameService _gameService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMapper _mapper;

    public IndexModel(IDeveloperService devService, IGameService gameService, IMapper mapper, ILogger<IndexModel> logger)
    {
        _devService = devService;
        _gameService = gameService;
        _mapper = mapper;
        _logger = logger;
    }

    public DeveloperModel Developer { get; set; }

    public IActionResult OnGet(int id)
    {
        var dev = _devService.GetById(id);
        if (dev is null)
        {
            _logger.LogInformation($"Developer with id {id} was not found");

            return NotFound();
        }
        else
        {
            Developer = _mapper.Map<DeveloperModel>(dev);
            _logger.LogInformation($"Found a developer: {Developer}");

            Developer.NumberOfGames = _gameService.GetNumberOfGamesForDeveloper(dev);

            return Page();
        }
    }
}
