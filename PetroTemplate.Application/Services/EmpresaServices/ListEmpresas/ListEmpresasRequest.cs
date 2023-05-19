using PetroTemplate.Application.SeedWork;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Application.Services.EmpresaServices.ListEmpresas;

public class ListEmpresasRequest : QueryFilter, IRequest<ListEmpresasResponse>
{
}
