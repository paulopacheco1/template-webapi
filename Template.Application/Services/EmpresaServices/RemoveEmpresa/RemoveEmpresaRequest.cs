using Template.Application.SeedWork;

namespace Template.Application.Services.EmpresaServices.RemoveEmpresa;

#pragma warning disable CS8618
public class RemoveEmpresaRequest : IRequest<MediatR.Unit>
{
    public Guid EmpresaId { get; private set; }

    public void SetEmpresaId(Guid id)
    {
        EmpresaId = id;
    }
}
