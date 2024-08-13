using System.Net;
using Template.Domain.Exceptions;
using Template.Domain.Seedwork;

namespace Template.Domain.Aggregates.EmpresaAggregate;

public class Empresa() : AggregateRoot
{
    public virtual string Nome { get; protected set; }
    public virtual string? Email { get; protected set; }
    public virtual IList<EmpresaFilial> Filiais { get; protected set; }

    public Empresa(string nome, string? email = null) : this()
    {
        Nome = nome;
        Email = email;

        Filiais = [];
    }

    public virtual void AtualizarDados(string nome, string? email = null)
    {
        Nome = nome;
        Email = email;
    }

    public virtual EmpresaFilial AdicionarFilial(string nomeFilial, EmpresaEndereco endereco)
    {
        var filial = Filiais.FirstOrDefault(f => f.Nome == nomeFilial);

        if (filial != null) throw new AppException("Já existe uma filial cadastrada com esse nome");

        filial = new EmpresaFilial(this, nomeFilial, endereco);
        Filiais.Add(filial);

        return filial;
    }

    public virtual void RemoverFilial(Guid filialId)
    {
        var filial = Filiais.FirstOrDefault(f => f.Id == filialId);

        if (filial == null) throw new AppException("Filial não encontrada", HttpStatusCode.NotFound);

        filial.Desativar();
    }
}
