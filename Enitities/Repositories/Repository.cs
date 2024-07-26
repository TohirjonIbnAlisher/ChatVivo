
using Enitities.Contexs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Enitities.Repositories;

public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
{
    private readonly ChatVivoDataContex chatVivoDataContex;

    public Repository(ChatVivoDataContex chatVivoDataContex)
    {
        this.chatVivoDataContex = chatVivoDataContex;
    }

    public async Task<TEntity> DeleteAsync(TKey id)
    {
        var entity = await this.SelectByIdAsync(id);
        var entityEntry = this.chatVivoDataContex.Set<TEntity>().Remove(entity);

        await this.chatVivoDataContex.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity? entity)
    {
        var removed = this.chatVivoDataContex
                .Set<TEntity>()
                .Remove(entity);

        await this.chatVivoDataContex.SaveChangesAsync();

        return removed.Entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var inserted = await this.chatVivoDataContex.Set<TEntity>().AddAsync(entity);

        return inserted.Entity;
    }

    public IQueryable<TEntity> SelectAll()
    {
        var a = chatVivoDataContex
            .Set<TEntity>()
            .AsNoTracking();
        return a;
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression, string[] includes)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<TEntity>> SelectByIdAsync(
        Expression<Func<TEntity, bool>> expression,
        string[] includes)
    {
        var entities = chatVivoDataContex
        .Set<TEntity>()
            .Where(expression);


        foreach (var item in includes)
        {
            entities = entities.Include(item);
        }

        return entities;
    }

    public Task<TEntity> SelectByIdAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return  chatVivoDataContex
            .Set<TEntity>()
            .Update(entity)
            .Entity;
    }

   
}
