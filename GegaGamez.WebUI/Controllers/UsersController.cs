using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Auth;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    // GET: api/<UserController>
    [HttpGet]
    public IEnumerable<UserModel> Get()
    {
        var users = _userService.GetAll();
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public ActionResult<UserModel> Get(int id)
    {
        var user = _userService.GetById(id);

        return user is null ? NotFound() : _mapper.Map<UserModel>(user);
    }

    [HttpGet("filter")]
    public IEnumerable<UserModel> Find(string username)
    {
        var users = _userService.Find(username);

        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    [HttpPost]
    public ActionResult<UserModel> Post([FromBody] RegisterUserModel newUser)
    {
        if (User.Identity?.IsAuthenticated == true)
            return BadRequest();

        var user = _mapper.Map<User>(newUser);
        _userService.CreateUser(user);
        var createdUser = _mapper.Map<UserModel>(user);
        return createdUser;
    }
}
