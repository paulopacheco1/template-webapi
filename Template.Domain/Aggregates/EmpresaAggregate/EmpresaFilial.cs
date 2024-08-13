using Template.Domain.Seedwork;

namespace Template.Domain.Aggregates.EmpresaAggregate;

public class EmpresaFilial() : Entity
{
    public virtual string Nome { get; protected set; }
    public virtual EmpresaEndereco Endereco { get; protected set; }
    public virtual Empresa? Empresa { get; protected set; }

    public EmpresaFilial(Empresa empresa, string nome, EmpresaEndereco endereco) : this()
    {
        Nome = nome;
        Endereco = endereco;
        Empresa = empresa;
    }

    public virtual void Desativar()
    {
        base.SetDeleted();
    }
}
