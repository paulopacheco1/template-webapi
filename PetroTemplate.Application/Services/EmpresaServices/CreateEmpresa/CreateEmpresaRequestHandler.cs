using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Application.Services.EmpresaServices.CreateEmpresa;

public class CreateEmpresaRequestHandler : RequestHandler<CreateEmpresaRequest, Guid>
{
    public CreateEmpresaRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Guid> Handle(CreateEmpresaRequest request, CancellationToken cancellationToken)
    {
        await CheckExisteEmpresaComMesmoNome(request.Nome);

        var empresa = new Empresa(request.Nome, request.Email);
        
        await _unitOfWork.Empresas.AddAsync(empresa);
        await _unitOfWork.CommitAsync();
        return empresa.Id;
    }    

    private async Task CheckExisteEmpresaComMesmoNome(string novoNome)
    {
        var empresaComMesmoNome = (await _unitOfWork.Empresas.ListAsync(new EmpresaQueryFilter()
        {
            Nome = novoNome,
        })).FirstOrDefault();

        if (empresaComMesmoNome != null) 
            throw new AppException("Já existe uma empresa cadastrada com esse nome.");
    }
}
