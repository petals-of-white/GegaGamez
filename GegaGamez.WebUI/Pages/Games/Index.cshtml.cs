using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Games;

public class IndexModel : PageModel
{
    private readonly IAuthorizationService _authService;
    private readonly IGameCollectionService _collectionService;
    private readonly ICommentService _commentService;
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMapper _mapper;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;

    private List<SelectListItem> GenerateDefaultCollectionsItems(User user,
                                                                     Game requestedGame)
    {
        var defaultCollections = _collectionService.GetDefaultColletionsForUser(user).ToList();

        foreach (var dc in defaultCollections)
            dc.Games = _gameService.GetGamesInCollection(dc).ToHashSet();

        List<SelectListItem> itemList = defaultCollections
            .Select(dc => new SelectListItem(dc.DefaultCollectionType.Name, dc.Id.ToString()))
            .ToList();

        return itemList;
    }

    private List<SelectListItem> GenerateUserCollectionsItems(User user,
                                                              Game requestedGame)
    {
        var userCollections = _collectionService.GetUserCollectionsForUser(user).ToList();

        foreach (var uc in userCollections)
            uc.Games = _gameService.GetGamesInCollection(uc).ToHashSet();

        List<SelectListItem> itemList = userCollections
            .Select(uc => new SelectListItem(uc.Name, uc.Id.ToString()))
            .ToList();

        return itemList;
    }

    private void LoadGameInfo(Game requestedGame)
    {
        requestedGame.Genres = _genreService.GetGameGenres(requestedGame).ToHashSet();
        requestedGame.Comments = _commentService.GetGameComments(requestedGame).ToHashSet();
    }

    private GameModel MapWithAvgScore(Game requestedGame)
    {
        var game = _mapper.Map<GameModel>(requestedGame);
        game.AvgRatingScore = _ratingService.GetAverageRatingScore(requestedGame);
        return game;
    }

    public IndexModel(IGameService games,
                                          IGameCollectionService collectionService,
                      IRatingService ratingService,
                      ICommentService commentService,
                      IUserService userService,
                      IGenreService genreService,
                      IMapper mapper,
                      IAuthorizationService authService, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _gameService = games;
        _collectionService = collectionService;
        _ratingService = ratingService;
        _commentService = commentService;
        _userService = userService;
        _genreService = genreService;
        _mapper = mapper;
        _authService = authService;
    }

    public List<SelectListItem>? AvailableDefaultCollections { get; set; }

    public List<SelectListItem> AvailableRatingScores { get; set; } = new List<SelectListItem>(10)
    {
        new (){ Value = "1", Text = "Pain (1/10)"},
        new (){ Value = "2", Text = "Terrible (2/10)"},
        new (){ Value = "3", Text = "Not recommended (3/10)"},
        new (){ Value = "4", Text = "Poor (4/10)"},
        new (){ Value = "5", Text = "Acceptable (5/10)"},
        new (){ Value = "6", Text = "Not bad (6/10)"},
        new (){ Value = "7", Text = "Decent (7/10)"},
        new (){ Value = "8", Text = "Good (8/10)"},
        new (){ Value = "9", Text = "Great (9/10)"},
        new (){ Value = "10", Text = "Masterpiece (10/10)"},
    };

    public List<SelectListItem>? AvailableUserCollections { get; set; }
    public List<CommentModel> Comments { get; set; } = new();
    public GameModel Game { get; set; }
    public List<GenreModel> GameGenres { get; set; } = new();

    [BindProperty]
    public GameToDefaultCollectionModel GameToDefaultCollection { get; set; }

    [BindProperty]
    public GameToUserCollectionModel GameToUserCollection { get; set; }

    [BindProperty]
    public NewCommentModel? NewComment { get; set; }

    [BindProperty]
    public UpdateRatingModel UpdateUserRating { get; set; }

    public byte? UserRatingForGame { get; set; }

