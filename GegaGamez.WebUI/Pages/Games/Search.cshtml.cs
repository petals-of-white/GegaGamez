using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Games;

public class SearchModel : PageModel
{
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public SearchModel(IGameService gameService, IGenreService genreService, IMapper mapper)
    {
        _gameService = gameService;
        _genreService = genreService;
        _mapper = mapper;
    }

    [BindProperty(SupportsGet = true)]
    public string? GameTitle { get; set; }

    [BindProperty(SupportsGet = true)]
    public HashSet<int> ByGenre { get; set; } = new();

    public List<GenreModel> Genres { get; set; } = new();
    public List<GameModel> Games { get; set; } = new();

    public void OnGet()
    {
        // Get all the genres
        var genres = _genreService.FindAll().ToList();
        Genres = _mapper.Map<List<GenreModel>>(genres);

        // display all the games if no search string
        IEnumerable<Game> games;
        if (string.IsNullOrWhiteSpace(GameTitle))
        {
            games = _gameService.FindAll();
        }
        else
        {
            //games = _gameService.FindByTitle(GameTitle);
            games = _gameService.Find(GameTitle);
        }

        // fill genres for each game
        foreach (var game in games)
        {
            game.Genres = _genreService.GetGameGenres(game).ToHashSet();
            //_gameService.LoadGameGenres(game);
        }

        // filter games by genre
        games = (from game in games
                 where game.Genres.Select(genre => genre.Id).ToHashSet().IsSupersetOf(ByGenre)
                 select game);

        Games = _mapper.Map<List<GameModel>>(games.ToList());
    }

    public void OnPost()
    {
    }
}
