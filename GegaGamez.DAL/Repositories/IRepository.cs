using System.Linq.Expressions;

namespace GegaGamez.DAL.Repositories
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        int Count();

        int CountAll(Expression<Func<TEntity, bool>> predicate);

        TEntity? Get(int id);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> List();

        void Remove(TEntity entity);

        void Remove(int id);

        void RemoveAll(Expression<Func<TEntity, bool>> predicate);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
