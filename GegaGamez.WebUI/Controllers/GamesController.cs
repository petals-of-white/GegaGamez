using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GamesController(IGameService gameService, IGenreService genreService, IMapper mapper)
    {
        _gameService = gameService;
        _genreService = genreService;
        _mapper = mapper;
    }

    // GET: api/<GameController>
    [HttpGet]
    public IEnumerable<GameModel> Get()
    {
        var games = _gameService.FindAll();
        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    // GET api/<GameController>/5
    [HttpGet("{id}")]
    public ActionResult<GameModel> Get(int id)
    {
        var game = _gameService.GetById(id);

        return game is null ? NotFound() : _mapper.Map<GameModel>(game);
    }

    // GET: api/<GameController>
    [HttpGet("filter")]
    public IEnumerable<GameModel> Get(string? title, [FromQuery] int[] genreIds)
    {
        var filterGenres = (from genre in genreIds
                           select _genreService.GetById(genre)).Where(g=>g is not null).ToArray();

        var games = _gameService.Find(title, filterGenres);

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }
}
