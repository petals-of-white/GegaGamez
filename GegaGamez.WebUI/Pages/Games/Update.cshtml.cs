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
public class UpdateModel : PageModel
{
    private readonly IDeveloperService _developerService;
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly ILogger<UpdateModel> _logger;
    private readonly IMapper _mapper;

    private List<SelectListItem> GetDevelopers() =>
      _developerService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    private List<SelectListItem> GetGenres() =>
      _genreService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    private void MarkDevSelected()
    {
        SelectListItem? dev = Developers.SingleOrDefault(g => g.Value == UpdatedGame?.DeveloperId.ToString());
        if (dev is not null) dev.Selected = true;
        _logger.LogTrace("Game dev marked selected");
    }

    private void MarkGenresSelected()
    {
        var genres = Genres.Where(g => UpdatedGame.GenreIds.Contains(int.Parse(g.Value)));

        foreach (var genre in genres)
            genre.Selected = true;
        _logger.LogTrace("Game genres marked selected");
    }

    public UpdateModel(ILogger<UpdateModel> logger, IMapper mapper, IDeveloperService developerService, IGenreService genreService, IGameService gameService)
    {
        _logger = logger;
        _mapper = mapper;
        _gameService = gameService;
        _developerService = developerService;
        _genreService = genreService;
    }

    public List<SelectListItem> Developers { get; set; }

    public List<SelectListItem> Genres { get; set; }

    [BindProperty]
    public EditGameModel UpdatedGame { get; set; }

    public IActionResult OnGet(int id)
    {
        Developers = GetDevelopers();
        Genres = GetGenres();

        Game? game = _gameService.GetById(id);

        if (game is not null)
        {
            game.Genres = _genreService.GetGameGenres(game).ToList();
            UpdatedGame = _mapper.Map<EditGameModel>(game);

            MarkDevSelected();
            MarkGenresSelected();

            _logger.LogInformation($"Loading update page for game {id}");

            return Page();
        }
        else
        {
            _logger.LogInformation($"A game with id {id} was not found.");
            return NotFound();
        }
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            var updatedGame = _mapper.Map<Game>(UpdatedGame);
            try
            {
                _gameService.UpdateGame(updatedGame);
                _logger.LogInformation($"Game has been updated: {UpdatedGame}");

                return RedirectToPage("/Games/Index", new { id = updatedGame.Id });
            }
            catch (UniqueEntityException ex)
            {
                _logger.LogWarning(ex, $"Cannot update the game {updatedGame.Id}");
                TempData [Messages.InfoKey] = "Failed to update the game. Please try again";

                return RedirectToPage(new { updatedGame.Id });
            }
            catch(EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Cannot update the game {updatedGame.Id}");
                TempData [Messages.InfoKey] = "Failed to update the game. Please try again";

                return RedirectToPage(new { updatedGame.Id });
            }
        }
        else
        {
            _logger.LogDebug($"Validation errors: {ModelState.ErrorCount}");
            TempData [Messages.InfoKey] = "Wrong input. Please check";

            return RedirectToPage(new { id = UpdatedGame.Id });
        }
    }
}
