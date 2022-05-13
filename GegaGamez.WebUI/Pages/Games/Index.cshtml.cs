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
    private readonly IGameService _gameService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authService;

    public IndexModel(IGameService games, IUserService userService, IMapper mapper, IAuthorizationService authService)
    {
        _gameService = games;
        _userService = userService;
        _mapper = mapper;
        _authService = authService;

        //NewComment = new() { Game = new GameModel { }, User = new UserModel { }, Text = String.Empty };
    }

    public GameModel Game { get; set; }
    public RatingModel UserRatingForGame { get; set; }
    public List<GenreModel> GameGenres { get; set; }

    public List<CommentModel> Comments { get; set; } = new();

    [BindProperty]
    public NewCommentModel NewComment { get; set; }

    public IActionResult OnGet(int id)
    {
        var requestedGame = _gameService.GetById(id);

        if (requestedGame is null)
            return NotFound();
        else
        {
            _gameService.LoadGameGenres(requestedGame);
            _gameService.LoadGameComments(requestedGame);

            Game = _mapper.Map<GameModel>(requestedGame);
            GameGenres = _mapper.Map<List<GenreModel>>(requestedGame.Genres.ToList());
            Comments = _mapper.Map<List<CommentModel>>(requestedGame.Comments.ToList());

            AuthDisplayHelper authHelper = new(User);

            int? userId = authHelper.UserId;

            if (userId is not null)
            {
                UserRatingForGame = _mapper.Map<RatingModel>(_userService.GetRatingForGame(
                    new User { Id = userId.Value },
                    requestedGame));
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
        var canPostComments = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);

        if (canPostComments.Succeeded)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = _mapper.Map<Comment>(NewComment);
                _userService.AddComment(newComment);
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
