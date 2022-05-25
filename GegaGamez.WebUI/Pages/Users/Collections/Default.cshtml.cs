using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users.Collections
{
    public class DefaultModel : PageModel
    {
        private readonly IGameCollectionService _collectionService;
        private readonly IGameService _gameService;
        private readonly ILogger<DefaultModel> _logger;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;
        private readonly IUserService _userService;

        private ICollection<DefaultCollectionTypeModel> _defaultCollectionTypes;

        private List<GameModel> GetGamesAndCalculateScore(DefaultCollection collection)
        {
            var games = _mapper.Map<List<GameModel>>(collection.Games);

            var gamesList = collection.Games.ToList();

            for (int i = 0; i < gamesList.Count; i++)
            {
                games [i].AvgRatingScore = _ratingService.GetAverageRatingScore(gamesList [i]);
            }

            return games;
        }

        private void LoadDefaultCollectionTypes()
        {
            var defaultCollectionTypes = _collectionService.GetDefaultCollectionTypes();
            _defaultCollectionTypes = _mapper.Map<ICollection<DefaultCollectionTypeModel>>(defaultCollectionTypes);
        }

        private DefaultCollection LoadFullCollectionInfo(User user, DefaultCollectionType type)
        {
            var dc = _collectionService.GetDefaultColletionsForUser(user)
                .Single(dc => dc.DefaultCollectionType.Id == type.Id);

            dc.Games = _gameService.GetGamesInCollection(dc).ToHashSet();

            return dc;
        }

        public DefaultModel(ILogger<DefaultModel> logger,
                                            IRatingService ratingService,
                            IMapper mapper,
                            IGameCollectionService collectionService,
                            IGameService gameService,
                            IUserService userService)
        {
            _logger = logger;
            _ratingService = ratingService;
            _mapper = mapper;
            _collectionService = collectionService;
            _gameService = gameService;
            _userService = userService;
        }

        public DefaultCollectionModel Collection { get; set; }

        public List<GameModel> GamesInCollection { get; set; }

        public IActionResult OnGet(int userId, string defaultCollection)
        {
            DefaultCollectionTypeModel type;

            LoadDefaultCollectionTypes();

            try
            {
                type = _defaultCollectionTypes.Single(dct => dct.Name.ToLower() == defaultCollection.ToLower());
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Could not find a default collection named {defaultCollection}.");
                return NotFound();
            }

            User? userById = _userService.GetById(userId);

            if (userById is not null)
            {
                DefaultCollection rightCollection = LoadFullCollectionInfo(
                    userById,
                    new DefaultCollectionType { Id = type.Id });

                Collection = _mapper.Map<DefaultCollectionModel>(rightCollection);
                GamesInCollection = GetGamesAndCalculateScore(rightCollection);

                return Page();
            }
            else
            {
                _logger.LogInformation($"User with id {userId} does not exist");
                return NotFound();
            }
        }

        public IActionResult OnPostRemoveGame(int gameId, int collectionId)
        {
            //var game = new Game { Id = gameId };


            bool isAuthenticated = User.IsAuthenticated();

            if (isAuthenticated)
            {
                Game? game = _gameService.GetById(gameId);
                DefaultCollection? dc = _collectionService.GetDefaultCollectionById(collectionId);

                if (dc is not null)
                {
                    bool isOwner = User.GetId() == dc.UserId;
                    if (isOwner)
                    {
                        try
                        {
                            _collectionService.RemoveGame(dc, game);

                            _logger.LogInformation($"Game {gameId} has been removed from default collection {collectionId}.");
                            this.GameRemoved(game.Title, dc.DefaultCollectionType.Name);

                            return RedirectToPage(new { userId = dc.UserId, defaultCollection = dc.DefaultCollectionType.Name });
                        }
                        catch (EntityNotFoundException ex)
                        {
                            _logger.LogWarning(ex, $"Something was not found. See the exception.");
                            return NotFound();
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"User {User.GetId()} is not the owner of this collection");
                        return Forbid();
                    }
                }
                else
                {
                    _logger.LogWarning($"Default collection with id {collectionId} was not found.");
                    return NotFound();
                }
            }
            else
            {
                _logger.LogInformation("User is not authenticated");
                return Unauthorized();
            }
        }
    }
}
