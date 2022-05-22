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
    private readonly IRatingService _ratingService;

    public SearchModel(IGameService gameService, IGenreService genreService, IMapper mapper, IRatingService ratingService)
    {
        _gameService = gameService;
        _genreService = genreService;
        _mapper = mapper;
        _ratingService = ratingService;
    }

    public List<GenreModel> AvailableGenres { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public HashSet<int> ByGenre { get; set; } = new();

    public List<GameModel> Games { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? GameTitle { get; set; }

    public void OnGet()
    {
        // Get all the genres
        var genres = _genreService.FindAll().ToList();
        AvailableGenres = _mapper.Map<List<GenreModel>>(genres);

        // display all the games if no search string
        IEnumerable<Game> games;

        if (string.IsNullOrWhiteSpace(GameTitle) && ByGenre.Count == 0)
            games = _gameService.FindAll();
        else
            games = _gameService.Find(GameTitle, ByGenre.Select(gId => new Genre { Id = gId }).ToArray());

        List<byte?> avgScores = new() { };
        foreach (var game in games)
        {
            game.Genres = _genreService.GetGameGenres(game).ToHashSet();
            avgScores.Add(_ratingService.GetAverageRatingScore(game));
        }

        Games = _mapper.Map<List<GameModel>>(games);

        for (var i = 0; i< avgScores.Count; i++)
        {
            Games [i].AvgRatingScore = avgScores [i];
        }

    }

    public void OnPost()
    {
    }
}
