using System.Linq.Expressions;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Teste.Operation.MasterData.Infra.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<List<TEntity>> FindAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(w => w.IsActive == includeInactive).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity == null) return;
        
        entity.Disable();
        
        await Context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Disable();

        await UpdateAsync(entity, cancellationToken);
        
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(async () =>
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            
            await Context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}