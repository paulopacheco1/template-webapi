using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Repositories;
using Template.Domain.Seedwork;
using Template.Infrastructure.Persistence.EFCore.Repositories;

namespace Template.Infrastructure.Persistence.EFCore;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly EFDataContext _context;

    public EFUnitOfWork(EFDataContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries<Entity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.GetType().GetMethod("SetUpdated", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entry.Entity, null);
                    break;
                case EntityState.Added:
                    entry.Entity.GetType().GetMethod("SetCreated", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entry.Entity, null);
                    break;
                case EntityState.Deleted:
                    entry.Entity.GetType().GetMethod("SetDeleted", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(entry.Entity, null);
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        await _context.SaveChangesAsync();
    }

    private IEmpresaRepository? _empresaRepository;
    public IEmpresaRepository Empresas { get { return _empresaRepository ??= new EFEmpresaRepository(_context); } }
}
