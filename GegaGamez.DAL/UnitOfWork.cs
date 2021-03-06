using GegaGamez.DAL.Data;
using GegaGamez.DAL.Repositories;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ICommentRepository _comments;
    private readonly ICountryRepository _countries;
    private readonly DbContext _dbContext;
    private readonly IDefaultCollectionRepository _defaultCollections;
    private readonly IDefaultCollectionTypeRepository _defaultCollectionTypes;
    private readonly IDeveloperRepository _developers;
    private readonly IGameRepository _games;
    private readonly IGenreRepository _genres;
    private readonly IRatingRepository _ratings;
    private readonly IRoleRepository _roles;
    private readonly IUserCollectionRepository _userCollections;
    private readonly IUserRepository _users;

    public UnitOfWork(GegaGamezContext dbContext)
    {
        _dbContext = dbContext;

        _comments = new CommentRepository(dbContext);
        _countries = new CountryRepository(dbContext);
        _defaultCollections = new DefaultCollectionRepository(dbContext);
        _defaultCollectionTypes = new DefaultCollectionTypeRepository(dbContext);
        _developers = new DeveloperRepository(dbContext);
        _games = new GameRepository(dbContext);
        _genres = new GenreRepository(dbContext);
        _ratings = new RatingRepository(dbContext);
        _userCollections = new UserCollectionRepository(dbContext);
        _users = new UserRepository(dbContext);
        _roles = new RoleRepository(dbContext);
    }

    public ICommentRepository Comments => _comments;

    public ICountryRepository Countries => _countries;

    public IDefaultCollectionRepository DefaultCollections => _defaultCollections;

    public IDefaultCollectionTypeRepository DefaultCollectionTypes => _defaultCollectionTypes;

    public IDeveloperRepository Developers => _developers;

    public IGameRepository Games => _games;

    public IGenreRepository Genres => _genres;

    public IRatingRepository Ratings => _ratings;

    public IRoleRepository Roles => _roles;
    public IUserCollectionRepository UserCollections => _userCollections;

    public IUserRepository Users => _users;

    public void Dispose() => _dbContext.Dispose();

    public int Save() => _dbContext.SaveChanges();

    public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();

    public void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
    {
        _dbContext.Update<TEntity>(entityToUpdate);
    }
}
