using NHibernate;
using NHibernate.Linq;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Infrastructure.Persistence.NHibernate.Repositories;

public abstract class NHBaseRepository<TType> where TType : Entity
{
    protected readonly ISession _dbSession;

    protected NHBaseRepository(ISession session)
    {
        _dbSession = session;
    }

    public virtual async Task<TType?> GetByIdAsync(Guid id)
    {
        return await _dbSession.Query<TType>()
            .Where(obj => obj.DeletedAt == null)
            .Where(obj => obj.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(TType obj)
    {
        await _dbSession.SaveAsync(obj);
    }

    public void Add(TType obj)
    {
        _dbSession.Save(obj);
    }

    public void Update(TType obj)
    {
        _dbSession.Update(obj);
    }

    public void Remove(TType obj)
    {
        _dbSession.Delete(obj);
    }
}
