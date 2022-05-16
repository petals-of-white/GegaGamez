using System;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.DataAccess;
using Moq;
using Xunit;

namespace GegaGamez.Tests.ServicesTests;

public class DeveloperService_Tests : IDisposable
{
    private readonly DeveloperService _devService;
    private readonly Mock<IUnitOfWork> _uowMock = new();

    public DeveloperService_Tests()
    {
        _devService = new(_uowMock.Object);
    }

    [Fact]
    public void Constructor_ShouldThrowNullReferenceException()
    {
        DeveloperService serviceWithNullUoW;
        Assert.Throws<ArgumentNullException>(() => serviceWithNullUoW = new(null));
    }

    public void Dispose() => _devService.Dispose();
}
