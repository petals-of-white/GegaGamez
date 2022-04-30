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
        IUserRepository Users { get; }
        IUserCollectionRepository UserCollections { get; }

        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;

        int Save();

        Task<int> SaveChangesAsync();
    }
}
