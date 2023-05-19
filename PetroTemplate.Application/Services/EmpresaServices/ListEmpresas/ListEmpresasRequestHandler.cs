using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Application.Services.EmpresaServices.ListEmpresas;

public class ListEmpresasRequestHandler : RequestHandler<ListEmpresasRequest, ListEmpresasResponse>
{
    public ListEmpresasRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<ListEmpresasResponse> Handle(ListEmpresasRequest request, CancellationToken cancellationToken)
    {
        var (empresas, count, filter) = await ListEmpresas(request);
        return new ListEmpresasResponse(empresas, count, filter);
    }

    private async Task<(List<Empresa>, int, QueryFilter)> ListEmpresas(ListEmpresasRequest request)
    {
        var filter = new EmpresaQueryFilter(request);

        var empresas = await _unitOfWork.Empresas.ListAsync(filter);
        var count = await _unitOfWork.Empresas.CountAsync(filter);
        return (empresas, count, filter);
    }
}
