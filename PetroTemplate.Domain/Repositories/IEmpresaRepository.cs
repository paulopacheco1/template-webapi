using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Repositories;

public interface IEmpresaRepository : IRepository<Empresa>
{
    Task AddAsync(Empresa empresa);
    void Update(Empresa empresa);
    void Remove(Empresa empresa);
    Task<Empresa?> GetByIdAsync(Guid id);
    Task<List<Empresa>> ListAsync(EmpresaQueryFilter filtro);
    Task<int> CountAsync(EmpresaQueryFilter filtro);
}

public class EmpresaQueryFilter : QueryFilter
{
    public string? Nome { get; set; }
    public string? Email { get; set; }

    public EmpresaQueryFilter(QueryFilter queryFilter) : base(queryFilter)
    {
    }

    public EmpresaQueryFilter() : base()
    {
    }
}
