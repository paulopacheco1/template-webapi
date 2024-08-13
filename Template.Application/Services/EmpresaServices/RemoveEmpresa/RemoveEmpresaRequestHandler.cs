using System.Net;
using Template.Application.SeedWork;
using Template.Domain.Aggregates.EmpresaAggregate;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Services.EmpresaServices.RemoveEmpresa;

public class RemoveEmpresaRequestHandler : RequestHandler<RemoveEmpresaRequest, MediatR.Unit>
{
    public RemoveEmpresaRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<MediatR.Unit> Handle(RemoveEmpresaRequest request, CancellationToken cancellationToken)
    {
        var empresa = await GetEmpresa(request.EmpresaId);

        _unitOfWork.Empresas.Remove(empresa);

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
