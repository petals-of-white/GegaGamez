using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenresController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    // GET: api/<GenresController>
    [HttpGet]
    public IEnumerable<GenreModel> Get()
    {
        var genres = _genreService.FindAll();
        return _mapper.Map<IEnumerable<GenreModel>>(genres);
    }

    // GET api/<GenresController>/5
    [HttpGet("{id}")]
    public ActionResult<GenreModel> Get(int id)
    {
        var genre = _genreService.GetById(id);

        return genre is null ? NotFound() : _mapper.Map<GenreModel>(genre);
    }
}
