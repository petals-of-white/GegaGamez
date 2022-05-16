using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users.Collections
{
    public class CustomModel : PageModel
    {
        private readonly IGameCollectionService _collectionService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CustomModel(IMapper mapper, IGameCollectionService collectionService, IGameService gameService, IUserService userService)
        {
            _mapper = mapper;
            _collectionService = collectionService;
            _gameService = gameService;
            _userService = userService;
        }

        public UserCollectionModel Collection { get; set; }
        public List<GameModel> GamesInCollection { get; set; }

        public IActionResult OnGet(int userId, int collectionId)
        {
            User? user;
            UserCollection? collection;

            collection = _collectionService.GetUserCollectionById(collectionId);

            if (collection is null || collection.User.Id != userId)
            {
                return NotFound();
            }
            else
            {
                collection.Games = _gameService.GetGamesInCollection(collection).ToHashSet();
                //_collectionService.LoadCollectionGames(collection);

                Collection = _mapper.Map<UserCollectionModel>(collection);

                GamesInCollection = _mapper.Map<List<GameModel>>((HashSet<Game>)collection.Games);

                return Page();
            }
        }
    }
}
