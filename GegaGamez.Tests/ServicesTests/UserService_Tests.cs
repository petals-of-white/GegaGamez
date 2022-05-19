using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using Moq;
using Xunit;

namespace GegaGamez.Tests.ServicesTests;

public class UserService_Tests : IDisposable
{
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly UserService _userService;

    public UserService_Tests()
    {
        _userService = new(_uowMock.Object);
    }

    [Fact]
    public void Constructor_ShouldThrowNullReferenceException()
    {
        UserService serviceWithNullUoW;
        Assert.Throws<ArgumentNullException>(() => serviceWithNullUoW = new(null));
    }

    [Fact]
    public void CreateUser_GeneratesDefaultCollections()
    {
        // Arrange
        User newUser = new()
        {
            Username = "Numa",
            Password = "secretpassword"
        };

        var types = new HashSet<DefaultCollectionType>() {
                 new (){Id = 1,Description="Bingo",Name ="Planned"},
                 new (){Id = 2,Description="Bing",Name = "Playing"},
                 new (){Id = 3,Description="Bingo",Name="Drooped"},
                 new (){Id = 4,Description="Bingo",Name="Completed"}
             };

        _uowMock.SetupAllProperties();
        _uowMock.Setup(uow => uow.DefaultCollectionTypes.AsEnumerable())
             .Returns(types);

        _uowMock.Setup(uow => uow.Roles.FindAll(It.IsAny<Expression<Func<Role, bool>>>()))
            .Returns(new HashSet<Role>() { new() { Id = 1, Name = "User" } });

        _uowMock.Setup(uow => uow.Users.Add(It.IsAny<User>()));

        // Act
        _userService.CreateUser(newUser);

        // Assert
        Assert.Equal(types, newUser.DefaultCollections.Select(dc => dc.DefaultCollectionType));
    }

    public void Dispose() => _userService.Dispose();
}
