using Template.Application.Services.EmpresaServices.GetEmpresa.ViewModels;
using Template.Domain.Aggregates.EmpresaAggregate;

namespace Template.Application.Services.EmpresaServices.GetEmpresa;

public class GetEmpresaResponse : EmpresaViewModel
{
    public GetEmpresaResponse(Empresa empresa) : base(empresa)
    {
    }
}
