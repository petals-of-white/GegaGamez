using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GegaGamez.WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public CountriesController(ICountryService countryService, IMapper mapper)
    {
        _countryService = countryService;
        _mapper = mapper;
    }

    // GET: api/<CountriesController>
    [HttpGet]
    public IEnumerable<CountryModel> Get()
    {
        var countries = _countryService.FindAll();
        return _mapper.Map<IEnumerable<CountryModel>>(countries);
    }

    // GET api/<CountriesController>/5
    [HttpGet("{id}")]
    public ActionResult<CountryModel> Get(int id)
    {
        var country = _countryService.GetById(id);

        return country is null ? NotFound() : _mapper.Map<CountryModel>(country);
    }
}