    public IActionResult OnGet(int id)
    {
        var requestedGame = _gameService.GetById(id);

        if (requestedGame is null)
        {
            _logger.LogInformation($"Game with id {id} was not found");

            return NotFound();
        }
        else
        {
            LoadGameInfo(requestedGame);

            Game = MapWithAvgScore(requestedGame);
            GameGenres = _mapper.Map<List<GenreModel>>(requestedGame.Genres);
            Comments = _mapper.Map<List<CommentModel>>(requestedGame.Comments);

            _logger.LogTrace("Successfully loaded game related information");

            int? userId = User.GetId();

            if (userId is not null)
            {
                _logger.LogDebug($"User is authenticated. Id {userId}");

                User user = new() { Id = userId.Value };

                Rating? userRating = _ratingService.GetUserRating(user, requestedGame);
                UserRatingForGame = userRating?.RatingScore;

                AvailableDefaultCollections = GenerateDefaultCollectionsItems(user,
                                                                              requestedGame);

                AvailableUserCollections = GenerateUserCollectionsItems(user,
                                                                        requestedGame);

                _logger.LogTrace("User-related stuff has been successfully loaded");
            }

            return Page();
        }
    }

    public IActionResult OnPostComment()
    {
        bool isAuthenticated = User.IsAuthenticated();
        if (isAuthenticated)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = _mapper.Map<Comment>(NewComment);
                try
                {
                    _logger.LogTrace("Attempting to post a comment");

                    _commentService.AddComment(newComment);

                    _logger.LogInformation($"Added a new comment: {NewComment}");
                }
                catch (UniqueEntityException ex)
                {
                    _logger.LogWarning(ex, "Failed to post a new comment due to unique constraint. See the exception");

                    return RedirectToPage("/Games/Search");
                }
            }
            else
            {
                _logger.LogInformation($"Comment didn't pass validation. Number of errors {ModelState.ErrorCount}");
                this.ValidationError();
            }

            _logger.LogInformation("Redirecting to the /Games/Index");

