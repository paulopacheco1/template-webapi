using PetroTemplate.Application.SeedWork;
using PetroTemplate.Application.Services.EmpresaServices.ListEmpresas.ViewModels;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Application.Services.EmpresaServices.ListEmpresas;

public class ListEmpresasResponse : Paginated<EmpresaViewModel>
{
    public ListEmpresasResponse(IEnumerable<Empresa> empresas, int count, QueryFilter filter) : base(empresas.Select(empresa => new EmpresaViewModel(empresa)), count, filter)
    {
    }
}
