using PetroTemplate.Application.Services.EmpresaServices.GetEmpresa.ViewModels;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa;

public class GetEmpresaResponse : EmpresaViewModel
{
    public GetEmpresaResponse(Empresa empresa) : base(empresa)
    {
    }
}
