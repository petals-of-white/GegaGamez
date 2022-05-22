using System.Linq.Expressions;
using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories;

/// <summary>
/// A generic repository interfaces with asymc methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryAsync<TEntity> where TEntity : class, IEntity
{
    Task<IEnumerable<TEntity>> AsEnumerableAsync(params Expression<Func<TEntity, object>> [] includes);

    Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync();

    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>> [] includes);

    Task<TEntity?> GetAsync(int id, params Expression<Func<TEntity, object>> [] includes);
}
