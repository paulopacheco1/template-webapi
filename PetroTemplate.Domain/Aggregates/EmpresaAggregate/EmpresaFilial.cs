using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Aggregates.EmpresaAggregate;

public class EmpresaFilial : Entity
{
    #region EFCORE
#pragma warning disable CS8618
    public EmpresaFilial() { }
#pragma warning restore CS8618
    #endregion EFCORE

    public virtual string Nome { get; protected set; }
    public virtual EmpresaEndereco Endereco { get; protected set; }
    public virtual Empresa? Empresa { get; protected set; }

    public EmpresaFilial(Empresa empresa, string nome, EmpresaEndereco endereco)
    {
        Nome = nome;
        Endereco = endereco;
        Empresa = empresa;
    }

    protected internal virtual void Desativar()
    {
        base.SetDeleted();
    }
}
