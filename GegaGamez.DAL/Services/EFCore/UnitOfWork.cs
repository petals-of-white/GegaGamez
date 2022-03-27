using GegaGamez.DAL.Data;
using GegaGamez.DAL.Repositories;
using GegaGamez.DAL.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Services.EFCore
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields

        private readonly GegaGamezContext _dbContext;

        private readonly ICommentRepository _comments;
        private readonly ICountryRepository _countries;
        private readonly IDefaultCollectionRepository _defaultCollections;
        private readonly IDefaultCollectionTypeRepository _defaultCollectionTypes;
        private readonly IDeveloperRepository _developers;
        private readonly IGameRepository _games;
        private readonly IGenreRepository _genres;
        private readonly IRatingRepository _ratings;
        private readonly IUserCollectionRepository _userCollections;
        private readonly IUserRepository _users;

        #endregion Fields

        #region Constructors

        public UnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GegaGamezContext>();

            AddDbProvider(optionsBuilder, connectionString);

            GegaGamezContext dbContext = new(optionsBuilder.Options);

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
        }

        //public UnitOfWork(IConfiguration configuration, ConnectionStringData connectionStringData)
        //{
        //    string connectionString = configuration.GetConnectionString(connectionStringData.SqlConnectionName);

        // var optionsBuilder = new DbContextOptionsBuilder<GegaGamezContext>();

        // AddDbProvider(optionsBuilder, connectionString);

        // GegaGamezContext dbContext = new(optionsBuilder.Options);

        // _dbContext = dbContext;

        //    _comments = new CommentRepository(dbContext);
        //    _countries = new CountryRepository(dbContext);
        //    _defaultCollections = new DefaultCollectionRepository(dbContext);
        //    _defaultCollectionTypes = new DefaultCollectionTypeRepository(dbContext);
        //    _developers = new DeveloperRepository(dbContext);
        //    _games = new GameRepository(dbContext);
        //    _genres = new GenreRepository(dbContext);
        //    _ratings = new RatingRepository(dbContext);
        //    _userCollections = new UserCollectionRepository(dbContext);
        //    _users = new UserRepository(dbContext);
        //}

        #endregion Constructors

        #region Properties

        public ICommentRepository Comments => _comments;

        public ICountryRepository Countries => _countries;

        public IDefaultCollectionRepository DefaultCollections => _defaultCollections;

        public IDefaultCollectionTypeRepository DefaultCollectionTypes => _defaultCollectionTypes;

        public IDeveloperRepository Developers => _developers;

        public IGameRepository Games => _games;

        public IGenreRepository Genres => _genres;

        public IRatingRepository Ratings => _ratings;

        public IUserCollectionRepository UserCollections => _userCollections;

        public IUserRepository Users => _users;

        #endregion Properties

        #region Public methods

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            if (_dbContext.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entityToUpdate);
            }

            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion Public methods

        #region Private methods

        private void AddDbProvider(DbContextOptionsBuilder<GegaGamezContext> optionsBuilder, string connectionString)
        {
            //use another provider if neccessary
            optionsBuilder.UseSqlServer(connectionString);
        }

        #endregion Private methods
    }
}
