using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class User_Tests
{
    private readonly Mock<IAuthorizationService> _authServiceMock = new();
    private readonly Mock<IGameCollectionService> _collectionServiceMock = new();
    private readonly Mock<ICountryService> _countryServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly IndexModel _userPage;
    private readonly Mock<IUserService> _userServiceMock = new();

    public User_Tests()
    {
        _userPage = new(
            _userServiceMock.Object,
            _authServiceMock.Object,
            _collectionServiceMock.Object,
            _countryServiceMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task AuthroziedUser_ReturnsPageResult()
    {
        // Arrange
        _authServiceMock.Setup(aus => aus.AuthorizeAsync(
            It.IsAny<ClaimsPrincipal>(),
            It.IsAny<object?>(),
            It.IsAny<string>()
            ).Result)
            .Returns(AuthorizationResult.Success())
            .Verifiable();

        // Act
        var postResult = await _userPage.OnPostUpdateProfile();

        // Assert
        _authServiceMock.Verify();
        Assert.IsType<PageResult>(postResult);
    }

    [Fact]
    public async Task UnauthroziedUser_ReturnsUnauthorizedResult()
    {
        /*(auS => auS.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<string>()).Result)*/

        // Arrange
        _authServiceMock.Setup(aus => aus.AuthorizeAsync(
            It.IsAny<ClaimsPrincipal>(),
            It.IsAny<object?>(),
            It.IsAny<string>()
            ).Result)
            .Returns(AuthorizationResult.Failed()).Verifiable();

        // Act
        var postResult = await _userPage.OnPostUpdateProfile();

        // Assert
        _authServiceMock.Verify();
        Assert.IsType<UnauthorizedResult>(postResult);
    }
}
