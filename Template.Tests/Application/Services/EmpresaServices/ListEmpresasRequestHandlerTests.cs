using Template.Application.Services.EmpresaServices.ListEmpresas;
using Template.Domain.Aggregates.EmpresaAggregate;
using Template.Domain.Repositories;

namespace Template.Tests.Application.Services.EmpresaServices;

public class ListEmpresasRequestHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ListEmpresasRequestHandler _handler;

    public ListEmpresasRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new ListEmpresasRequestHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsListEmpresasResponse()
    {
        // Arrange
        var request = new ListEmpresasRequest();
        var empresas = new List<Empresa>
        {
            new Empresa("Empresa 1", "empresa1@example.com"),
            new Empresa("Empresa 2", "empresa2@example.com")
        };
        var count = empresas.Count;

        _unitOfWorkMock.Setup(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(empresas);
        _unitOfWorkMock.Setup(uow => uow.Empresas.CountAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(count);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(empresas.Count, result.Data.Count());
        Assert.Equal(count, result.Count);
    }
}
