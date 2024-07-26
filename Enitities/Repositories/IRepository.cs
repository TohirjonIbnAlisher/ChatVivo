using System.Linq.Expressions;

namespace Enitities.Repositories;

public interface IRepository<TKey, TEntity>
{
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression, string[] includes);
    Task<TEntity> SelectByIdAsync(TKey id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TKey id);
    Task<TEntity> DeleteAsync(TEntity entity);
}
