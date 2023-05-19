using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Application.Services.EmpresaServices.AddEmpresaFilial;

public class AddEmpresaFilialRequestHandler : RequestHandler<AddEmpresaFilialRequest, Guid>
{
    public AddEmpresaFilialRequestHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<Guid> Handle(AddEmpresaFilialRequest request, CancellationToken cancellationToken)
    {
        var empresa = await GetEmpresa(request.EmpresaId);
        
        var endereco = new EmpresaEndereco(
            cep: request.Endereco.CEP,
            uf: request.Endereco.UF,
            cidade: request.Endereco.Cidade,
            bairro: request.Endereco.Bairro,
            logradouro: request.Endereco.Logradouro,
            numero: request.Endereco.Numero,
            complemento: request.Endereco.Complemento
        );

        var filial = empresa.AdicionarFilial(request.Nome, endereco);

        await _unitOfWork.CommitAsync();
        return filial.Id;
    }

    private async Task<Empresa> GetEmpresa(Guid empresaId)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(empresaId);
        if (empresa == null) throw new AppException("Empresa não encontrada.");
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
