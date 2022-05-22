using System.Linq.Expressions;
using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories;

/// <summary>
/// A generic repository interface with both sync and async methods
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IRepositoryBase<TEntity>, IRepositoryAsync<TEntity> where TEntity : class, IEntity
{

}
