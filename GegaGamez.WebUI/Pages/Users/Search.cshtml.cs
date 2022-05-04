using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.Pages.Users;

public class SearchModel : PageModel
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public SearchModel(IUserService users, IMapper mapper)
    {
        _userService = users;
        _mapper = mapper;
    }

    [BindProperty(SupportsGet = true)]
    public string? UsernameSearchString { get; set; }

    public List<UserModel> Users { get; set; } = new();

    public void OnGet()
    {
        //if ()
        //Users = string.IsNullOrEmpty(UsernameSearchString) ? Users : _users
        //    .FindByUsername(UsernameSearchString).ToList();

        if (string.IsNullOrWhiteSpace(UsernameSearchString))
        {
            var users = _userService.GetAll();
            Users = _mapper.Map<List<UserModel>>(users.ToList());
        }
        else
        {
            var users = _userService.FindByUsername(UsernameSearchString);
            Users = _mapper.Map<List<UserModel>>(users.ToList());
        }
    }
}
