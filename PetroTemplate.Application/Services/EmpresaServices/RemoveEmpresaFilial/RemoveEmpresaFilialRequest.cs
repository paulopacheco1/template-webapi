using PetroTemplate.Application.SeedWork;

namespace PetroTemplate.Application.Services.EmpresaServices.RemoveEmpresaFilial;

#pragma warning disable CS8618
public class RemoveEmpresaFilialRequest : IRequest<MediatR.Unit>
{
    public Guid EmpresaId { get; private set; }
    public Guid FilialId { get; private set; }

    public void SetEmpresaId(Guid id)
    {
        EmpresaId = id;
    }

    public void SetFilialId(Guid id)
    {
        FilialId = id;
    }
}
