using System.Net;
using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Application.Services.EmpresaServices.RemoveEmpresaFilial;

public class RemoveEmpresaFilialRequestHandler : RequestHandler<RemoveEmpresaFilialRequest, MediatR.Unit>
{
    public RemoveEmpresaFilialRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<MediatR.Unit> Handle(RemoveEmpresaFilialRequest request, CancellationToken cancellationToken)
    {
        var empresa = await GetEmpresa(request.EmpresaId);

        empresa.RemoverFilial(request.FilialId);

        await _unitOfWork.CommitAsync();
        return MediatR.Unit.Value;
    }

    private async Task<Empresa> GetEmpresa(Guid empresaId)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(empresaId);
        if (empresa == null) throw new AppException("Empresa não encontrada.", HttpStatusCode.NotFound);
        return empresa;
    }
}
