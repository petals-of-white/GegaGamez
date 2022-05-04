using System.Linq.Expressions;

namespace GegaGamez.Shared.DataAccess.Repositories;

/// <summary>
/// A generic repository interface with default-sync methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    IEnumerable<TEntity> AsEnumerable();

    int Count();

    int CountAll(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

    TEntity? Get(int id);

    void Remove(int id);

    void Remove(TEntity entity);

    void RemoveAll(Expression<Func<TEntity, bool>> predicate);

    void RemoveRange(IEnumerable<TEntity> entities);
}
