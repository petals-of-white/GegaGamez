using System.Linq.Expressions;

namespace GegaGamez.Shared.Repositories
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        int Count();

        int CountAll(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync();

        TEntity? Get(int id);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> GetAsync(int id);

        IEnumerable<TEntity> List();

        Task<IEnumerable<TEntity>> ListAsync();

        void Remove(int id);

        void Remove(TEntity entity);

        void RemoveAll(Expression<Func<TEntity, bool>> predicate);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
