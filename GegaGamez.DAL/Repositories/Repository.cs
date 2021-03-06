using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

/// <summary>
/// Generic EF Core repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected DbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    protected static IQueryable<TEntity> QueryWithIncludes(IQueryable<TEntity> initialQuery, Expression<Func<TEntity, object>> [] includes)
    {
        initialQuery = includes.Aggregate(initialQuery, (current, property) => current.Include(property));
        return initialQuery;
    }

    protected IQueryable<TEntity> DbSetQueryWithIncludes(Expression<Func<TEntity, object>> [] includes) =>
        QueryWithIncludes(_dbSet.AsQueryable(), includes);

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity) => _dbSet.Add(entity);

    public Task AddAsync(TEntity entity)
    {
        return _dbSet.AddAsync(entity).AsTask();
    }

    public void AddRange(IEnumerable<TEntity> entities) =>
        _dbSet.AddRange(entities);

    public Task AddRangeAsync(IEnumerable<TEntity> entities) =>
        _dbSet.AddRangeAsync(entities);

    public virtual IEnumerable<TEntity> AsEnumerable(params Expression<Func<TEntity, object>> [] includes)
        => DbSetQueryWithIncludes(includes).ToList();

    public virtual async Task<IEnumerable<TEntity>> AsEnumerableAsync(
        params Expression<Func<TEntity, object>> [] includes)
        => await DbSetQueryWithIncludes(includes).ToListAsync();

    public int Count() => _dbSet.Count();

    public int CountAll(Expression<Func<TEntity, bool>> predicate) =>
        _dbSet.Count(predicate);

    public Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate) =>
        _dbSet.CountAsync(predicate);

    public Task<int> CountAsync() => _dbSet.CountAsync();

    /// <summary>
    /// Finds all the entries by predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
                                                params Expression<Func<TEntity, object>> [] includes) =>
        DbSetQueryWithIncludes(includes).Where(predicate).ToList();

    /// <summary>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>> [] includes)
        => await DbSetQueryWithIncludes(includes).Where(predicate).ToListAsync();

    /// <summary>
    /// Finds an entry by Id. This method doesn't use eager loading by default, So it can be
    /// overriden to do so
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TEntity if found, otherwise null</returns>
    public virtual TEntity? Get(int id, params Expression<Func<TEntity, object>> [] includes) =>
         DbSetQueryWithIncludes(includes).SingleOrDefault(entity => entity.Id == id);

    public virtual Task<TEntity?> GetAsync(int id, params Expression<Func<TEntity, object>> [] includes)
    {
        return DbSetQueryWithIncludes(includes).SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public void Remove(int id)
    {
        TEntity? entity = Get(id);

        if (entity is not null)
            Remove(entity);
        else
            throw new ArgumentOutOfRangeException(nameof(id), $"Record with Id {id} does not exist");
    }

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void RemoveAll(Expression<Func<TEntity, bool>> predicate) =>
        RemoveRange(FindAll(predicate));

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        _dbSet.RemoveRange(entities);
}
