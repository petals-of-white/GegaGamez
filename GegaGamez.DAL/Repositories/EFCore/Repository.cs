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

        public void AddRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

        public int Count() => _dbSet.Count();

        public int CountAll(Expression<Func<TEntity, bool>> predicate) => _dbSet.Count(predicate);

        public TEntity? Get(int id) => _dbSet.Find(id);

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).ToList();

        public IEnumerable<TEntity> List() => _dbSet.ToList();

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

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

        public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
    }
}
