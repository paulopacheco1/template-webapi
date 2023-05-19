using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Aggregates.EmpresaAggregate;

public class EmpresaEndereco : ValueObject
{
    #region EFCORE
#pragma warning disable CS8618
    public EmpresaEndereco() { }
#pragma warning restore CS8618
    #endregion EFCORE

    public string CEP { get; private set; }
    public string UF { get; private set; }
    public string Cidade { get; private set; }
    public string Bairro { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string? Complemento { get; private set; }

    public EmpresaEndereco(string? cep, string? uf, string? cidade, string? bairro, string? logradouro, string? numero, string? complemento)
    {
        CEP = cep ?? "";
        UF = uf ?? "";
        Cidade = cidade ?? "";
        Bairro = bairro ?? "";
        Logradouro = logradouro ?? "";
        Numero = numero ?? "";
        Complemento = complemento ?? "";
    }

    public override string ToString()
    {
        return $"{CEP} | {UF} - {Cidade} - {Bairro} | {Logradouro} - {Numero} - {Complemento}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CEP;
        yield return UF;
        yield return Cidade;
        yield return Bairro;
        yield return Logradouro;
        yield return Numero;
        if (Complemento is not null) yield return Complemento;
    }
}
