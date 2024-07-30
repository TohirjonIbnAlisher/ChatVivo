using System.Linq.Expressions;

namespace Enitities.Repositories;

public interface IRepository<TKey, TEntity>
{
    IQueryable<TEntity> SelectByExpressionAsync(
        Expression<Func<TEntity, bool>> expression,
        string[] includes);

    IQueryable<TEntity> SelectAll();
    Task<TEntity> SelectByIdAsync(TKey id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);


}
