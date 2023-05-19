using PetroTemplate.Application.SeedWork;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa;

#pragma warning disable CS8618
public class GetEmpresaRequest : IRequest<GetEmpresaResponse>
{
    public Guid EmpresaId { get; private set; }

    public void SetEmpresaId(Guid id)
    {
        EmpresaId = id;
    }
}
