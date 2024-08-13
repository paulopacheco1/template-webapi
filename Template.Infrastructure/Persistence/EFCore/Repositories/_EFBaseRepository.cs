using Microsoft.EntityFrameworkCore;
using Template.Domain.Seedwork;

namespace Template.Infrastructure.Persistence.EFCore.Repositories;

public abstract class EFBaseRepository<TType, TContext> where TType : Entity, new() where TContext : EFDataContext
{
    protected readonly TContext _dbContext;

    protected EFBaseRepository(TContext context)
    {
        _dbContext = context;
    }

    protected IQueryable<TType> GetEntities()
    {
        return _dbContext.Set<TType>().AsQueryable();
    }

    public virtual async Task<TType?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<TType>().AsQueryable()
            .Where(obj => obj.DeletedAt == null)
            .Where(obj => obj.Id == id)
            .FirstOrDefaultAsync();
    }

    public virtual async Task AddAsync(TType obj)
    {
        await _dbContext.Set<TType>().AddAsync(obj);
    }

    public virtual void Add(TType obj)
    {
        _dbContext.Set<TType>().Add(obj);
    }

    public virtual void Update(TType obj)
    {
        _dbContext.Set<TType>().Update(obj);
    }

    public virtual void Remove(TType obj)
    {
        _dbContext.Set<TType>().Remove(obj);
    }
}
