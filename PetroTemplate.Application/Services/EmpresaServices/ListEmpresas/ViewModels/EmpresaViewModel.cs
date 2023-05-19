using PetroTemplate.Domain.Aggregates.EmpresaAggregate;

namespace PetroTemplate.Application.Services.EmpresaServices.ListEmpresas.ViewModels;

public class EmpresaViewModel
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string? Email { get; private set; }
    
    public EmpresaViewModel(Empresa empresa)
    {
        Id = empresa.Id;
        Nome = empresa.Nome;
        Email = empresa.Email;
    }
}
