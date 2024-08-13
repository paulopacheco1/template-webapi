using Template.Application.SeedWork;
using Template.Application.Services.EmpresaServices.ListEmpresas.ViewModels;
using Template.Domain.Aggregates.EmpresaAggregate;
using Template.Domain.Seedwork;

namespace Template.Application.Services.EmpresaServices.ListEmpresas;

public class ListEmpresasResponse : Paginated<EmpresaViewModel>
{
    public ListEmpresasResponse(IEnumerable<Empresa> empresas, int count, QueryFilter filter) : base(empresas.Select(empresa => new EmpresaViewModel(empresa)), count, filter)
    {
    }
}
