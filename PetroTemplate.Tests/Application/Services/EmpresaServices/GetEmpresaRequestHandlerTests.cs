using PetroTemplate.Application.Services.EmpresaServices.GetEmpresa;
using PetroTemplate.Application.Services.EmpresaServices.GetEmpresa.ViewModels;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;

namespace PetroTemplate.Tests.Application.Services.EmpresaServices;

public class GetEmpresaRequestHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly GetEmpresaRequestHandler _handler;

    public GetEmpresaRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetEmpresaRequestHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsGetEmpresaResponse()
    {
        // Arrange
        var empresaId = Guid.NewGuid();

        var request = new GetEmpresaRequest();
        request.SetEmpresaId(empresaId);

        var empresa = new Empresa("Empresa 1", "empresa1@example.com");
        empresa.AdicionarFilial("Filial 1", new EmpresaEndereco("12345", "SP", "Sao Paulo", "Centro", "Rua A", "10"));
        empresa.AdicionarFilial("Filial 2", new EmpresaEndereco("67890", "RJ", "Rio de Janeiro", "Copacabana", "Rua B", "20"));

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync(empresa);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(empresa.Id, result.Id);
        Assert.Equal(empresa.Nome, result.Nome);
        Assert.Equal(empresa.Email, result.Email);
        Assert.Equal(empresa.Filiais.Count, result.Filiais.Count());
    }

    [Fact]
    public async Task Handle_UnknownEmpresaId_ThrowsAppExceptionWithNotFoundStatusCode()
    {
        // Arrange
        var empresaId = Guid.NewGuid();
        
        var request = new GetEmpresaRequest();
        request.SetEmpresaId(empresaId);

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync((Empresa?)null);

        // Act
        async Task Execute() => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<AppException>(Execute);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
    }
}
