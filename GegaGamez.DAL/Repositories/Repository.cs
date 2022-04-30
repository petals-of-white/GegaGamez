using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

/// <summary>
/// Generic EF Core repository
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    /// <summary>
    /// Override this property to include more
    /// </summary>
    protected virtual IQueryable<TEntity> DbSetWithIncludedProperties { get => _dbSet.AsQueryable(); }

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

    public void AddRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

    public Task AddRangeAsync(IEnumerable<TEntity> entities) => _dbSet.AddRangeAsync(entities);

    public int Count() => _dbSet.Count();

    public int CountAll(Expression<Func<TEntity, bool>> predicate) => _dbSet.Count(predicate);

    public Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate) => _dbSet.CountAsync(predicate);

    public Task<int> CountAsync() => _dbSet.CountAsync();

    /// <summary>
    /// Finds an entry by Id. This method doesn't use eager loading by default, So it can be
    /// overriden to do so
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TEntity if found, otherwise null</returns>
    public virtual TEntity? Get(int id) => _dbSet.Find(id);

    /// <summary>
    /// Finds all the entries by predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate) => DbSetWithIncludedProperties.Where(predicate).ToList();

    /// <summary>
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var list = await DbSetWithIncludedProperties.Where(predicate).ToListAsync();

        return list;
    }

    /// <summary>
    /// This method doesn't use eager loading by default, So it can be overriden to do so
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual Task<TEntity?> GetAsync(int id) => _dbSet.FindAsync(id).AsTask();

    public virtual IEnumerable<TEntity> List() => DbSetWithIncludedProperties.ToList();

    public virtual async Task<IEnumerable<TEntity>> ListAsync()
    {
        var list = await DbSetWithIncludedProperties.ToListAsync();

        return list;
    }

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void Remove(int id)
    {
        // TEntity? entity = _dbSet.Find(id);
        TEntity? entity = Get(id);

        if (entity is not null)
        {
            Remove(entity);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(id), $"Record with Id {id} does not exist");
        }
    }

    public void RemoveAll(Expression<Func<TEntity, bool>> predicate) => RemoveRange(GetAll(predicate));

    public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
}
