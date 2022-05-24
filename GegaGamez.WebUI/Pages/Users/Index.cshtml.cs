using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Models.ModifyModels;
using GegaGamez.WebUI.PageHelpers;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GegaGamez.WebUI.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IAuthorizationService _authService;
    private readonly IGameCollectionService _collectionService;
    private readonly ICountryService _countryService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public IndexModel(ILogger<IndexModel> logger, IUserService userService, IAuthorizationService authService, IGameCollectionService collectionService, ICountryService countryService, IMapper mapper)
    {
        _logger = logger;
        _userService = userService;
        _countryService = countryService;
        _mapper = mapper;
        _authService = authService;
        _collectionService = collectionService;
    }

    public ICollection<SelectListItem> Countries { get; set; }
    public List<DefaultCollectionModel> DefaultCollections { get; set; }

    [BindProperty]
    public UpdateProfileModel UpdatedUserProfile { get; set; }

    public List<UserCollectionModel> UserCollections { get; set; } = new();
    public UserModel UserProfile { get; set; }

    public IActionResult OnGet(int id)
    {
        // get user
        var user = _userService.GetById(id);

        if (user is null)
        {
            _logger.LogInformation($"User with id {id} was not found");

            return NotFound();
        }
        else
        {
            user.DefaultCollections = _collectionService.GetDefaultColletionsForUser(user).ToHashSet();
            user.UserCollections = _collectionService.GetUserCollectionsForUser(user).ToHashSet();

            UserProfile = _mapper.Map<UserModel>(user);
            DefaultCollections = _mapper.Map<List<DefaultCollectionModel>>((HashSet<DefaultCollection>) user.DefaultCollections);
            UserCollections = _mapper.Map<List<UserCollectionModel>>((HashSet<UserCollection>) user.UserCollections);

            //load countries
            var countries = _countryService.FindAll();

            Countries = (from country in countries
                         select new SelectListItem(country.Name, country.Id.ToString(), false, false)).ToArray();

            _logger.LogInformation($"Loading users {id} page.");

            return Page();
        }
    }

    public async Task<IActionResult> OnPostDeleteAccountAsync(int userId)
    {
        var isAuthenticated = User.IsAuthenticated();
        bool areTheSameUser = User.GetId() == userId;

        if (isAuthenticated)
        {
            if (areTheSameUser)
            {
                var userToDelete = new User { Id = userId };

                try
                {
                    _userService.DeleteUser(userToDelete);

                    await HttpContext.SignOutAsync();

                    _logger.LogInformation($"Signed out a user with id {userId}");
                    TempData [Messages.InfoKey] = "Your account has been deleted. Thank you for your time.";

                    return RedirectToPage("/Games/Search");
                }
                catch (EntityNotFoundException ex)
                {
                    _logger.LogWarning(ex, $"User {userId} wasn't found. Can not delete this account.");

                    return NotFound();
                }
            }
            else
            {
                _logger.LogInformation($"User with {User.GetId()} is not the owner of this page.");

                return Forbid();
            }
        }
        else
        {
            _logger.LogInformation("User is not authorized, therefore he/she cannot delete this account.");

            return Unauthorized();
        }
    }

    public IActionResult OnPostUpdateProfile()
    {
        var isAuthenticated = User.IsAuthenticated();
        bool areTheSameUser = User.GetId() == UpdatedUserProfile.Id;

        if (isAuthenticated && areTheSameUser)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(UpdatedUserProfile);

                try
                {
                    _userService.UpdateUser(user);
                    _logger.LogInformation($"Successfully update user profile. New user data: {UpdatedUserProfile}.");
                    TempData [Messages.InfoKey] = "Your profile has been updated.";
                }
                catch (EntityNotFoundException ex)
                {
                    _logger.LogWarning(ex, $"An error occured while trying to update the user {user.Id}. See the inner exception.");
                    TempData [Messages.InfoKey] = "An error has occured while trying to update profile.";

                    return NotFound();
                }
                catch (UniqueEntityException ex)
                {
                    _logger.LogWarning(ex, $"An error occured while trying to update the user {user.Id}. See the inner exception.");
                    TempData [Messages.InfoKey] = "An error has occured while trying to update profile.";
                }
            }
            else
            {
                _logger.LogDebug($"Validation errors: {ModelState.ErrorCount}");
                TempData [Messages.InfoKey] = "Wrong update info. Please try again";
            }

            return RedirectToPage(new { id = UpdatedUserProfile.Id });
        }
        else
        {
            _logger.LogInformation("User is not authorized, therefore he/she cannot update this account.");

            return Unauthorized();
        }
    }
}
