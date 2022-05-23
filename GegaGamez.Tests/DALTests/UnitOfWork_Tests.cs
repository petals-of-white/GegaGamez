using System;
using System.Linq;
using EntityFramework.Exceptions.Common;
using EntityFramework.Exceptions.SqlServer;
using GegaGamez.DAL;
using GegaGamez.DAL.Data;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace GegaGamez.Tests.DALTests;

public class UnitOfWork_Tests : IDisposable
{
    private readonly UnitOfWork _db;
    private readonly GegaGamezContext _dbContext;
    private readonly Game [] _games;
    private readonly ITestOutputHelper _output;
    private readonly Rating [] _ratings;
    private readonly User [] _users;

    public UnitOfWork_Tests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
        var conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        var contextOptions = new DbContextOptionsBuilder<GegaGamezContext>()
            .UseSqlServer(@"Data Source=(localdb)\MSSQLLocal;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            .UseExceptionProcessor()
            .Options;

        _dbContext = new(contextOptions);
        _db = new(_dbContext);

        /*
        var contextOptions = new DbContextOptionsBuilder<GegaGamezContext>().UseInMemoryDatabase("TestDb").Options;
        _dbContext = new(contextOptions);

        _db = new(_dbContext);
        _users = new User [] {
            new()
            {
                Username="Rerorer",
                Password="SpookyScary"
            },

            new()
            {
                Username="BigBlackCat",
                Password="SoNeedlessToSay"
            },

            new()
            {
                Username="ZombieO",
                Password="Yuyuyu"
            }
        };
        _db.Users.AddRange(_users);

        _games = new Game []
        {
            new()
            {
                Title="TestGameOne",
                ReleaseDate= DateTime.Today,
                Description="TastyTest",
                DeveloperId=3
            },
            new()
            {
                Title="TestGameTwo",
                ReleaseDate= DateTime.Today.AddYears(-1),
                Description="TestyTasty",
                DeveloperId=5
            },
            new()
            {
                Title="TestGameThree",
                ReleaseDate= DateTime.Today.AddYears(-2),
                Description="West?",
                DeveloperId=1
            },
        };
        _db.Games.AddRange(_games);

        _ratings = new Rating []
        {
            new () { User = _users[0], Game = _games[0], RatingScore = 5},
            new () { User = _users[1], Game = _games[1], RatingScore = 8},
            new () { User = _users[2], Game = _games[2], RatingScore = 10},
        };

        _db.Ratings.AddRange(_ratings);
        _db.Save();
        */
        _output = outputHelper;
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void AddingDublicate_ThrowsException(int gameIndex, int userIndex)
    {
        var dublicateRating = new Rating { UserId = _users [userIndex].Id, GameId = _games [gameIndex].Id, RatingScore = 2 };
        Assert.Throws<UniqueConstraintException>(() =>
        {
            _db.Ratings.Add(dublicateRating);
            _db.Save();
            _output.WriteLine("Dublicate has been added.. huh..");
        });
    }

    [Fact]
    public void AddingDublicateToSqlDb_ThrowsException()
    {
        var dublicateRating = new Rating { UserId = 1005, GameId = 1005, RatingScore = 2 };
        Assert.Throws<UniqueConstraintException>(() =>
        {
            _db.Ratings.Add(dublicateRating);
            _db.Save();
            _output.WriteLine("Dublicate has been added.. huh..");
        });
    }

    [Fact]
    public void DeletingEntityWithDefaultId_ThrowsInvalidOperationException()
    {
        // arrange

        // act
        Comment nonexistentComment = new() { Id = 0 };

        // assert
        var exception = Assert.Throws<InvalidOperationException>(() => _db.Comments.Remove(nonexistentComment));
    }

    [Fact]
    public void DeletingNonExistent_ThrowsDbUpdateConcurrencyException()
    {
        // arrange
        Comment nonexistentComment = new() { Id = 1 };

        // act
        _db.Comments.Remove(nonexistentComment);

        // assert
        var exception = Assert.ThrowsAny<DbUpdateConcurrencyException>(() => _db.Save());
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public void InsertingNullIntoNonNullable_ThrowsExactException()
    {
        Developer dev = new() { Name = null! };

        _db.Developers.Add(dev);

        var exception = Assert.Throws<NumericOverflowException>(() => _db.Save());
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Rating_FindsCorrect(int gameIndex, int userIndex)
    {
        var ratingsExpected = _ratings.Where(r => r.User == _users [userIndex] && r.Game == _games [gameIndex]).ToArray();

        var ratingsActual = _db.Ratings.FindAll(r => r.UserId == _users [userIndex].Id && r.GameId == _games [gameIndex].Id).ToArray();

        Assert.Single(ratingsExpected);
        Assert.Single(ratingsActual);
        Assert.Equal(ratingsExpected.Single(), ratingsActual.Single());
    }
}
