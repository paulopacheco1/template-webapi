using FluentValidation;
using Template.Application.SeedWork;

namespace Template.Application.Services.EmpresaServices.CreateEmpresa;

#pragma warning disable CS8618
public class CreateEmpresaRequest : IRequest<Guid>
{
    public string Nome { get; set; }
    public string? Email { get; set; }
}

public class CreateEmpresaRequestValidator : AbstractValidator<CreateEmpresaRequest>
{
    public CreateEmpresaRequestValidator()
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
