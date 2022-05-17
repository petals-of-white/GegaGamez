using System.Security.Claims;
using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class Login_Tests
{
    private readonly Mock<IAuthManager> _authManagerMock = new();
    private readonly LoginModel _loginPage;
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<ClaimsPrincipal> _userMock = new();

    public Login_Tests()
    {
        _loginPage = new(_userServiceMock.Object, _authManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void LoginAttempt_WhileAuthenticated_ReturnsForbid()
    {
        // Arrange
        _userMock.SetupGet(u => u.Identity!.IsAuthenticated).Returns(true);
        DefaultHttpContext httpContext = new() { User = _userMock.Object };

        IModelMetadataProvider modelMetadataProvider = new EmptyModelMetadataProvider();
        ModelStateDictionary modelState = new();
        ViewDataDictionary viewData = new(modelMetadataProvider, modelState);

        ActionContext actionContext = new(httpContext, new RouteData(), new PageActionDescriptor());

        PageContext pageContext = new(actionContext) { ViewData = viewData };

        _loginPage.PageContext = pageContext;

        // Act
        var getResult = _loginPage.OnGet();

        // Asssert
        Assert.IsType<ForbidResult>(getResult);
    }

    [Fact]
    public void LoginAttempt_WhileAnonymous_ReturnsPage()
    {
        // Arrange
        _userMock.SetupGet(u => u.Identity!.IsAuthenticated).Returns(false); //!!!

        DefaultHttpContext httpContext = new() { User = _userMock.Object };

        IModelMetadataProvider modelMetadataProvider = new EmptyModelMetadataProvider();
        ModelStateDictionary modelState = new();
        ViewDataDictionary viewData = new(modelMetadataProvider, modelState);

        ActionContext actionContext = new(httpContext, new RouteData(), new PageActionDescriptor());

        PageContext pageContext = new(actionContext) { ViewData = viewData };

        _loginPage.PageContext = pageContext;

        // Act
        var getResult = _loginPage.OnGet();

        // Asssert
        Assert.IsType<PageResult>(getResult);
    }
}
