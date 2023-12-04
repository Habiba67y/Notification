using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Domain.Common.Entities;
using System.Linq.Expressions;

namespace Notification.Infrastructure.Persistence.Repositories;

public abstract class EntityRepositoryBase<TIEntity, TContext> where TIEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => (TContext)_dbContext;
    private readonly DbContext _dbContext;

    public EntityRepositoryBase(TContext dbContext)
    {
        _dbContext = dbContext;
    }
    protected IQueryable<TIEntity> Get(Expression<Func<TIEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TIEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected ValueTask<TIEntity?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellation = default)
    {
        var initialQuery = DbContext.Set<TIEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        var entity = initialQuery.FirstOrDefaultAsync(e => e.Id == id, cancellationToken: cancellation);

        return new(entity);
    }

    protected async ValueTask<IList<TIEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellation = default)
    {
        var initialQuery = DbContext.Set<TIEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        var entities = await initialQuery.Where(entity => ids.Contains(entity.Id))
            .ToListAsync(cancellationToken: cancellation);
        return entities;
    }

    protected async ValueTask<TIEntity> CreateAsync(TIEntity entity, bool saveChanges = true, CancellationToken cancellation = default)
    {
        await DbContext.Set<TIEntity>().AddAsync(entity, cancellation);

        if (saveChanges)
            await _dbContext.SaveChangesAsync(cancellation);

        return entity;
    }

    protected async ValueTask<TIEntity> UpdateAsync(TIEntity entity, bool saveChanges = true, CancellationToken cancellation = default)
    {
        DbContext.Set<TIEntity>().Update(entity);

        if (saveChanges)
            await _dbContext.SaveChangesAsync(cancellation);

        return entity;
    }

    protected async ValueTask<TIEntity?> DeleteAsync(TIEntity entity, bool saveChanges = true, CancellationToken cancellation = default)
    {
        DbContext.Set<TIEntity>().Remove(entity);

        if (saveChanges)
            await _dbContext.SaveChangesAsync(cancellation);

        return entity;
    }

    protected async ValueTask<TIEntity?> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellation = default)
    {
        var entity = await GetByIdAsync(id) ?? throw new InvalidOperationException();

        return await DeleteAsync(entity, saveChanges, cancellation);
    }
}

