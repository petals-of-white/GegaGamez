using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Users.Collections
{
    public class DefaultModel : PageModel
    {
        private readonly IGameCollectionService _collectionService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private ICollection<DefaultCollectionTypeModel> _defaultCollectionTypes { get; init; }

        public DefaultModel(IMapper mapper, IGameCollectionService collectionService, IGameService gameService, IUserService userService)
        {
            _mapper = mapper;
            _collectionService = collectionService;
            _gameService = gameService;
            _userService = userService;

            var defaultCollectionTypes = _collectionService.GetDefaultCollectionTypes();
            _defaultCollectionTypes = _mapper.Map<ICollection<DefaultCollectionTypeModel>>(defaultCollectionTypes);
        }

        public List<SelectListItem>? AvailableDefaultCollections { get; set; }
        public List<SelectListItem>? AvailableUserCollections { get; set; }

        public DefaultCollectionModel Collection { get; set; }

        public List<GameModel> GamesInCollection { get; set; }

        [BindProperty]
        public GameToDefaultCollectionModel GameToDefaultCollection { get; set; }

        [BindProperty]
        public GameToUserCollectionModel GameToUserCollection { get; set; }

        public IActionResult OnGet(int userId, string defaultCollection)
        {
            DefaultCollectionTypeModel type;

            // check whether default collection with this name exists
            try
            {
                type = _defaultCollectionTypes.Single(dct => dct.Name.ToLower() == defaultCollection.ToLower());
            }
            catch (Exception)
            {
                return NotFound();
            }

            User? userById = _userService.GetById(userId);

            // if user exists?
            if (userById is null)
            {
                return NotFound();
            }
            else
            {
                // load user collections
                //userById.UserCollections = _collectionService.GetUserCollectionsForUser(userById).ToHashSet();
                userById.DefaultCollections = _collectionService.GetDefaultColletionsForUser(userById).ToHashSet();

                var theRightCollection = userById.DefaultCollections.Single(dc => dc.DefaultCollectionType.Id == type.Id);
                Collection = _mapper.Map<DefaultCollectionModel>(theRightCollection);

                // load games for the found collection
                //_collectionService.LoadCollectionGames(theRightCollection);
                theRightCollection.Games = _gameService.GetGamesInCollection(theRightCollection).ToHashSet();
                GamesInCollection = _mapper.Map<List<GameModel>>((HashSet<Game>) theRightCollection.Games);

                return Page();
            }
        }
    }
}
