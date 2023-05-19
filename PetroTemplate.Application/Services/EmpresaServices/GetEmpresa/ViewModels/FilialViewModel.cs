using PetroTemplate.Domain.Aggregates.EmpresaAggregate;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa.ViewModels;

public class FilialViewModel
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public EnderecoViewModel Endereco { get; private set; }

    public FilialViewModel(EmpresaFilial filial)
    {
        Id = filial.Id;
        Nome = filial.Nome;
        Endereco = new EnderecoViewModel(filial.Endereco);
    }
}
