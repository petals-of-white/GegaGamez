using AutoMapper;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users.Collections
{
    public class DefaultModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IGameCollectionService _collectionService;
        private readonly IUserService _userService;

        private ICollection<DefaultCollectionTypeModel> _defaultCollectionTypes { get; init; }

        public DefaultModel(IMapper mapper, IGameCollectionService collectionService, IUserService userService)
        {
            _mapper = mapper;
            _collectionService = collectionService;
            _userService = userService;

            var defaultCollectionTypes = _collectionService.GetDefaultCollectionTypes();
            _defaultCollectionTypes = _mapper.Map<ICollection<DefaultCollectionTypeModel>>(defaultCollectionTypes);
        }

        public DefaultCollectionModel Collection { get; set; }
        public List<GameModel> GamesInCollection { get; set; }

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
                _userService.LoadUsersCollections(userById);

                var theRightCollection = userById.DefaultCollections.Single(dc => dc.DefaultCollectionType.Id == type.Id);
                Collection = _mapper.Map<DefaultCollectionModel>(theRightCollection);

                // load games for the found collection
                _collectionService.LoadCollectionGames(theRightCollection);
                GamesInCollection = _mapper.Map<List<GameModel>>(theRightCollection.Games.ToList());

                return Page();
            }
        }
    }
}
