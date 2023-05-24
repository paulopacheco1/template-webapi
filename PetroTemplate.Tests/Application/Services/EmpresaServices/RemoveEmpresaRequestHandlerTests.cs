using System.Reflection;
using PetroTemplate.Application.Services.EmpresaServices.RemoveEmpresa;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Tests.Application.Services.EmpresaServices;

public class RemoveEmpresaRequestHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RemoveEmpresaRequestHandler _handler;

    public RemoveEmpresaRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RemoveEmpresaRequestHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_EmpresaRemovedAndCommitted()
    {
        // Arrange
        var empresaId = Guid.NewGuid();

        var request = new RemoveEmpresaRequest();
        request.SetEmpresaId(empresaId);

        var empresa = new Empresa("Empresa", "empresa@example.com");
        typeof(Entity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(empresa, empresaId);

        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync(empresa);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(MediatR.Unit.Value, result);
        _unitOfWorkMock.Verify(uow => uow.Empresas.Remove(empresa), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_UnknownEmpresaId_ThrowsAppExceptionWithNotFoundStatusCode()
    {
        // Arrange
        var empresaId = Guid.NewGuid();
        var request = new RemoveEmpresaRequest();
        request.SetEmpresaId(empresaId);
        _unitOfWorkMock.Setup(uow => uow.Empresas.GetByIdAsync(empresaId))
            .ReturnsAsync((Empresa?)null);

        // Act
        async Task Execute() => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<AppException>(Execute);
        _unitOfWorkMock.Verify(uow => uow.Empresas.Remove(It.IsAny<Empresa>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Never);
    }
}
