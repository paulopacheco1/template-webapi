using Template.Application.SeedWork;
using Template.Domain.Seedwork;

namespace Template.Application.Services.EmpresaServices.ListEmpresas;

public class ListEmpresasRequest : QueryFilter, IRequest<ListEmpresasResponse>
{
}
