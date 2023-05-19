using PetroTemplate.Domain.Aggregates.EmpresaAggregate;

namespace PetroTemplate.Application.Services.EmpresaServices.GetEmpresa.ViewModels;

public class EnderecoViewModel
{
    public string CEP { get; private set; }
    public string UF { get; private set; }
    public string Cidade { get; private set; }
    public string Bairro { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }

    public EnderecoViewModel(EmpresaEndereco endereco)
    {
        CEP = endereco.CEP; 
        UF = endereco.UF; 
        Cidade = endereco.Cidade; 
        Bairro = endereco.Bairro; 
        Logradouro = endereco.Logradouro; 
        Numero = endereco.Numero; 
        Complemento = endereco.Complemento; 
    }
}
