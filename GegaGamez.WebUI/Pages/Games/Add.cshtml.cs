using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Games;

[Authorize(Roles = Roles.Admin)]
public class AddModel : PageModel
{
    private readonly IDeveloperService _developerService;
    private readonly IGameService _gameService;
    private readonly ILogger<AddModel> _logger;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    private List<SelectListItem> GetDevelopers() =>
      _developerService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    private List<SelectListItem> GetGenres() =>
      _genreService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    public AddModel(IMapper mapper, IDeveloperService developerService, IGenreService genreService, IGameService gameService, ILogger<AddModel> logger)
    {
        _mapper = mapper;
        _gameService = gameService;
        _logger = logger;
        _developerService = developerService;
        _genreService = genreService;
    }

    public List<SelectListItem> Developers { get; set; }
    public List<SelectListItem> Genres { get; set; }

    [BindProperty]
    public EditGameModel NewGame { get; set; }

    public void OnGet()
    {
        Developers = GetDevelopers();
        Genres = GetGenres();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _logger.LogDebug($"Game model has some validation errors: {ModelState.Values}");

            var newGame = _mapper.Map<Game>(NewGame);

            try
            {
                _gameService.CreateGame(newGame);
            }
            catch (UniqueEntityException ex)
            {
                _logger.LogWarning(ex, "Failed to create a new game.");

                TempData [Messages.InfoKey] = "Failed to create a new game. It most likely aleady exists";
                return RedirectToPage();
            }
            return RedirectToPage("/Games/Index", new { id = newGame.Id });
        }
        else
        {
            _logger.LogDebug($"{ModelState.ErrorCount} validation errors.");

            this.ValidationError();
            return RedirectToPage();
        }
    }
}
