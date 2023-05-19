using FluentValidation;
using PetroTemplate.Application.SeedWork;
using PetroTemplate.Application.Services.EmpresaServices.AddEmpresaFilial.InputModels;

namespace PetroTemplate.Application.Services.EmpresaServices.AddEmpresaFilial;

#pragma warning disable CS8618
public class AddEmpresaFilialRequest : IRequest<Guid>
{
    public Guid EmpresaId { get; private set; }

    public string Nome { get; set; }
    public AddEmpresaFilialEnderecoInputModel Endereco { get; set; }

    public void SetEmpresaId(Guid id)
    {
        EmpresaId = id;
    }
}

public class AddEmpresaFilialRequestValidator : AbstractValidator<AddEmpresaFilialRequest>
{
    public AddEmpresaFilialRequestValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O nome da filial é obrigatório");

        RuleFor(x => x.Endereco)
            .NotEmpty()
            .WithMessage("O endereço da filial é obrigatório")
            .SetValidator(new AddEmpresaFilialEnderecoInputModelValidator());
    }
}
