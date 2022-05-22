using System.Linq.Expressions;
using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories;

/// <summary>
/// A generic repository interface with default-sync methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    IEnumerable<TEntity> AsEnumerable(params Expression<Func<TEntity, object>> [] includes);

    int Count();

    int CountAll(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>> [] includes);

    TEntity? Get(int id, params Expression<Func<TEntity, object>> [] includes);

    void Remove(int id);

    void Remove(TEntity entity);

    void RemoveAll(Expression<Func<TEntity, bool>> predicate);

    void RemoveRange(IEnumerable<TEntity> entities);
}
