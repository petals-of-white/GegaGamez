using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users;

public class SearchModel : PageModel
{
    private readonly IMapper _mapper;
    private readonly ILogger<SearchModel> _logger;
    private readonly IUserService _userService;

    public SearchModel(IUserService users, IMapper mapper, ILogger<SearchModel> logger)
    {
        _userService = users;
        _mapper = mapper;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public string? UsernameSearchString { get; set; }

    public List<UserModel> Users { get; set; } = new();

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(UsernameSearchString))
        {
            _logger.LogTrace("Showing all the users");

            var users = _userService.GetAll();
            Users = _mapper.Map<List<UserModel>>(users.ToList());
        }
        else
        {
            _logger.LogTrace("Filtering users");

            var users = _userService.Find(UsernameSearchString);
            Users = _mapper.Map<List<UserModel>>(users.ToList());
        }

        _logger.LogInformation($"User search gave {Users.Count} results");

    }
}
