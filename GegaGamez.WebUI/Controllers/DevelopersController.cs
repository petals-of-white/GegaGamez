using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevelopersController : ControllerBase
{
    private readonly IDeveloperService _devService;
    private readonly IMapper _mapper;

    public DevelopersController(IDeveloperService devService, IMapper mapper)
    {
        _devService = devService;
        _mapper = mapper;
    }

    // GET: api/<DeveloperController>
    [HttpGet]
    public IEnumerable<DeveloperModel> Get()
    {
        var devs = _devService.FindAll();
        return _mapper.Map<IEnumerable<DeveloperModel>>(devs);
    }

    // GET api/<DeveloperController>/5
    [HttpGet("{id}")]
    public ActionResult<DeveloperModel> Get(int id)
    {
        var dev = _devService.GetById(id);
        return dev is null ? NotFound() : _mapper.Map<DeveloperModel>(dev);
    }

    [HttpGet("filter")]
    public IEnumerable<DeveloperModel> Find(string byName)
    {
        var devs = _devService.Find(byName);
        return _mapper.Map<IEnumerable<DeveloperModel>>(devs);
    }
}
