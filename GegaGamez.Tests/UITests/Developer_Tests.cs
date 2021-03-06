using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages.Developers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class Developer_Tests
{
    private readonly IndexModel _devPage;
    private readonly Mock<IDeveloperService> _devServiceMock = new();
    private readonly Mock<IGameService> _gameServiceMock = new();

    private readonly Mock<ILogger<IndexModel>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public Developer_Tests()
    {
        _devPage = new(_devServiceMock.Object, _gameServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void DevIsNotNull_ReturnsPage()
    {
        // Arrange
        var id = 1;
        Developer? devToReturn = new() { Id = id };
        _devServiceMock.Setup(ds => ds.GetById(It.IsAny<int>()))
            .Returns(devToReturn)
            .Verifiable();

        // Act
        var getResult = _devPage.OnGet(id);

        // Assert
        _devServiceMock.Verify();
        Assert.IsType<PageResult>(getResult);
    }

    [Fact]
    public void DevIsNull_ReturnsNotFound()
    {
        // Arrange
        var id = 5;
        _devServiceMock.Setup(ds => ds.GetById(It.IsAny<int>()))
            .Returns<Developer?>(null).Verifiable();

        // Act
        var getResult = _devPage.OnGet(id);

        // Assert
        _devServiceMock.Verify();
        Assert.IsType<NotFoundResult>(getResult);
    }
}
