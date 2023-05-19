using FluentValidation;
using PetroTemplate.Application.SeedWork;

namespace PetroTemplate.Application.Services.EmpresaServices.UpdateEmpresa;

#pragma warning disable CS8618
public class UpdateEmpresaRequest : IRequest<Guid>
{
    public Guid EmpresaId { get; private set; }

    public string Nome { get; set; }
    public string? Email { get; set; }

    public void SetEmpresaId(Guid id)
    {
        EmpresaId = id;
    }
}

public class UpdateEmpresaRequestValidator : AbstractValidator<UpdateEmpresaRequest>
{
    public UpdateEmpresaRequestValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O nome da empresa é obrigatório");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("E-mail da empresa inválido")
            .When(x => !string.IsNullOrEmpty(x.Email));
    }
}
