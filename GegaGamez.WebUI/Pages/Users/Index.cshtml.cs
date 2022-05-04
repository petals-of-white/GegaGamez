using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public IndexModel(IUserService userService, ICountryService countryService, IMapper mapper)
    {
        _userService = userService;
        _countryService = countryService;
        _mapper = mapper;
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

    public IActionResult OnPostUpdateProfile()
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(UpdateModel);
            _userService.UpdateUser(user);
        }
        else
        {
        }

        return RedirectToPage("/Users/Index", new { id = UpdateModel.Id });

    }
}
