using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Games;

public class IndexModel : PageModel
{
    private readonly IAuthorizationService _authService;
    private readonly ICommentService _commentService;
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public IndexModel(IGameService games, IRatingService ratingService, ICommentService commentService, IUserService userService, IGenreService genreService, IMapper mapper, IAuthorizationService authService)
    {
        _gameService = games;
        _ratingService = ratingService;
        _commentService = commentService;
        _userService = userService;
        _genreService = genreService;
        _mapper = mapper;
        _authService = authService;
    }

    public GameModel Game { get; set; }
    public RatingModel UserRatingForGame { get; set; }
    public List<GenreModel> GameGenres { get; set; }
    public List<CommentModel> Comments { get; set; } = new();

    [BindProperty]
    public NewCommentModel? NewComment { get; set; }

    public IActionResult OnGet(int id)
    {
        var requestedGame = _gameService.GetById(id);

        if (requestedGame is null)
            return NotFound();
        else
        {
            //_gameService.LoadGameGenres(requestedGame);
            //_gameService.LoadGameComments(requestedGame);
            requestedGame.Genres = _genreService.GetGameGenres(requestedGame).ToHashSet();
            requestedGame.Comments = _commentService.GetGameComments(requestedGame).ToHashSet();

            Game = _mapper.Map<GameModel>(requestedGame);
            GameGenres = _mapper.Map<List<GenreModel>>((HashSet<Genre>) requestedGame.Genres);
            Comments = _mapper.Map<List<CommentModel>>((HashSet<Comment>) requestedGame.Comments);

            AuthDisplayHelper authHelper = new(User);

            int? userId = authHelper.UserId;

            if (userId is not null)
            {
                Rating? userRating = _ratingService.GetUserRating(new User { Id = userId.Value }, requestedGame);
                UserRatingForGame = _mapper.Map<RatingModel>(userRating);
            }

            return Page();
        }
    }

    /*
    public async Task<IActionResult> OnPostEditGameInfo()
    {
        var canEditGames = await _authService.AuthorizeAsync(User, PolicyNames.AdminPolicy);
        if (canEditGames.Succeeded)
        {
            if (ModelState.IsValid)
            {
            }
        }
    }
    */

    public async Task<IActionResult> OnPostComment()
    {
        AuthorizationResult canPostComments = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);

        if (canPostComments.Succeeded)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = _mapper.Map<Comment>(NewComment);
                _commentService.AddComment(newComment);
                //_userService.AddComment(newComment);
            }
            else
                ViewData ["Error"] = "Please check comment requirements";

            return OnGet(NewComment.GameId);
        }
        else
            return Forbid();

        /*
        if (HttpContext.User.Identity?.IsAuthenticated == false)
        {
            return Forbid()("/");
        }
        */
        //else
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Comment newComment = _mapper.Map<Comment>(NewComment);
        //        _userService.AddComment(newComment);
        //    }

        //    return OnGet(NewComment.GameId);
        //}
    }
}
