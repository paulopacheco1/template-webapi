using PetroTemplate.Domain.Aggregates.EmpresaAggregate;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa.ViewModels;

public class EmpresaViewModel
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string? Email { get; private set; }
    public IEnumerable<FilialViewModel> Filiais { get; private set; }
    
    public EmpresaViewModel(Empresa empresa)
    {
        Id = empresa.Id;
        Nome = empresa.Nome;
        Email = empresa.Email;
        Filiais = empresa.Filiais.Select(filial => new FilialViewModel(filial));
    }   
}
