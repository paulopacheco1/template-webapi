using System.Net;
using Template.Application.SeedWork;
using Template.Domain.Aggregates.EmpresaAggregate;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Services.EmpresaServices.UpdateEmpresa;

public class UpdateEmpresaRequestHandler : RequestHandler<UpdateEmpresaRequest, Guid>
{
    public UpdateEmpresaRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Guid> Handle(UpdateEmpresaRequest request, CancellationToken cancellationToken)
    {
        var empresa = await GetEmpresa(request.EmpresaId);

        await CheckExisteEmpresaComMesmoNome(empresa, request.Nome);
        
        empresa.AtualizarDados(request.Nome, request.Email);

        await _unitOfWork.CommitAsync();
        return empresa.Id;
    }

    private async Task<Empresa> GetEmpresa(Guid empresaId)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(empresaId);
        if (empresa == null) throw new AppException("Empresa não encontrada.", HttpStatusCode.NotFound);
        return empresa;
    }

    private async Task CheckExisteEmpresaComMesmoNome(Empresa empresa, string novoNome)
    {
        var empresaComMesmoNome = (await _unitOfWork.Empresas.ListAsync(new EmpresaQueryFilter()
        {
            Nome = novoNome,
        })).FirstOrDefault();

        if (empresaComMesmoNome != null && empresaComMesmoNome.Id != empresa.Id) 
            throw new AppException("Já existe uma empresa cadastrada com esse nome.");
    }
}
