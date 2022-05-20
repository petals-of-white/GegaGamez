using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CollectionsController : ControllerBase
{
    private readonly IGameCollectionService _collectionService;
    private readonly IMapper _mapper;

    public CollectionsController(IMapper mapper, IGameCollectionService collectionService)
    {
        _mapper = mapper;
        _collectionService = collectionService;
    }

    [HttpPost("custom")]
    public ActionResult<UserCollectionModel> CreateUserCollection([FromBody] UserCollectionModel newUserCollection)
    {
        var uc = _mapper.Map<UserCollection>(newUserCollection);
        _collectionService.CreateUserCollection(uc);
        var createdCollection = _mapper.Map<UserCollectionModel>(uc);
        return createdCollection;
    }

    [HttpGet("default/{id}")]
    public ActionResult<DefaultCollectionModel> GetDefaultCollection(int id)
    {
        var uc = _collectionService.GetDefaultCollectionById(id);
        return uc is null ? NotFound() : _mapper.Map<DefaultCollectionModel>(uc);
    }

    [HttpGet("custom/{id}")]
    public ActionResult<UserCollectionModel> GetUserCollection(int id)
    {
        var dc = _collectionService.GetUserCollectionById(id);
        return dc is null ? NotFound() : _mapper.Map<UserCollectionModel>(dc);
    }
}
