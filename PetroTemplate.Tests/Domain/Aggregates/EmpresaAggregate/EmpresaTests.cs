using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;

namespace PetroTemplate.Tests.Domain.Aggregates.EmpresaAggregate;

public class EmpresaTests
{
    public EmpresaTests()
    {
        //EFCore Empty Constructors
        var _1 = new Empresa();
        var _2 = new EmpresaFilial();
        var _3 = new EmpresaEndereco();
    }

    [Fact]
    public void Empresa_AtualizarDados_AtualizaDadosCorretamente()
    {        
        // Arrange
        var empresa = new Empresa("Test Company", "test@example.com");

        // Act
        empresa.AtualizarDados("Updated Company", "updated@example.com");

        // Assert
        Assert.Equal("Updated Company", empresa.Nome);
        Assert.Equal("updated@example.com", empresa.Email);
    }

    [Fact]
    public void Empresa_AdicionarFilial_CriaNovaFilialComSucesso()
    {
        // Arrange
        var empresa = new Empresa("Test Company");
        var endereco = new EmpresaEndereco("12345-000", "RJ", "Cidade", "Bairro", "rua", "1", null);

        // Act
        var filial = empresa.AdicionarFilial("Test Filial", endereco);

        // Assert
        Assert.NotNull(filial);
        Assert.Equal("Test Filial", filial.Nome);
        Assert.Equal(1, empresa.Filiais.Count);
        Assert.Equal(filial, empresa.Filiais[0]);
    }

    [Fact]
    public void Empresa_AdicionarFilial_LancaExcecao_QuandoJaExisteFilialComMesmoNome()
    {
        // Arrange
        var empresa = new Empresa("Test Company");
        var endereco = new EmpresaEndereco("12345-000", "RJ", "Cidade", "Bairro", "rua", "1", null);
        empresa.AdicionarFilial("Test Filial", endereco);

        // Act & Assert
        Assert.Throws<AppException>(() => empresa.AdicionarFilial("Test Filial", endereco));
    }

    [Fact]
    public void Empresa_RemoverFilial_MarcaFilialComoDeletada()
    {
        // Arrange
        var empresa = new Empresa("Test Company");
        var endereco = new EmpresaEndereco("12345-000", "RJ", "Cidade", "Bairro", "rua", "1", null);
        var filial = empresa.AdicionarFilial("Test Filial", endereco);

        // Act
        empresa.RemoverFilial(filial.Id);

        // Assert
        Assert.NotNull(filial.DeletedAt);
    }

    [Fact]
    public void Empresa_RemoverFilial_LancaExcecao_QuandoFilialNaoExiste()
    {
        // Arrange
        var empresa = new Empresa("Test Company");

        // Act & Assert
        Assert.Throws<AppException>(() => empresa.RemoverFilial(Guid.NewGuid()));
    }
}
