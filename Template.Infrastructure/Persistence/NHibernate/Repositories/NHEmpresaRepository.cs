using NHibernate;
using Template.Domain.Aggregates.EmpresaAggregate;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Persistence.NHibernate.Repositories;

public class NHEmpresaRepository : NHBaseRepository<Empresa>, IEmpresaRepository
{
    public NHEmpresaRepository(ISession session) : base(session) { }

    public async Task<List<Empresa>> ListAsync(EmpresaQueryFilter filtro)
    {
        var query = _dbSession.QueryOver<Empresa>()
            .Where(obj => obj.DeletedAt == null);

        if (filtro.Nome != null) query.Where(obj => obj.Nome == filtro.Nome);
        if (filtro.Email != null) query.Where(obj => obj.Email == filtro.Email);
        if (filtro.Search != null) query.WhereRestrictionOn(c => c.Nome).IsLike($"%{filtro.Search}%");
        if (filtro.Paginated) query
            .Skip((filtro.Page - 1) * filtro.PageSize)
            .Take(filtro.PageSize);
        
        var result = await query
            .OrderBy(obj => obj.CreatedAt).Desc            
            .ListAsync();
        
        return result.ToList();
    }

    public async Task<int> CountAsync(EmpresaQueryFilter filtro)
    {
        var query = _dbSession.QueryOver<Empresa>()
            .Where(obj => obj.DeletedAt == null);

        if (filtro.Nome != null) query.Where(obj => obj.Nome == filtro.Nome);
        if (filtro.Email != null) query.Where(obj => obj.Email == filtro.Email);
        if (filtro.Search != null) query.WhereRestrictionOn(c => c.Nome).IsLike($"%{filtro.Search}%");
        
        var result = await query.RowCountAsync();
        
        return result;
    }
}