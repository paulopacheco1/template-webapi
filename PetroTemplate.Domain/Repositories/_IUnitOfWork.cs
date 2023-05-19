namespace PetroTemplate.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> CommitAsync();

    IEmpresaRepository Empresas { get; }
}
