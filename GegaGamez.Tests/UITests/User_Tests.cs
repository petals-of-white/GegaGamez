using System.Security.Claims;
using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class User_Tests
{
    private readonly Mock<IAuthorizationService> _authServiceMock = new();
    private readonly Mock<IGameCollectionService> _collectionServiceMock = new();
    private readonly Mock<ICountryService> _countryServiceMock = new();
    private readonly Mock<ILogger<IndexModel>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly IndexModel _userPage;
    private readonly Mock<IUserService> _userServiceMock = new();

    public User_Tests()
    {
        _userPage = new(_loggerMock.Object,
                        _userServiceMock.Object,
                        _authServiceMock.Object,
                        _collectionServiceMock.Object,
                        _countryServiceMock.Object,
                        _mapperMock.Object);
    }

    [Fact]
    public void AuthroziedUser_ReturnsPageResult()
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
        var postResult = _userPage.OnPostUpdateProfile();

        // Assert
        _authServiceMock.Verify();
        Assert.IsType<PageResult>(postResult);
    }

    [Fact]
    public void UnauthroziedUser_ReturnsUnauthorizedResult()
    {
        // Arrange
        _authServiceMock.Setup(aus => aus.AuthorizeAsync(
            It.IsAny<ClaimsPrincipal>(),
            It.IsAny<object?>(),
            It.IsAny<string>()
            ).Result)
            .Returns(AuthorizationResult.Failed()).Verifiable();

        // Act
        var postResult = _userPage.OnPostUpdateProfile();

        // Assert
        _authServiceMock.Verify();
        Assert.IsType<UnauthorizedResult>(postResult);
    }
}
