using System.Reflection;
using PetroTemplate.Application.Services.EmpresaServices.CreateEmpresa;
using PetroTemplate.Domain.Aggregates.EmpresaAggregate;
using PetroTemplate.Domain.Exceptions;
using PetroTemplate.Domain.Repositories;
using PetroTemplate.Domain.Seedwork;

namespace PetroTemplate.Tests.Application.Services.EmpresaServices;

public class CreateEmpresaRequestHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateEmpresaRequestHandler _handler;

    public CreateEmpresaRequestHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateEmpresaRequestHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_CreatesEmpresaAndReturnsId()
    {
        // Arrange
        var request = new CreateEmpresaRequest
        {
            Nome = "Test Company",
            Email = "test@example.com"
        };
        var empresaId = Guid.NewGuid();

        _unitOfWorkMock.Setup(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(new List<Empresa>());

        _unitOfWorkMock.Setup(uow => uow.Empresas.AddAsync(It.IsAny<Empresa>()))
            .Callback<Entity>(entity =>
            {
                typeof(Entity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(entity, empresaId);
            })
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(uow => uow.Empresas.AddAsync(It.IsAny<Empresa>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
        Assert.Equal(empresaId, result);
    }

    [Fact]
    public async Task Handle_WithExistingEmpresa_ThrowsAppException()
    {
        // Arrange
        var request = new CreateEmpresaRequest
        {
            Nome = "Existing Company",
            Email = "existing@example.com"
        };

        var existingEmpresa = new Empresa("Existing Company", "existing@example.com");
        var empresasList = new List<Empresa> { existingEmpresa };

        _unitOfWorkMock.Setup(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()))
            .ReturnsAsync(empresasList);

        // Act & Assert
        await Assert.ThrowsAsync<AppException>(() => _handler.Handle(request, CancellationToken.None));

        _unitOfWorkMock.Verify(uow => uow.Empresas.ListAsync(It.IsAny<EmpresaQueryFilter>()), Times.Once);
    }
}
