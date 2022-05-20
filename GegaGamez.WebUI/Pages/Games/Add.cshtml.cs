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
public class AddModel : PageModel
{
    private readonly IDeveloperService _developerService;
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    private List<SelectListItem> GetDevelopers() =>
      _developerService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    private List<SelectListItem> GetGenres() =>
      _genreService.FindAll().Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

    public AddModel(IMapper mapper, IDeveloperService developerService, IGenreService genreService, IGameService gameService)
    {
        _mapper = mapper;
        _gameService = gameService;
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
            var newGame = _mapper.Map<Game>(NewGame);
            try
            {
                _gameService.CreateGame(newGame);
            }
            catch (Exception ex)
            {
                // log
                ViewData ["InfoMessage"] = "Failed to create a new game. It most likely aleady exists";
                return RedirectToPage();
            }
            return RedirectToPage("/Games/Index", new { id = newGame.Id });
        }
        else
        {
            ViewData ["InfoMessage"] = "Wrong input. Please check";
            return RedirectToPage();
        }
    }
}
