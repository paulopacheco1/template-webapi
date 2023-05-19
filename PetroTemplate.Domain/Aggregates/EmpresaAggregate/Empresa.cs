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

    public string Nome { get; private set; }
    public string? Email { get; private set; }

    private readonly HashSet<EmpresaFilial> _filiais;
    public IReadOnlyCollection<EmpresaFilial> Filiais => _filiais;

    public Empresa(string nome, string? email = null)
    {
        Nome = nome;
        Email = email;

        _filiais = new HashSet<EmpresaFilial>();
    }

    public void AtualizarDados(string nome, string? email = null)
    {
        Nome = nome;
        Email = email;
    }

    public EmpresaFilial AdicionarFilial(string nomeFilial, EmpresaEndereco endereco)
    {
        var filial = _filiais.FirstOrDefault(f => f.Nome == nomeFilial);

        if (filial != null) throw new AppException("Já existe uma filial cadastrada com esse nome");

        filial = new EmpresaFilial(this, nomeFilial, endereco);
        _filiais.Add(filial);

        return filial;
    }

    public void RemoverFilial(Guid filialId)
    {
        var filial = _filiais.FirstOrDefault(f => f.Id == filialId);

        if (filial == null) throw new AppException("Filial não encontrada", HttpStatusCode.NotFound);

        _filiais.Remove(filial);
    }
}
