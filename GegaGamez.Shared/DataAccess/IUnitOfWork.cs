using GegaGamez.Shared.DataAccess.Repositories;

namespace GegaGamez.Shared.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ICommentRepository Comments { get; }
        ICountryRepository Countries { get; }
        IDefaultCollectionRepository DefaultCollections { get; }
        IDefaultCollectionTypeRepository DefaultCollectionTypes { get; }
        IDeveloperRepository Developers { get; }
        IGameRepository Games { get; }
        IGenreRepository Genres { get; }
        IRatingRepository Ratings { get; }
        IRoleRepository Roles { get; }
        IUserCollectionRepository UserCollections { get; }
        IUserRepository Users { get; }

        int Save();

        Task<int> SaveAsync();

        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
    }
}
