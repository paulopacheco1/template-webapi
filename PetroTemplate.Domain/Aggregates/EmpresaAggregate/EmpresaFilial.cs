using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Domain.Aggregates.EmpresaAggregate;

public class EmpresaFilial : Entity
{
   #region EFCORE
#pragma warning disable CS8618
    public EmpresaFilial() { }
#pragma warning restore CS8618
    #endregion EFCORE

    public string Nome { get; private set; }
    public EmpresaEndereco Endereco { get; private set; }  
    public Empresa Empresa { get; private set; }  

    public EmpresaFilial(Empresa empresa, string nome, EmpresaEndereco endereco)
    {
        Nome = nome;
        Endereco = endereco;
        Empresa = empresa;
    }
}
