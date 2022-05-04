using AutoMapper;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users.Collections
{
    public class CustomModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IGameCollectionService _collectionService;
        private readonly IUserService _userService;

        public CustomModel(IMapper mapper, IGameCollectionService collectionService, IUserService userService)
        {
            _mapper = mapper;
            _collectionService = collectionService;
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
                _collectionService.LoadCollectionGames(collection);

                Collection = _mapper.Map<UserCollectionModel>(collection);
                GamesInCollection = _mapper.Map<List<GameModel>>(collection.Games.ToList());

                return Page();
            }
        }
    }
}
