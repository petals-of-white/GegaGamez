using GegaGamez.BLL.Services;
using GegaGamez.Shared.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;

        public IndexModel(GameService games)
        {
            _gameService = games;
        }

        public Game Game { get; set; }
        public List<Comment> Comments { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var requestedGame = _gameService.GetById(id);

            if (requestedGame is null)
                return NotFound();
            else
            {
                Game = requestedGame;
                return Page();
            }
        }
    }
}
