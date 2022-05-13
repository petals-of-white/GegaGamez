using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authService;

    public IndexModel(IUserService userService, IAuthorizationService authService, ICountryService countryService, IMapper mapper)
    {
        _userService = userService;
        _countryService = countryService;
        _mapper = mapper;
        _authService = authService;
    }

    public UserModel UserProfile { get; set; }

    public List<DefaultCollectionModel> DefaultCollections { get; set; }
    public List<UserCollectionModel> UserCollections { get; set; } = new();

    [BindProperty]
    public UpdateProfileModel UpdateModel { get; set; }

    public ICollection<SelectListItem> Countries { get; set; }

    public IActionResult OnGet(int id)
    {
        // get user
        var user = _userService.GetById(id);
        //UserProfile = _userService.GetById(id);

        if (user is null)
            return NotFound();
        else
        {
            // get collections
            _userService.LoadUsersCollections(user);
            UserProfile = _mapper.Map<UserModel>(user);
            DefaultCollections = _mapper.Map<List<DefaultCollectionModel>>(user.DefaultCollections.ToList());

            //load countries
            var countries = _countryService.FindAll();

            Countries = (from country in countries
                         select new SelectListItem(country.Name, country.Id.ToString(), false, false)).ToArray();

            return Page();
        }
    }

    public async Task<IActionResult> OnPostUpdateProfile()
    {
        var canUpdateProfile = await _authService.AuthorizeAsync(User, PolicyNames.UserPolicy);
        if (canUpdateProfile.Succeeded)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(UpdateModel);
                _userService.UpdateUser(user);
            }
            else
                ViewData ["Error"] = "Wrong update info. Please try again";

            return OnGet(UpdateModel.Id);
        }
        else
            return Unauthorized();

        //return RedirectToPage("/Users/Index", new { id = UpdateModel.Id });
    }
}
