using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;

    public CommentsController(IMapper mapper, ICommentService commentService)
    {
        _mapper = mapper;
        _commentService = commentService;
    }

    // GET: api/<CommentsController>
    [HttpGet]
    public IEnumerable<CommentModel> Get()
    {
        var comments = _commentService.FindAll();
        return _mapper.Map<IEnumerable<CommentModel>>(comments);
    }

    // GET api/<CommentsController>/5
    [HttpGet("{id}")]
    public ActionResult<CommentModel> Get(int id)
    {
        var comment = _commentService.GetById(id);
        return comment is null ? NotFound() : _mapper.Map<CommentModel>(comment);
    }

    [HttpGet("filter")]
    public IEnumerable<CommentModel> GetByGame(int gameId)
    {
        var comments = _commentService.GetGameComments(new Game { Id = gameId });
        return _mapper.Map<IEnumerable<CommentModel>>(comments);
    }

    [HttpPost]
    [Authorize]
    public ActionResult<CommentModel> Create([FromBody] NewCommentModel newComment)
    {
        var comment = _mapper.Map<Comment>(newComment);

        _commentService.AddComment(comment);

        var createdComment = _mapper.Map<CommentModel>(comment);

        return createdComment;
    }
}
