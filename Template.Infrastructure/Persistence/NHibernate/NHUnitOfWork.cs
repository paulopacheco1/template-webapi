using NHibernate;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence.NHibernate.Repositories;

namespace Template.Infrastructure.Persistence.NHibernate;

public class NHUnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ISession _session;

    public NHUnitOfWork(ISessionFactory sessionFactory)
    {
        _session = sessionFactory.OpenSession();
        
        _session.FlushMode = FlushMode.Commit;
        _session.BeginTransaction();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _session.GetCurrentTransaction().CommitAsync();
        }
        catch (System.Exception)
        {
            await _session.GetCurrentTransaction().RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        _session.Close();
    }

    private IEmpresaRepository? _empresaRepository;
    public IEmpresaRepository Empresas { get { return _empresaRepository ??= new NHEmpresaRepository(_session); } }
}
