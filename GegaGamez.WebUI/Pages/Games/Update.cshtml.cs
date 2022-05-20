using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Games;

[Authorize(Roles = Roles.Admin)]
public class UpdateModel : PageModel
{
    private readonly IMapper _mapper;
    private readonly IGameService _gameService;
    private readonly IDeveloperService _developerService;
    private readonly IGenreService _genreService;

    [BindProperty]
    public EditGameModel UpdatedGame { get; set; }
    public UpdateModel(IMapper mapper, IDeveloperService developerService, IGenreService genreService, IGameService gameService)
    {
        _mapper = mapper;
        _gameService = gameService;
        _developerService = developerService;
        _genreService = genreService;
    }

    public List<SelectListItem> Developers { get; set; }
    public List<SelectListItem> Genres { get; set; }

    private List<SelectListItem> GetDevelopers() =>
      _developerService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    private List<SelectListItem> GetGenres() =>
      _genreService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    public void OnGet(int id)
    {
        Developers = GetDevelopers();
        Genres = GetGenres();


    }
    public IActionResult OnPost()
    {
        var updatedGame = _mapper.Map<Game>(UpdatedGame);
        try
        {
            _gameService.UpdateGame(updatedGame);
            return RedirectToPage("/Games/Index", new { id = updatedGame.Id });
        }
        catch(Exception ex)
        {
            // log
            ViewData ["InfoMessage"] = "Failed to update the game. Please try again";
            return RedirectToPage(new {updatedGame.Id});
        }
    }
}
