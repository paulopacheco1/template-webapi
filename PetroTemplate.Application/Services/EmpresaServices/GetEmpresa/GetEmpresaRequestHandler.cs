using System.Net;
using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa;

public class GetEmpresaRequestHandler : RequestHandler<GetEmpresaRequest, GetEmpresaResponse>
{
    public GetEmpresaRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<GetEmpresaResponse> Handle(GetEmpresaRequest request, CancellationToken cancellationToken)
    {
        var empresa = await GetEmpresa(request.EmpresaId);
        return new GetEmpresaResponse(empresa);
    }

    private async Task<Empresa> GetEmpresa(Guid empresaId)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(empresaId);
        if (empresa == null) throw new AppException("Empresa não encontrada.", HttpStatusCode.NotFound);
        return empresa;
    }
}
