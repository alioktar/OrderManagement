using OrderManagement.Core.Entities;
using System.Linq.Expressions;

namespace OrderManagement.Core.Repository
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes = null);
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>>[] includes = null);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
    }
}
