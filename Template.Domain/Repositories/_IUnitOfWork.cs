namespace Template.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();

    IEmpresaRepository Empresas { get; }
}
