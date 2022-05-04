using System.Linq.Expressions;

namespace GegaGamez.Shared.DataAccess.Repositories;

/// <summary>
/// A generic repository interfaces with asymc methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepositoryAsync<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> AsEnumerableAsync();

    Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync();

    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> GetAsync(int id);
}
