using Microsoft.EntityFrameworkCore;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Infrastructure.Persistence.EFCore.Repositories;

public class EFEmpresaRepository : EFBaseRepository<Empresa, EFDataContext>, IEmpresaRepository
{
    public EFEmpresaRepository(EFDataContext context) : base(context) { }

    public override async Task<Empresa?> GetByIdAsync(Guid id)
    {
        return await GetEntities()
            .Where(obj => obj.DeletedAt == null)
            .Where(obj => obj.Id == id)
            .Include(obj => obj.Filiais.Where(r => r.DeletedAt == null))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Empresa>> ListAsync(EmpresaQueryFilter filtro)
    {
        return await GetEntities()
            .Where(obj => obj.DeletedAt == null)
            .Where(obj => filtro.Nome == null || obj.Nome == filtro.Nome)
            .Where(obj => filtro.Email == null || obj.Email == filtro.Email)
            .Where(obj => string.IsNullOrEmpty(filtro.Search) || obj.Nome.ToLower().Contains(filtro.Search.ToLower()))
            .OrderByDescending(obj => obj.CreatedAt)
            .CheckPagination(filtro)
            .ToListAsync();
    }

    public async Task<int> CountAsync(EmpresaQueryFilter filtro)
    {
        return await GetEntities()
            .Where(obj => obj.DeletedAt == null)
            .Where(obj => filtro.Nome == null || obj.Nome == filtro.Nome)
            .Where(obj => filtro.Email == null || obj.Email == filtro.Email)
            .Where(obj => string.IsNullOrEmpty(filtro.Search) || obj.Nome.ToLower().Contains(filtro.Search.ToLower()))
            .CountAsync();
    }
}