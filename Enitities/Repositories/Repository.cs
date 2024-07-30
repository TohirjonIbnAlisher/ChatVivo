
using Enitities.Contexs;
using Enitities.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Enitities.Repositories;

public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> 
    where TEntity : BaseModel
{
    private readonly ChatVivoDataContex chatVivoDataContex;

    public Repository(ChatVivoDataContex chatVivoDataContex)
    {
        this.chatVivoDataContex = chatVivoDataContex;
    }

    public async Task<TEntity> SelectByIdAsync(TKey id) =>
            await this.chatVivoDataContex.Set<TEntity>().FindAsync(id);
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

        await this.chatVivoDataContex.SaveChangesAsync();

        return inserted.Entity;
    }

    public IQueryable<TEntity> SelectAll()
    {
        var a = chatVivoDataContex
            .Set<TEntity>()
            .AsNoTracking();
        return a;
    }

    public IQueryable<TEntity> SelectByExpressionAsync(
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



    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result =  chatVivoDataContex
            .Set<TEntity>()
            .Update(entity)
            .Entity;

        await this.chatVivoDataContex.SaveChangesAsync();

        return result;
    }

   
}
