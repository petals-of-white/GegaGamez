using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    /// <summary>
    /// Generic EF Core repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

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

        public TEntity? Get(int id) => _dbSet.Find(id);

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).ToList();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var list = await _dbSet.Where(predicate).ToListAsync();
            var enumerable = await Task.FromResult(list.AsEnumerable());

            return enumerable;
        }

        public Task<TEntity?> GetAsync(int id) => _dbSet.FindAsync(id).AsTask();

        public IEnumerable<TEntity> List() => _dbSet.ToList();

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            var list = await _dbSet.ToListAsync();
            var enumerable = await Task.FromResult(list.AsEnumerable());

            return enumerable;
        }

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        //public void RemoveAsync(TEntity entity)

        public void Remove(int id)
        {
            TEntity? entity = _dbSet.Find(id);

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

        public async Task RemoveAsync(int id)
        {
            TEntity? entity = await GetAsync(id);

            if (entity is not null)
            {
                Remove(entity);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), $"Record with Id {id} does not exist");
            }
        }

        //public Task RemoveAllAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //}

        public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

        //public Task RemoveRangeAsync(IEnumerable<TEntity>)
        //{
        //}
    }
}
