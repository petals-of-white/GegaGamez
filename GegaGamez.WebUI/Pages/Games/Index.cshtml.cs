using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
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
    //public RatingModel? UserRatingForGame { get; set; }

    public IActionResult OnGet(int id)
    {
        var requestedGame = _gameService.GetById(id);

        if (requestedGame is null)
            return NotFound();
        else
        {
            requestedGame.Genres = _genreService.GetGameGenres(requestedGame).ToHashSet();
            requestedGame.Comments = _commentService.GetGameComments(requestedGame).ToHashSet();

            Game = _mapper.Map<GameModel>(requestedGame);
            Game.AvgRatingScore = _ratingService.GetAverageRatingScore(requestedGame);
            GameGenres = _mapper.Map<List<GenreModel>>((HashSet<Genre>) requestedGame.Genres);
            Comments = _mapper.Map<List<CommentModel>>((HashSet<Comment>) requestedGame.Comments);

            AuthDisplayHelper authHelper = new(User);

            int? userId = authHelper.UserId;

            if (userId is not null)
            {
                User user = new() { Id = userId.Value };
                Rating? userRating = _ratingService.GetUserRating(user, requestedGame);
                //UserRatingForGame = _mapper.Map<RatingModel>(userRating);
                UserRatingForGame = userRating?.RatingScore;
                var defaultCollectionsForUser = _collectionService.GetDefaultColletionsForUser(user);
                var userCollectionsForUser = _collectionService.GetUserCollectionsForUser(user);

                AvailableDefaultCollections = new(defaultCollectionsForUser.Select(dc =>
                {
                    return new SelectListItem(dc.DefaultCollectionType.Name, dc.Id.ToString());
                }));

                AvailableUserCollections = new(userCollectionsForUser.Select(uc =>
                {
                    return new SelectListItem(uc.Name, uc.Id.ToString());
                }));
            }

            return Page();
        }
    }

    public async Task<IActionResult> OnPostCommentAsync()
    {
        AuthorizationResult canPostComments = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);

        if (canPostComments.Succeeded)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = _mapper.Map<Comment>(NewComment);
                _commentService.AddComment(newComment);
            }
            else
                ViewData ["Error"] = "Please check comment requirements";

            return OnGet(NewComment!.GameId);
        }
        else
            return Forbid();
    }

    public IActionResult OnPostMoveGameToCollection()
    {
        if (GameToDefaultCollection.GameId != GameToUserCollection.GameId)
        {
            return BadRequest();
        }
        else
        {
            var gameId = GameToDefaultCollection.GameId;
            var gameToAdd = new Game { Id = gameId };
            var wantedUserCollection = new UserCollection { Id = GameToUserCollection.CollectionId };
            var wantedDefaultCollection = new DefaultCollection { Id = GameToDefaultCollection.CollectionId };
            try
            {
                _collectionService.AddGame(wantedDefaultCollection, gameToAdd);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Something is wrong");
            }
            try
            {
                _collectionService.AddGame(wantedUserCollection, gameToAdd);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Something is wrong");
            }

            return Page();
        }
    }

    public async Task<IActionResult> OnPostRateGameAsync()
    {
        AuthorizationResult canRateGames = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);

        if (canRateGames.Succeeded)
        {
            foreach(var entry in ModelState)
            {
                if (entry.Key.StartsWith(nameof(UpdateUserRating)) == false) ModelState.Remove(entry.Key);
            }
            if (ModelState.IsValid)
            {
                var rating = _mapper.Map<Rating>(UpdateUserRating);
                _ratingService.RateGame(rating);
            }
            else
                ViewData ["Error"] = "Wrong rate input data";

            return RedirectToPage( new { id = UpdateUserRating.GameId });
        }
        else
            return Forbid();
    }
}
