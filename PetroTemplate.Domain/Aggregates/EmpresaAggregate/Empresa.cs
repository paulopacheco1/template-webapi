using System.Net;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Aggregates.EmpresaAggregate;

public class Empresa : AggregateRoot
{
    #region EFCORE
#pragma warning disable CS8618
    public Empresa() { }
#pragma warning restore CS8618
    #endregion EFCORE

    public virtual string Nome { get; protected set; }
    public virtual string? Email { get; protected set; }
    public virtual IList<EmpresaFilial> Filiais { get; protected set; }

    public Empresa(string nome, string? email = null)
    {
        Nome = nome;
        Email = email;

        Filiais = new List<EmpresaFilial>();
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