            return RedirectToPage(new { id = NewComment!.GameId });
        }
        else
        {
            _logger.LogInformation("User is not authenticated, therefore he/she cannot post any comments");

            return Forbid();
        }
    }

    public IActionResult OnPostDeleteComment(int id)
    {
        bool isAuthenticated = User.IsAuthenticated();

        if (isAuthenticated)
        {
            Comment? actualComment = _commentService.GetById(id);

            if (actualComment is not null)
            {
                bool isCommentOwner = User.GetId() == actualComment.UserId;
                var gameId = actualComment.GameId;

                if (isCommentOwner)
                {
                    try
                    {
                        _commentService.DeleteComment(actualComment);

                        _logger.LogInformation($"Comment with id {id} has been successfully deleted.");
                        this.CommentDeleted();

                        return RedirectToPage(new { id = gameId });
                    }
                    catch (EntityNotFoundException ex)
                    {
                        _logger.LogWarning(ex, $"Could not delete comment with id {id}, most likely because it does not exist");

                        return NotFound();
                    }
                }
                else
                {
                    _logger.LogInformation($"User {actualComment.UserId} tried to delete comment that doesn't belong to them." +
                        $"Access denied.");

                    return Forbid();
                }
            }
            else
            {
                _logger.LogWarning($"Could not delete comment with id {id}, most likely because it does not exist");

                return NotFound();
            }
        }
        else
        {
            _logger.LogInformation($"Impossible to delete the comment because user isn't authenticated.");

            return Unauthorized();
        }
    }

    public IActionResult OnPostDeleteGame(int id)
    {
        bool isAuthenticated = User.IsAuthenticated();
        bool isAdmin = User.IsAdmin();

        if (isAuthenticated)
        {
            if (isAdmin)
            {
                var game = _gameService.GetById(id)!;

                try
                {
                    _gameService.DeleteGame(game);

                    _logger.LogInformation($"Game with id {id} has been successfully deleted.");
                    this.GameDeleted(game.Title);

                    return RedirectToPage("/Games/Search");
                }
                catch (EntityNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Requested game wasn't found, therefore can not be deleted.");

                    return NotFound();
                }
            }
            else
            {
                _logger.LogInformation($"User with id {User.GetId()} does not have admin rights to delete games.");

                return Forbid();
            }
        }
        else
        {
            _logger.LogInformation($"Impossible to delete the game because user isn't authenticated.");

            return Unauthorized();
        }
    }

    public IActionResult OnPostMoveGameToCollection()
    {
        if (User.IsAuthenticated() == false)
            return Unauthorized();

        // Validation
        this.ValidateOnly(new string [] { nameof(GameToUserCollection), nameof(GameToDefaultCollection) });
        if (ModelState.IsValid == false)
        {
            _logger.LogInformation($"GameToUserCollection or GameToDefaultCollection didn't pass validation. Number of errors {ModelState.ErrorCount}");
            this.ValidationError();

            return BadRequest();
        }

        // Check whether collections owner is the current user
        UserCollection? wantedUserCollection = _collectionService
            .GetUserCollectionById(GameToUserCollection.CollectionId);

        DefaultCollection? wantedDefaultCollection = _collectionService
            .GetDefaultCollectionById(GameToDefaultCollection.CollectionId);

        int? userId = User.GetId();

        int gameId = GameToDefaultCollection.GameId;
        var gameToAdd = new Game { Id = gameId };

        // Default Collection First
        if (wantedDefaultCollection is not null)
        {
            if (wantedDefaultCollection.UserId != userId)
            {
                _logger.LogInformation($"User {userId} isn't the owner of default collection {wantedDefaultCollection.Id}.");
                return Forbid();
            }    
            try
            {
                _collectionService.AddGame(wantedDefaultCollection, gameToAdd);
                _logger.LogInformation($"Game {gameToAdd.Id} added to default collection {wantedDefaultCollection.Id}");
                this.GameAdded(gameToAdd.Title, wantedDefaultCollection.DefaultCollectionType.Name);
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Either collection or game wasn't found. See the exception.");
            }
            catch (UniqueEntityException ex)
            {
                _logger.LogWarning(ex, "Attempting to add an existing game to collection. No changes.");
                _logger.LogInformation("Redirecting...");
                TempData [Messages.InfoKey] = $"Game {gameToAdd.Title} is already in {wantedDefaultCollection!.DefaultCollectionType.Name}";

                return RedirectToPage(new { id = gameId });
            }
        }


        // User collection
        if (wantedUserCollection is not null)
        {
            if (wantedUserCollection.UserId != userId)
            {
                _logger.LogInformation($"User {userId} isn't the owner of user collection {wantedUserCollection.Id}.");
                return Forbid();
            }
            try
            {
                _collectionService.AddGame(wantedUserCollection, gameToAdd);
                _logger.LogInformation($"Game {gameToAdd.Id} added to user collection {wantedUserCollection.Id}.");

            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, "Either collection or game wasn't found. See the exception.");
            }
            catch (UniqueEntityException ex)
            {
                _logger.LogWarning(ex, "Attempting to add an existing game to collection. No changes.");
                _logger.LogInformation("Redirecting...");
                TempData [Messages.InfoKey] = $"Game {gameToAdd.Title} is already in {wantedUserCollection.Name}.";
            }

        }

        _logger.LogInformation("Successfully added games to collections. Redirecting...");

        return RedirectToPage(new { id = gameId });
    }

    public IActionResult OnPostRateGame()
    {
        //AuthorizationResult canRateGames = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);
        bool isAuthenticated = User.IsAuthenticated();

        if (isAuthenticated)
        {
            foreach (var entry in ModelState)
            {
                if (entry.Key.StartsWith(nameof(UpdateUserRating)) == false) ModelState.Remove(entry.Key);
            }

            if (ModelState.IsValid)
            {
                var rating = _mapper.Map<Rating>(UpdateUserRating);

                try
                {
                    _ratingService.RateGame(rating);
                    _logger.LogInformation($"Rating added/updated: {UpdateUserRating}");
                }
                catch (EntityNotFoundException ex)
                {
                    _logger.LogWarning(ex, $"Can not give rating because game or user does not exist");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, $"An error occured: {UpdateUserRating}");
                }
            }
            else
            {
                _logger.LogDebug($"Validation Errors: {ModelState.Count}");
                this.ValidationError();
            }

            return RedirectToPage(new { id = UpdateUserRating.GameId });
        }
        else
        {
            _logger.LogInformation("User isn't authenticated, therefore cannot rate games.");

            return Unauthorized();
        }
    }
}
