using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Games
{
    public class SearchModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly GenreService _genreService;

        public SearchModel(GameService gameService, GenreService genreService)
        {
            _gameService = gameService;
            _genreService = genreService;
        }

        [BindProperty(SupportsGet = true)]
        public string? GameTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public HashSet<int> ByGenre { get; set; } = new();

        public List<Genre> Genres { get; set; } = new();
        public List<Game> Games { get; set; } = new();

        public void OnGet()
        {
            // Get all the genres
            Genres = _genreService.GetAll().ToList();

            //Games = string.IsNullOrEmpty(GameTitle) ? Games : _games.FindByTitle(GameTitle).ToList();

            // display all the games if no search string
            if (string.IsNullOrWhiteSpace(GameTitle))
            {
                Games = _gameService.GetAll().ToList();
            }
            else
            {
                Games = _gameService.FindByTitle(GameTitle).ToList();
            }

            // fill genres for each game
            foreach (var game in Games)
            {
                _gameService.LoadGameGenres(game);
            }

            // filter games by genre
            Games = (from game in Games
                     where game.Genres.Select(genre => genre.Id).ToHashSet().IsSupersetOf(ByGenre)
                     select game).ToList();
        }

        public void OnPost()
        {
        }
    }
}
