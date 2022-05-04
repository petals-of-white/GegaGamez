using AutoMapper;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Games;

public class IndexModel : PageModel
{
    private readonly IGameService _gameService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public IndexModel(IGameService games, IUserService userService, IMapper mapper)
    {
        _gameService = games;
        _userService = userService;
        _mapper = mapper;

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

    public IActionResult OnPostComment()
    {
        if (HttpContext.User.Identity?.IsAuthenticated == false)
        {
            return RedirectToPage("/");
        }
        else
        {
            if (ModelState.IsValid)
            {
                Comment newComment = _mapper.Map<Comment>(NewComment);
                _userService.AddComment(newComment);
            }

            return OnGet(NewComment.GameId);
        }
    }
}
