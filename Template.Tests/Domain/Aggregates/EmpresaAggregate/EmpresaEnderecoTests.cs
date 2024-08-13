using Template.Domain.Aggregates.EmpresaAggregate;

namespace Template.Tests.Domain.Aggregates.EmpresaAggregate;

public class EmpresaEnderecoTests
{
    [Fact]
    public void EmpresaEndereco_ToString_ReturnsFormattedAddressString()
    {
        // Arrange
        var endereco = new EmpresaEndereco("12345", "State", "City", "Bairro", "Street 1", "1", null);

        // Act
        var result = endereco.ToString();

        // Assert
        var expected = "12345 | State - City - Bairro | Street 1 - 1 - ";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void EmpresaEndereco_ToString_ReturnsFormattedAddressString_WithComplement()
    {
        // Arrange
        var endereco = new EmpresaEndereco("12345", "State", "City", "Bairro", "Street 1", "1", "Apt 2");

        // Act
        var result = endereco.ToString();

        // Assert
        var expected = "12345 | State - City - Bairro | Street 1 - 1 - Apt 2";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetEqualityComponents_SameProperties_ReturnsEqualComponents()
    {
        // Arrange
        var endereco1 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10");
        var endereco2 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10");

        // Act
        var isSame = endereco1 == endereco2;

        // Assert
        Assert.Equal(endereco1, endereco2);
        Assert.True(isSame);
    }

    [Fact]
    public void GetEqualityComponents_DifferentProperties_ReturnsDifferentComponents()
    {
        // Arrange
        var endereco1 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10");
        var endereco2 = new EmpresaEndereco("67890", "RJ", "Rio de Janeiro", "Copacabana", "Rua B", "20");

        // Act
        var isSame = endereco1 == endereco2;

        // Assert
        Assert.NotEqual(endereco1, endereco2);
        Assert.False(isSame);
    }

    [Fact]
    public void GetEqualityComponents_DifferentComplemento_ReturnsDifferentComponents()
    {
        // Arrange
        var endereco1 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10", "Apt 123");
        var endereco2 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10", "Apt 456");

        // Act
        var isSame = endereco1 == endereco2;

        // Assert
        Assert.NotEqual(endereco1, endereco2);
        Assert.False(isSame);
    }

    [Fact]
    public void GetEqualityComponents_NullComplemento_ReturnsEqualComponents()
    {
        // Arrange
        var endereco1 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10", null);
        var endereco2 = new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10", null);

        // Act
        var isSame = endereco1 == endereco2;

        // Assert
        Assert.Equal(endereco1, endereco2);
        Assert.True(isSame);
    }
}