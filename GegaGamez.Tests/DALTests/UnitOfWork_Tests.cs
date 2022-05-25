using System;
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
    private readonly ITestOutputHelper _output;

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

        _output = outputHelper;
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
}
