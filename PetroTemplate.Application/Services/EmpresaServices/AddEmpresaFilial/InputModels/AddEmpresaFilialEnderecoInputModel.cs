using FluentValidation;

namespace PetroTemplate.Application.Services.EmpresaServices.AddEmpresaFilial.InputModels;

#pragma warning disable CS8618
public class AddEmpresaFilialEnderecoInputModel
{
    public string CEP { get; set; }
    public string UF { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string? Complemento { get; set; }
}

public class AddEmpresaFilialEnderecoInputModelValidator : AbstractValidator<AddEmpresaFilialEnderecoInputModel>
{
    public AddEmpresaFilialEnderecoInputModelValidator()
    {
        RuleFor(x => x.CEP)
            .NotEmpty()
            .WithMessage("O CEP da loja é obrigatório")
            .Matches(@"^[\d]{5}-[\d]{3}$")
            .WithMessage("CEP da loja inválido");

        RuleFor(x => x.UF)
            .NotEmpty()
            .WithMessage("A UF da loja é obrigatória")
            .Matches(@"^[a-zA-Z]{2}$")
            .WithMessage("UF da loja inválida");

        RuleFor(x => x.Cidade)
            .NotEmpty()
            .WithMessage("A cidade da loja é obrigatória");

        RuleFor(x => x.Bairro)
            .NotEmpty()
            .WithMessage("O bairro da loja é obrigatório");

        RuleFor(x => x.Logradouro)
            .NotEmpty()
            .WithMessage("O logradouro da loja é obrigatório");

        RuleFor(x => x.Numero)
            .NotEmpty()
            .WithMessage("O número do endereço da loja é obrigatório");
    }
}
