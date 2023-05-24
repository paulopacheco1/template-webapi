using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Aggregates.EmpresaAggregate;

public class EmpresaEndereco : ValueObject
{
    #region EFCORE
#pragma warning disable CS8618
    public EmpresaEndereco() { }
#pragma warning restore CS8618
    #endregion EFCORE

    public virtual string CEP { get; protected set; }
    public virtual string UF { get; protected set; }
    public virtual string Cidade { get; protected set; }
    public virtual string Bairro { get; protected set; }
    public virtual string Logradouro { get; protected set; }
    public virtual string Numero { get; protected set; }
    public virtual string? Complemento { get; protected set; }

    public EmpresaEndereco(string cep, string uf, string cidade, string bairro, string logradouro, string numero, string? complemento = null)
    {
        CEP = cep;
        UF = uf;
        Cidade = cidade;
        Bairro = bairro;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
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
